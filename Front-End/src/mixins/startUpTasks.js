import {updateUserProfile} from "../modules/Account/actions/authenticationActions";

/**
 * Requests the user profile if the a token exists
 * @param store Store instance
 */
export const initUserProfile = (store) => {
    const {user} = store.getState();

    if (user.authentication.isAuthenticated) {
        store.dispatch(updateUserProfile());
    }
};

