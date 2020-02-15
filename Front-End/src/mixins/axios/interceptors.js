import axios from "axios";
import {tokenExpired, setRefreshToken} from "../../actions/authenticationActions";
import {store} from "../../App";

export default function initInterceptors() {
// Attach access token to every request
    axios.interceptors.request.use((config) => {
        const {user} = store.getState();

        // Attach the token to the authenticated user
        if (user.authentication.isAuthenticated)
            config.headers.Authorization = `Bearer ${user.authentication.token}`;

        return config;
    }, (error) => {
        return Promise.reject(error);
    });


// Expired token interceptors
    axios.interceptors.response.use(response => {
        const refresh = response.headers["__refresh_token"];

        if(refresh != null) {
            store.dispatch(setRefreshToken(refresh));
        }
        return response;
    }, error => {
        if (error.response.status === 401) {
            store.dispatch(tokenExpired());
        } else
            return Promise.reject(error);
    });
}