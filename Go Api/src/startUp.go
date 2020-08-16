package main

import (
	"WhoseTurn/src/configurations"
	"WhoseTurn/src/controllers"
	"WhoseTurn/src/database"
	"WhoseTurn/src/repositories"
	"context"
	"fmt"
	"github.com/gorilla/mux"
	"github.com/jinzhu/configor"
	"github.com/jinzhu/gorm"
	_ "github.com/jinzhu/gorm/dialects/mssql"
	"github.com/rs/cors"
	"github.com/sirupsen/logrus"
	"go.uber.org/fx"
	"log"
	"net/http"
	"os"
	"time"
)

func ConfigureApi() {

	fx.New(
		controllers.Provider,
		repositories.Provider,
		fx.Provide(newConfiguration),
		fx.Provide(connectToDb),
		fx.Provide(newLogging),
		fx.Provide(newRouting),
		fx.Invoke(registerDi),
	).Run()
}

func registerDi(lifecycle fx.Lifecycle, router *mux.Router, config *configurations.AppSettings) {
	lifecycle.Append(
		fx.Hook{
			OnStart: func(c context.Context) error {
				// Start listening to incoming connections
				go startServer(router, config)
				return nil
			},
			OnStop: func(c context.Context) error {
				fmt.Println("Closing connection")
				return nil
			},
		},
	)

}

// Start the http server
func startServer(router *mux.Router, settings *configurations.AppSettings) {
	handler := configureCors(router, settings) // Configure cors

	log.Fatal(http.ListenAndServe(":4046", handler))
}

func newLogging() *logrus.Logger {
	var log = logrus.New()
	log.Out = os.Stdout

	return log
}

func commonMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		w.Header().Add("Content-Type", "application/json")
		next.ServeHTTP(w, r)
	})
}

func newRouting(usrC *controllers.UserController) *mux.Router {
	router := mux.NewRouter().StrictSlash(true)
	routeUserController(router, usrC) // Route th user controller

	router.Use(commonMiddleware)
	return router
}

// Configures routing for the users controller
func routeUserController(router *mux.Router, controller *controllers.UserController) {
	router.HandleFunc("/users/token", controller.Token).
		Methods("POST").
		Schemes("http").
		Headers("Content-Type", "application/json")

	router.HandleFunc("/users/create", controller.Create).
		Methods("POST").
		Schemes("http").
		Headers("Content-Type", "application/json")
}

// Configures the cors middleware
func configureCors(router *mux.Router, settings *configurations.AppSettings) http.Handler {

	c := cors.New(cors.Options{
		AllowedOrigins:   []string{settings.ReactAppSettings.Url},
		AllowCredentials: true,
		AllowedHeaders:   []string{settings.ReactAppSettings.RefreshTokenName},
		Debug:            false,
	})

	handler := cors.Default().Handler(router)

	// Insert the Cors middleware
	return c.Handler(handler)
}

// Connects to the database
func connectToDb(appSettings *configurations.AppSettings) (*gorm.DB, error) {
	db, err := database.InitialiseDatabase(appSettings)

	if err == nil{
		database.EnableAutoMigration(db)
	}

	return db, err
}

// Loads app configurations
func newConfiguration() (*configurations.AppSettings, error) {
	config := configurations.AppSettings{}

	// Load the config and reload every minute
	err := configor.New(&configor.Config{AutoReload: true,
		AutoReloadInterval: time.Minute}).Load(&config, "appsettings.json")

	return &config, err
}

//docker run --name sql_server_whose_turn -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=zetjar-diCce0-civwyz' -p 1433:1433 microsoft/mssql-server-linux
