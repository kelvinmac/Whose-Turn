


export default function errorReducer (state ={
    critical: [],
    alert: []
}, {type, payload}) {
    switch (type) {
        case "APP::ERRORS::CRITICAL::UPDATED": {

            return {
                ...state,
                critical:  computeNewState(state.critical, payload)
            }
        }

        case "APP::ERRORS::ALERT::UPDATED": {

            return{
                ...state,
                alert:  computeNewState(state.alert, payload)
            }
        }
        default:
            return state;
    }
}

// Compute the new state based using the payload
const computeNewState = (state, payload) => {
    const copy = [...state];
    let currentState = copy.find(a => a.id === payload.d);

    debugger;
    // Filters out the old state
    let result = [...copy.filter(e => e.id !== payload.id)];

    if(!payload.remove) {
        result.push({...currentState, ...payload});
    }

    return result;
};
