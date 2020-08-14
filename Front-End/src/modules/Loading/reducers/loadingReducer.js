export default function loadingReducer(state = {
    backdrop: {
        show: false
    }
}, {type, payload}) {
    switch (type) {
        case "APP::LOADING::BACKDROP::UPDATED":
            return {
                ...state,
                backdrop: {
                    ...state.backdrop,
                    ...payload
                }
            };
        default:
            return state;
    }
}