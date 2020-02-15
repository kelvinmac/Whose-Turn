

import React from 'react';
import {Switch, Route, Redirect} from 'react-router-dom'
import {Provider as StoreProvider} from 'react-redux'
import {NewItem} from './views/NewItem'
import {configureStore} from "./store";
import TodaysItems from './views/TodaysItems'
import Item from "./views/Item";
import Home from "./views/Home";
import Login from "./views/Account/Login";
import MainLayout from "./layouts";
import EmptyLayout from "./layouts/EmptyLayout";
import FullScreenLoading from "./Components/Loading/FullScreenLoading";
import initInterceptors from "./mixins/axios/interceptors";
import CriticalError from "./Components/Errors/CriticalError";
import Logout from "./views/Account/Logout";

export const store = configureStore();

function RouteWithLayout({component: Component, layout: Layout, ...rest}) {
    return (
        <Route {...rest} render={(props) =>
            <Layout {...props}>
                <FullScreenLoading/>
                <Component {...props} />
            </Layout>
        }/>
    );
}

function PrivateRouteWithLayout({component: Component, layout: Layout, ...rest}) {
    return (
        <Route {...rest} render={(props) => {
            const {user} = store.getState();

            if (user.authentication.isAuthenticated) {
                return (
                    <Layout {...props}>
                        <FullScreenLoading/>
                        <Component {...props} />
                    </Layout>
                )
            } else {
                return <Redirect to={{
                    pathname: '/login',
                    state: {from: props.location}
                }}/>
            }
        }
        }/>
    );
}

class App extends React.Component {
    constructor(props) {
        super(props);

        // init the axios interceptors
        initInterceptors();
    }

    render() {
        return (
            <StoreProvider store={store}>
                <CriticalError/>
                <Switch>
                    <PrivateRouteWithLayout path="/newItem"
                                            component={NewItem}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/items/:id"
                                            component={Item}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/items/todays"
                                            component={TodaysItems}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/items/logcomplete"
                                            component={TodaysItems}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/allhistory"
                                            component={TodaysItems}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/history"
                                            component={TodaysItems}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/logout"
                                            component={Logout}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/" exact={true}
                                            component={Home}
                                            layout={MainLayout}/>

                    <RouteWithLayout path="/login"
                                     component={Login}
                                     layout={EmptyLayout}/>
                </Switch>
            </StoreProvider>
        );
    }
}

export default App;

