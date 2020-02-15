import userReducer from "../reducers/userReducer";
import {combineReducers} from "redux";
import loadingReducer from "./loadingReducer";
import errorReducer from "./errorReducer";
import validationReducer from "./validationReducer";
import todoReducer from "./todoReducer";


const reducers = combineReducers({
    user: userReducer,
    loading: loadingReducer,
    errors: errorReducer,
    validation: validationReducer,
    todo: todoReducer
});

export default reducers;