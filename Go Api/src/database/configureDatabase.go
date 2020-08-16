package database

import (
	"WhoseTurn/src/configurations"
	"WhoseTurn/src/database/entities"
	"github.com/jinzhu/gorm"
)

// Enables automatic migrations on all the tables
// param: db Pointer to database
func EnableAutoMigration(db *gorm.DB) {
	db.AutoMigrate(&entities.User{})
	db.AutoMigrate(&entities.HouseHold{})
}

// Initialises the database instance
// param: appSettings The apps configuration instance
// returns: A pointer to the database
//			Error thrown if no successful and null otherwise
func InitialiseDatabase(appSettings *configurations.AppSettings) (*gorm.DB, error) {
	config := appSettings.DatabaseConfigs
	db, err := gorm.Open(config.Dialect, config.ConnectionString)

	return db, err
}
