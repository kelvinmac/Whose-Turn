import axios from "axios";
import {updateBackdropState} from "../../Loading/actions/loadingActions";
import {updateCritical} from "../../Errors/actions/errorActions";
import {updateAlert} from "../../Errors/actions/errorActions";
import {updateLoginValidation, updateSignUpValidation} from '../../Validation/actions/validationActions'


export const authenticateUser = model => async function (dispatch) {

    dispatch(updateBackdropState(true));

    const instance = axios.create();

    return dispatch({
        type: "USER::AUTHENTICATION::AUTHENTICATING",
        payload: instance.post(`${process.env.REACT_APP_API_URI}account/token`, model)
    }).then((response) => {

        const {token} = response.value.data;
        localStorage.setItem("__access_token", token);

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

        // Dispatch any Validation errors
        dispatch(updateLoginValidation(errors));

        let message = "Please check your credentials and try again";
        if (response.status === 401) {
            if (result.isLockedOut) {
                message = "You're account has been locked out";
            } else if (result.isNotAllowed) {
                message = "You're account has been disabled";
            } else if (result.requiresTwoFactor) {
                message = "You need to verify your email, please check your email or spam folder.";
            }
        }

        dispatch(updateAlert({message, show: true}));

    }).then(() => dispatch(updateBackdropState(false)))
};

export const signUp = (model) => function (dispatch) {
    dispatch(updateBackdropState(true));
    const instance = axios.create();

    instance.post(`${process.env.REACT_APP_API_URI}account/newuser`, model)
        .then((response) => {
            return dispatch(markUserAsSignedUp(true));
        })
        .catch((error) => {
            console.log(error);

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
            const {errors} = response.data;

            // Dispatch any Validation errors
            dispatch(updateSignUpValidation(errors));

            // Error title
            let message = response.data?.title;
            dispatch(updateAlert({message, show: true}));

        })
        .finally(() =>
            dispatch(updateBackdropState(false)));
};

export const markUserAsSignedUp = (isSignedUp) => function (dispatch) {
    return dispatch({
        type: "USER::AUTHENTICATION::AUTHENTICATING",
        payload: isSignedUp
    })
};

export const logout = () => function (dispatch) {
    localStorage.removeItem("__access_token");

    return dispatch({
        type: "USER::AUTHENTICATION::LOGOUT"
    });
};

export const tokenExpired = () => function (dispatch) {
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
};

export const updateUserProfile = () => function (dispatch) {

    return dispatch({
        type: "USER::PROFILE::REQUEST",
        payload: axios({
            method: 'get',
            url: `${process.env.REACT_APP_API_URI}account/profile`
        })
    }).then(() => {
        dispatch(updateUserPreferences());
    })
};

export const updateUserPreferences = () => function (dispatch) {

};

export const setRefreshToken = refreshToken => {
    localStorage.setItem("__access_token", refreshToken);

    return {
        type: "USER::AUTHENTICATION::REFRESH_TOKEN",
        payload: refreshToken
    };
};
