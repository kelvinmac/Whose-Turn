package repositories

import "go.uber.org/fx"

// Provide all repos
var Provider = fx.Provide(NewUserRepository,
)
