

import Enumerable from "linq";
export default function errorReducer (state ={
    critical: [],
    alert: []
}, {type, payload}) {
    switch (type) {
        case "APP::ERRORS::CRITICAL::UPDATED": {
            const copy = [...state.critical];

            // find the error being updated
            const error = copy.find(c => c.id === payload.id);

            return {
                ...state,
                critical: [
                    // Filters out the old state
                    ...copy.filter(e => e.id !== payload.id),
                    {
                        ...error,
                        ...payload
                    }
                ]
            };
        }
        case "APP::ERRORS::ALERT::UPDATED":
            return {
                ...state,
                alert: [
                    ...state.alert,
                    {
                        ...payload
                    }
                ]
            };
        default:
            return state;
    }
}

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