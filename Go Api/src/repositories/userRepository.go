package repositories

import (
	"WhoseTurn/src/database/entities"
	"github.com/google/uuid"
	"github.com/jinzhu/gorm"
	"github.com/sirupsen/logrus"
)

type UserRepository struct {
	_logger *logrus.Logger
	_db     *gorm.DB
}

func NewUserRepository(logger *logrus.Logger, db *gorm.DB) *UserRepository {
	return &UserRepository{
		_logger: logger,
		_db:     db,
	}
}

// Adds the given entity to the database
func (repo UserRepository) CreateNewUser(user *entities.User) error {
	return repo._db.Create(user).Error
}

// Finds the user the giver id
func (repo UserRepository) GetById(uuid uuid.UUID) (*entities.User, error) {
	var user entities.User

	result := repo._db.Where(&entities.User{UserId: uuid}).First(&user)

	if result.Error != nil {
		return nil, result.Error
	}

	return &user, nil
}

// Returns the user with the given email
func (repo UserRepository) GetByEmail(email string) (*entities.User, error) {
	var user entities.User

	result := repo._db.Where(&entities.User{Email: email}).First(&user)

	if result.Error != nil {
		return nil, result.Error
	}

	return &user, nil
}

// Returns true if a user with the given email exists
func (repo UserRepository) EmailExists(email string) (bool, error) {
	var users []entities.User
	result := repo._db.Where(&entities.User{Email: email}).Find(&users)

	if result.Error != nil {
		return false, result.Error
	}

	count := len(users)
	return count > 0, nil
}
