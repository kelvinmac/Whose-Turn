package models

type ApiErrors struct {
	Field  string
	Errors string
}

type ErrorModel struct {
	// Errors
	Errors map[string][]string  `json:"errors,omitempty"`
	// The error title
	Title string 				`json:"title"`
	// The http status code
	Status int 					`json:"status"`
}
