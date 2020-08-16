package models

type LoginModel struct {
	Email    string `json:"email"`
	Password string `json:"password"`
}
