package configurations
type jwtTokenSettings struct {
	Secret          string `json:"secret"`
	Issuer          string `json:"issuer"`
	Audience        string `json:"audience"`
	Lifetime        int    `json:"lifetime"`
	RefreshDuration int    `json:"refreshDuration"`
}

