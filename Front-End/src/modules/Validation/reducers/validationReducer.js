export default function validationReducer(state = {
    loginForm: {},
    signUpForm: {},
    createTodo: {}
}, {type, payload}) {
    switch (type) {
        case "APP::VALIDATION::LOGINFORM::UPDATED":
            return {
                ...state,
                loginForm: {
                    ...payload
                }
            };
        case "APP::VALIDATION::SIGNUPFORM::UPDATED":
            return {
                ...state,
                signUpForm: {
                    ...payload
                }
            };
        case "APP::VALIDATION::CREATETODO::UPDATED":
            return {
                ...state,
                createTodo: {
                    ...payload
                }
            };
        default:
            return state;
    }
}