DROP KEYSPACE IF exists todos;
CREATE KEYSPACE todos WITH REPLICATION = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 };

CREATE TABLE todos.Todos_v1
(
    id          UUID,
    createdOn   TIMESTAMP,
    createdBy   UUID,
    dueOn       TIMESTAMP,
    task        TEXT,
    assignedTo  LIST<UUID>,
    isCompleted BOOLEAN,
    completedOn TIMESTAMP,
    PRIMARY KEY (id)
);
