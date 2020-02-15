import React, {useState} from "react";
import {makeStyles} from "@material-ui/core/styles";
import {connect} from 'react-redux'
import Page from "../../Components/Page";
import {Container} from "@material-ui/core";
import {Header} from "./Header";
import {TodoType} from "./TodoType";
import {TodoDescription} from "./TodoDescription";

const useStyles = makeStyles((theme) => ({
    todoType: {
        marginTop: theme.spacing(3)
    },
    todoDescription:{
        marginTop: theme.spacing(3)
    }
}));

export const NewItem = (props) => {
    const classes = useStyles();

    const [createTodo, setCreateTodo] = useState({});

    const setTodoType = (type) => {
        setCreateTodo({
            ...createTodo,
            type: type
        });
    };

    return (
        <Page
            title={"New todo"}
        >
            <Container maxWidth={"lg"}>
                <Header/>
                <TodoType setTodoType={setTodoType} className={classes.todoType}/>
                <TodoDescription className={classes.todoDescription} />
            </Container>
        </Page>
    )
};

const mapDispatchToProps = {};

const matchDispatchToProps = (state) => {
    return ({
    })
};

const mapStateToProps = (state) => {
    return ({}
    )
};

export default connect(
    mapDispatchToProps,
    matchDispatchToProps
)(NewItem);