package controllers

import "go.uber.org/fx"

// Provide all controllers
var Provider = fx.Provide(NewUserController,
	)
