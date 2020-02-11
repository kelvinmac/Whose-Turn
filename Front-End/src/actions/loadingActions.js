export function updateBackdropState(show) {

    return {
        type : "APP::LOADING::BACKDROP::UPDATED",
        payload: {
            show
        }
    }
}