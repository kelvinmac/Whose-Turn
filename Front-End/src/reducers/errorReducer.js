import uuid from 'react-uuid'
export default function errorReducer (state ={
    critical: [],
    alert: []
}, {type, payload}) {
    switch (type) {
        case "APP::ERRORS::CRITICAL::UPDATED":
            return {
                ...state,
                critical: [
                    ...state.critical,
                    {

                            actions: null,
                        ...payload
                    }
                ]
            };
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