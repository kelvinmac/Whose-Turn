package entities

import (
	"github.com/google/uuid"
	"time"
)

type HouseHold struct {
	Id            uuid.UUID `gorm:"type:uniqueidentifier;primary_key;unique;unique_index;notnull;default:newid();"`
	Name          string    `gorm:"type:varchar(500);not null"`
	CreatedBy     uuid.UUID `gorm:"type:uniqueidentifier;not null"`
	ManOfTheHouse uuid.UUID `gorm:"type:uniqueidentifier;not null"`
	Users         []User    `gorm:"foreignkey:HouseHoldId"`
	CreatedOn     *time.Time
}
