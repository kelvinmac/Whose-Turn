package whoseturn.cassandra;

import com.datastax.driver.core.AtomicMonotonicTimestampGenerator;
import com.datastax.driver.core.Cluster;
import com.google.common.io.Resources;
import com.google.common.net.HostAndPort;
import org.apache.cassandra.config.DatabaseDescriptor;
import org.apache.cassandra.service.EmbeddedCassandraService;
import org.apache.cassandra.service.StorageService;
import org.apache.cassandra.utils.FBUtilities;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.IOException;

import static com.google.common.base.Preconditions.checkState;

public class EmbeddedCassandra {
    private enum CassandraStatus {
        STARTING,
        STARTED,
        FAILED
    }

    private final static Logger _logger = LoggerFactory.getLogger(EmbeddedCassandra.class.getName());

    private static CassandraStatus _embeddedCassndraStatus = CassandraStatus.STARTING;

    /**
     * Starts a Cassandra server inside the current JVM and returns a client connection.
     */
    public static Cluster startAndConnect(String cassandraYamlUrl) {
        startCassandra(cassandraYamlUrl);
        return connectToCluster();
    }

    private static void startCassandra(String cassandraYamlUrl) {
        switch (_embeddedCassndraStatus) {
            case STARTED:
                _logger.debug("Skipping start of embedded Cassandra server, already started.");
                return;
            case FAILED:
                throw new IllegalStateException("Embedded Cassandra server startup failed, not retrying.");
        }

        _logger.info("Starting embedded Cassandra server.");
        _embeddedCassndraStatus = CassandraStatus.FAILED;

        // Override port settings so they won't conflict with an existing Cassandra daemon running on standard ports.
        setSystemProperty("cassandra.jmx.local.port", "17199");
        setSystemProperty("mx4jport", "19080");

        // Use a custom copy of cassandra.yaml with ports re-mapped to avoid conflicts with an existing Cassandra instance.
        setSystemProperty("cassandra.config", cassandraYamlUrl);
        setSystemProperty("cassandra.storagedir", "target/cassandra");

        // Note there is no clean way to stop and restart Cassandra within a JVM.
        try {
            new EmbeddedCassandraService().start();
        } catch (IOException e) {
            throw new RuntimeException("Unable to start embedded Cassandra for unit tests.", e);
        }

        // Suppress Cassandra shutdown errors (remove once https://issues.apache.org/jira/browse/CASSANDRA-8220 is resolved)
        StorageService.instance.removeShutdownHook();

        _embeddedCassndraStatus = CassandraStatus.STARTED;
    }

    /**
     * Sets a system property but only if it has not been set already.
     */
    private static void setSystemProperty(String key, String value) {
        if (System.getProperty(key) == null)
            System.setProperty(key, value);
    }

    private static Cluster connectToCluster() {
        HostAndPort hostAndPort = getNativeHostAndPort();

        _logger.debug("Connecting to local Cassandra server at {}...", hostAndPort);

        return Cluster.builder()
                .addContactPoint(hostAndPort.getHostText())
                .withPort(hostAndPort.getPort())
                .withTimestampGenerator(new AtomicMonotonicTimestampGenerator())
                .withoutMetrics()
                .withoutJMXReporting()
                .build();
    }

    /**
     * Returns the host and port on which the local in-memory Cassandra server is listening for CQL traffic.
     */
    public static HostAndPort getNativeHostAndPort() {
        checkState(_embeddedCassndraStatus == CassandraStatus.STARTED,
                "Embedded Cassandra has not been started");

        String host = FBUtilities.getBroadcastAddress().getHostAddress();
        int port = DatabaseDescriptor.getNativeTransportPort();

        return HostAndPort.fromParts(host, port);
    }
}
