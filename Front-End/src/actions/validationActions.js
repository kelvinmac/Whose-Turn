export function updateLoginValidation(errors) {
    return {
        type : "APP::VALIDATION::LOGINFORM::UPDATED",
        payload: {
            ...errors
        }
    }
}