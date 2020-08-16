package configurations

type AppSettings struct {
	ReactAppSettings       ReactApp               `json:"ReactApp"`
	JwtTokenSettings       jwtTokenSettings       `json:"JwtTokens"`
	DatabaseConfigs DatabaseConfigurations `json:"Database"`
}
