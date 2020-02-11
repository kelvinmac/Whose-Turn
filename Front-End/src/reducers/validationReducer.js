export default function validationReducer (state = {
    loginForm: {}
}, {type, payload}) {
    switch (type) {
        case "APP::VALIDATION::LOGINFORM::UPDATED":
            return {
                ...state,
                loginForm: {
                    ...state.loginForm,
                    ...payload
                }
            };
        default:
            return state;
    }
}