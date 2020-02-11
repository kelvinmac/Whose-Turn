export function updateCritical(options) {
    return {
        type: "APP::ERRORS::CRITICAL::UPDATED",
         payload: {
             ...options
         }
    }
}

export function updateAlert(options){
    return {
        type: "APP::ERRORS::ALERT::UPDATED",
        payload:{
            ...options
        }
    }
}
