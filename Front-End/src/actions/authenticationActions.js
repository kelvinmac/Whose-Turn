import React from "react";
import axios from "axios";
import {updateBackdropState} from "./loadingActions";
import {updateCritical} from "./errorActions";
import {updateAlert} from "./errorActions";
import {updateLoginValidation} from './validationActions'
import {useHistory} from "react-router-dom";


export function authenticateUser(model) {
    return async function (dispatch) {

        dispatch(updateBackdropState(true));

        const instance = axios.create();
        instance.interceptors.request.use(function (config) {
            return config;
        }, function (error) {
            return Promise.reject(error);
        });

        return dispatch({
            type: "USER::AUTHENTICATION::AUTHENTICATING",
            payload: instance.post(`${process.env.REACT_APP_API_URI}account/token`, model)
        }).then((response) => {

            const {token} = response.value.data;
            localStorage.setItem("__access_token", token);

            debugger;

            if (!model.remember) {
                localStorage.setItem("__access_token_time", new Date().getTime().toString());
            } else {
                localStorage.removeItem("__access_token_time");
            }

            // Get user profile
            return dispatch(updateUserProfile());
        }).catch((error) => {
            const {response} = error;

            // Connection error
            if (response == null) {
                dispatch(updateCritical({
                    show: true,
                    message: "There was an error while contacting the server, try again shortly",
                    title: "Connection Error"
                }));
                return;
            }

            const {result, errors} = response.data;

            // Dispatch any validation errors
            dispatch(updateLoginValidation(errors));

            let message = "Please check your credentials and try again";
            if (response.status === 401) {
                if (result.isLockedOut) {
                    message = "You're account has been locked out";
                } else if (result.isNotAllowed) {
                    message = "You're account has been disabled";
                } else if (result.requiresTwoFactor) {
                    message = "You need to verify your email";
                }
            }

            dispatch(updateAlert({message, show: true}));

        }).then(() => dispatch(updateBackdropState(false)))
    }
}

export function logout() {
    return function (dispatch) {
        localStorage.removeItem("__access_token");

        return dispatch({
            type: "USER::AUTHENTICATION::LOGOUT"
        });
    }
}

export function tokenExpired() {
    return function (dispatch) {
        dispatch(logout());

        dispatch(updateCritical({
            actions: [
                {
                    actionText: "Goto Login",
                    clickHandler: (event) => {
                        document.location = "/login";
                    }
                }
            ],
            title: "Login session expired",
            message: "You need to login again to regain access",
            show: true
        }))
    }
}

export function updateUserProfile() {
    return function (dispatch) {

        return dispatch({
            type: "USER::PROFILE::REQUEST",
            payload: axios({
                method: 'get',
                url: `${process.env.REACT_APP_API_URI}account/profile`
            })
        })
    };
}
