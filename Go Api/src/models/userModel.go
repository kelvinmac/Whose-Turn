package models

import (
	"github.com/google/uuid"
)

type UserModel struct {
	UserId      uuid.UUID
	FirstName   string
	LastName    string
	Email       string
	HouseHoldId uuid.UUID
}
