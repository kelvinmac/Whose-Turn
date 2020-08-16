package configurations

type DatabaseConfigurations struct {
	Dialect          string `json:"dialect"`
	ConnectionString string `json:"connectionString"`
}
