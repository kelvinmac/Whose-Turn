import uuid from 'react-uuid'

export function updateCritical(options) {
    if(options.id == null)
        options.id = uuid();

    return {
        type: "APP::ERRORS::CRITICAL::UPDATED",
         payload: {
             ...options
         }
    }
}

export function updateAlert(options){
    if(options.id == null)
        options.id = uuid();

    return {
        type: "APP::ERRORS::ALERT::UPDATED",
        payload:{
            ...options
        }
    }
}
