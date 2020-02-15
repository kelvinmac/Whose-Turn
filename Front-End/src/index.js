import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import {BrowserRouter as Router} from 'react-router-dom'
import {MuiPickersUtilsProvider} from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import * as serviceWorker from './serviceWorker';

ReactDOM.render(
    <Router>
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
            <App/>
        </MuiPickersUtilsProvider>
    </Router>,

    document.getElementById('root')
);

serviceWorker.unregister();