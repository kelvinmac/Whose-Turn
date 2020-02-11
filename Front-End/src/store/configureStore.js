import {applyMiddleware, createStore, compose} from 'redux'
import reducers from "../reducers";
import reduxImmutableStateInvariant from 'redux-immutable-state-invariant'
import thunk from 'redux-thunk';
import {createPromise} from "redux-promise-middleware";

export default function configureStore(preloadedState ={}) {
    const composeEnhancers =
        window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;


    return createStore(reducers, preloadedState, composeEnhancers(applyMiddleware(
        reduxImmutableStateInvariant(),
        thunk,
        createPromise({
            promiseTypeDelimiter: '::'
        })
    )));
}