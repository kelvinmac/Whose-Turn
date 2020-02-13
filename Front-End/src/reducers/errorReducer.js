

import Enumerable from "linq";
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

    if (payload.remove)
        currentState = null;
    else
        currentState = {...currentState, ...payload};

    return [
        // Filters out the old state
        ...copy.filter(e => e.id !== payload.id), {
            ...currentState
        }
    ];
};

// return {
//     ...state,
//     critical: [
//         ...state.critical,
//         {
//             id: payload.id == null ? uuid() : payload.id,
//             actions: null,
//             ...payload
//         }
//     ]
// };