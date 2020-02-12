

import Enumerable from "linq";
export default function errorReducer (state ={
    critical: [],
    alert: []
}, {type, payload}) {
    switch (type) {
        case "APP::ERRORS::CRITICAL::UPDATED": {

            return {
                ...state,
                critical:  computeNewState(state, payload)
            }
        }

        case "APP::ERRORS::ALERT::UPDATED": {

            return{
                ...state,
                alert:  computeNewState(state, payload)
            }
        }
        default:
            return state;
    }
}

// Compute the new state based using the payload
const computeNewState = (state, payload) => {
    const copy = [...state.alert];
    let alert = copy.find(a => a.id === payload.d);

    if (payload.remove)
        alert = null;
    else
        alert = {...alert, ...payload};

    return [
        // Filters out the old state
        ...copy.filter(e => e.id !== payload.id), {
            ...alert
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