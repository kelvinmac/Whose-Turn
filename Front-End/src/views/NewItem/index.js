import React, {useState} from "react";
import {makeStyles} from "@material-ui/core/styles";
import {connect} from 'react-redux'
import Page from "../../Components/Page";
import {Container} from "@material-ui/core";
import {Header} from "./Header";
import {TodoType} from "./TodoType";
import TodoDescription from "./TodoDescription";
import TodoDetails from "./TodoDetails";

const useStyles = makeStyles((theme) => ({
    todoType: {
        marginTop: theme.spacing(3)
    },
    todoDescription: {
        marginTop: theme.spacing(3)
    },
    todoDetails: {
        marginTop: theme.spacing(3)
    }
}));

export const NewItem = (props) => {
    const classes = useStyles();

    const [createTodo, setCreateTodo] = useState({});

    const setTodoType = (type) => {
        setCreateTodo(prevState => ({
            ...prevState,
            type: type
        }));
    };

    const onDescriptionChanged = (descriptionState) => {
        setCreateTodo(prevState => ({
            ...prevState,
            description: descriptionState
        }));
    };

    const onDetailsUpdated = (detailsState) => {
        setCreateTodo((prevState) => ({
            ...prevState,
            details: detailsState
        }));
    };

    console.log(createTodo);
    return (
        <Page
            title={"New todo"}
        >
            <Container maxWidth={"lg"}>
                <Header/>
                <TodoType className={classes.todoType}
                          onTodoTypeChanged={setTodoType}
                />

                <TodoDescription className={classes.todoDescription}
                                 isPersonal={createTodo.type === "personal"}
                                 onDescriptionChanged={onDescriptionChanged}
                />

                <TodoDetails className={classes.todoDetails}
                             onDetailsChanged={onDetailsUpdated}
                />
                // todo, add submit button

            </Container>
        </Page>
    )
};

const mapDispatchToProps = {};

const matchDispatchToProps = (state) => {
    return ({})
};

const mapStateToProps = (state) => {
    return ({}
    )
};

export default connect(
    mapDispatchToProps,
    matchDispatchToProps
)(NewItem);