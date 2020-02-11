
const token  = localStorage.getItem("__access_token");

export default function userReducer(state={
    authentication: {
        isAuthenticated: token != null,
        token,
    },
    profile: {}
}, {type, payload})
{
    switch (type) {
        case "USER::AUTHENTICATION::AUTHENTICATING::FULFILLED":
            return ({
                ...state,
                authentication: {
                    isAuthenticated: true,
                    token: payload.data.token,
                    tokenDate: new Date().getTime().toString()
                }
            });
        case "USER::AUTHENTICATION::AUTHENTICATING::REJECTED":
            return ({
                ...state,
                authentication: {
                    isAuthenticated: false,
                    token: null
                }
            });
        case "USER::PROFILE::REQUEST::FULFILLED":
            return({
                ...state,
                profile: {
                    ...payload.data
                }
            });
        case "USER::PROFILE::REQUEST::REJECTED":
            return({
                ...state,
                profile: null
            });
        case "USER::AUTHENTICATION::LOGOUT":
            return({
                ...state,
                profile: {},
                authentication: {
                    isAuthenticated: false,
                    token: null,
                    tokenDate: null
                }
            });

        default:
            return state;
    }
}
