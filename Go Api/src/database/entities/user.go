package entities

import (
	"github.com/google/uuid"
	"github.com/jinzhu/gorm"
	"time"
)

type User struct {
	gorm.Model
	UserId            uuid.UUID `gorm:"type:uniqueidentifier;primary_key;unique;unique_index;not null;default:newid();"`
	FirstName         string    `gorm:"type:varchar(128);not null"`
	LastName          string    `gorm:"type:varchar(128);not null"`
	Email             string    `gorm:"type:varchar(100);unique_index;not null"`
	Hash              string    `gorm:"type:varchar(MAX);not null"`
	SecurityToken     string    `gorm:"type:varchar(MAX);not null"`
	TwoFactorRequired bool      `gorm:"type:bit;not null"`
	AccountClosed     bool      `gorm:"type:bit;not null"`
	IsLockedOut       bool      `gorm:"type:bit;not null"`
	LockoutReason     string    `gorm:"type:varchar(500);"`
	HouseHoldId       uuid.UUID `gorm:"type:uniqueidentifier;"`
	LockoutEndDate    time.Time
	LastLogin         time.Time
	RegisteredOn      time.Time
	//HouseHoldId       uuid.UUID `gorm:"type:uniqueidentifier;unique_index"`
}
