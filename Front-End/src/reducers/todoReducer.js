

export default function todoReducer(state = {
    creating:{ }
}, {type, payload}) {
    switch (type) {
        case "TODO::CREATE::PENDING": {
            return {
                ...state.creating,
                payload
            }
        }

        default:
            return state;
    }
}