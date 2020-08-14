import {combineReducers} from "redux";
import userReducer from "../modules/Account/reducers/userReducer";
import loadingReducer from "../modules/Loading/reducers/loadingReducer";
import errorReducer from "../modules/Errors/reducers/errorReducer";
import validationReducer from "../modules/Validation/reducers/validationReducer";
import todoReducer from "../modules/Todo/reducers/todoReducer";


const reducers = combineReducers({
    user: userReducer,
    loading: loadingReducer,
    errors: errorReducer,
    validation: validationReducer,
    todo: todoReducer
});

export default reducers;