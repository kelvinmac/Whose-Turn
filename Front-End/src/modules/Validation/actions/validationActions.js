export function updateLoginValidation(errors) {
    return {
        type : "APP::VALIDATION::LOGINFORM::UPDATED",
        payload: {
            ...errors
        }
    }
}

export function updateSignUpValidation(errors){
    return{
        type: "APP::VALIDATION::SIGNUPFORM::UPDATED",
        payload : {
            ...errors
        }
    }
}

export function updateCreateTodoValidation(errors) {
    return{
        type: "APP::VALIDATION::CREATETODO::UPDATED",
        payload : {
            ...errors
        }
    }
}