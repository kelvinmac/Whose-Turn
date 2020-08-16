package controllers

import (
	"WhoseTurn/src/models"
	"WhoseTurn/src/repositories"
	"fmt"
	"github.com/go-playground/validator/v10"
	"github.com/iancoleman/strcase"
	"github.com/jinzhu/gorm"
	"github.com/sirupsen/logrus"
)

type UserController struct {
	_logger     *logrus.Logger
	_db         *gorm.DB
	_userRepo   *repositories.UserRepository
}

// Constructs the user controller
func NewUserController(logger *logrus.Logger, db *gorm.DB,
	userRepo *repositories.UserRepository) *UserController {
	return &UserController{
		_logger:   logger,
		_db:       db,
		_userRepo: userRepo,
	}
}

func (uc UserController) generateValidationError(err error, title string, status int) *models.ErrorModel {
	model := models.ErrorModel{}
	model.Title = title
	model.Status = status
	model.Errors = make(map[string][]string)

	for _, e := range err.(validator.ValidationErrors) {
		field := strcase.ToLowerCamel(e.Field())

		model.Errors[field] = append(model.Errors[field],
			fmt.Sprintf("%s field is required", e.Field()))
	}

	return &model
}

func (uc UserController) generateErrorModel(title string, status int) *models.ErrorModel {
	model := models.ErrorModel{}
	model.Title = title
	model.Status = status

	return &model
}
