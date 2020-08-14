import React from 'react';
import {Switch, Route, Redirect} from 'react-router-dom'
import {Provider as StoreProvider} from 'react-redux'
import {NewTodo} from './modules/Todo/NewTodo'
import {configureStore} from "./store";
import {TodaysItems, Item} from './modules/Items'
import {Home} from "./modules/Home";
import {Login, SignUp} from "./modules/Account";
import MainLayout from "./layouts";
import EmptyLayout from "./layouts/EmptyLayout";
import {FullScreenLoading} from "./modules/Loading";
import {initInterceptors} from "./mixins/axios";
import {CriticalError} from "./modules/Errors";
import {Logout} from "./modules/Account";
import {initUserProfile} from "./mixins/startUpTasks";

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
    }

    componentDidMount() {
        initInterceptors();  // init the axios interceptors

        initUserProfile(store); // Request the users profile
    }

    render() {
        return (
            <StoreProvider store={store}>
                <CriticalError/>
                <Switch>
                    <PrivateRouteWithLayout path="/todo/new"
                                            component={NewTodo}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/todo/:id"
                                            component={Item}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/items/myitems"
                                            component={TodaysItems}
                                            layout={MainLayout}/>

                    <PrivateRouteWithLayout path="/items/household"
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

                    <RouteWithLayout path="/signup"
                                     component={SignUp}
                                     layout={EmptyLayout}/>
                </Switch>
            </StoreProvider>
        );
    }
}

export default App;

