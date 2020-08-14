import React, {useEffect, useState} from "react";
import {TodoItems} from "../Todo/Items";
import Page from "../Components/Page/Page";
import axios from "axios";
import {CriticalError} from "../Errors";
import {updateCritical} from "../Errors/actions/errorActions";
import {updateBackdropState} from "../Loading/actions/loadingActions";
import {updateCreateTodoValidation} from "../Validation/actions/validationActions";
import {connect} from "react-redux";

const Home = ({updateCritical, ...rest}) => {
    const [todoData, setTodoData] = useState({});

    useEffect(() => {
        axios.get(`${process.env.REACT_APP_API_URI}todos`)
            .then(result => {
                setTodoData(result.data);
            })
            .catch(error => {
                updateCritical({
                    show: true,
                    message: "There was an error while retrieving todos, try again shortly"
                });
            });
    }, []);

    return (
        <Page
            title={"Home"}>
            <TodoItems todoData={todoData}/>
        </Page>
    )
};

const matchDispatchToProps = {
    updateCritical,
    updateBackdropState
};

const mapStateToProps = (state) => ({});

export default connect(
    mapStateToProps,
    matchDispatchToProps
)(Home);