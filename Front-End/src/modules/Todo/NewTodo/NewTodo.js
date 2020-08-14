import {makeStyles} from "@material-ui/core/styles";
import React, {useState} from "react";
import axios from "axios";
import Page from "../../Components/Page/Page";
import Snackbar from "@material-ui/core/Snackbar";
import Alert from "@material-ui/lab/Alert";
import {Container} from "@material-ui/core";
import {Header} from "./Header";
import {TodoType} from "./TodoType";
import TodoDescription from "./TodoDescription";
import TodoDetails from "./TodoDetails";
import Preferences from "./Preferences";
import Button from "@material-ui/core/Button";
import {updateCritical} from "../../Errors/actions/errorActions";
import {updateBackdropState} from "../../Loading/actions/loadingActions";
import {updateCreateTodoValidation} from "../../Validation/actions/validationActions";
import {connect} from "react-redux";

const useStyles = makeStyles((theme) => ({
    todoType: {
        marginTop: theme.spacing(3)
    },
    todoDescription: {
        marginTop: theme.spacing(3)
    },
    todoDetails: {
        marginTop: theme.spacing(3)
    },
    preferences: {
        marginTop: theme.spacing(3)
    },
    submitButton: {
        marginTop: theme.spacing(3)
    }
}));

const NewTodo = ({updateCritical, updateBackdropState, updateCreateTodoValidation, errors, ...rest}) => {
    const classes = useStyles();

    const [todoState, setTodoState] = useState({
        privacy: {},
        description: {},
        details: {},
        preferences: {}
    });

    const [snackBarState, setSnackBarState] = useState({
        show: false
    });

    const snackbarClose = (event) => {
        setSnackBarState((oldState) => ({
            ...oldState,
            show: false
        }))
    };

    const setTodoType = (type) => {
        setTodoState(prevState => ({
            ...prevState,
            privacy: type
        }));
    };

    const onDescriptionChanged = (descriptionState) => {
        setTodoState(prevState => ({
            ...prevState,
            description: descriptionState
        }));
    };

    const onDetailsUpdated = (detailsState) => {
        setTodoState((prevState) => ({
            ...prevState,
            details: detailsState
        }));
    };

    const onPreferencesChanged = (preferencesState) => {
        setTodoState((prevState) => ({
            ...prevState,
            preferences: preferencesState
        }));
    };

    const submitTodo = (event) => {
        event.preventDefault();
        updateCreateTodoValidation({});

        console.log(todoState);
        axios.post(`${process.env.REACT_APP_API_URI}todos`, todoState)
            .then((response) => {
                updateBackdropState(true);
            })
            .catch((error) => {
                const {response} = error;

                // Connection error
                if (response == null || !response.data) {
                    updateCritical({
                        show: true,
                        message: "There was an error while contacting the server, try again shortly",
                        title: "Connection Error"
                    });
                    return;
                }

                const {errors, title} = response.data;

                updateCreateTodoValidation(errors);

                setSnackBarState({
                    show: true,
                    title: title ?? "There was one or more errors, please try again"
                })

            }).finally(() => {
            updateBackdropState(false);
        });
    };

    return (
        <Page
            title={"New todo"}
        >
            <Snackbar open={snackBarState.show} autoHideDuration={3000} onClose={snackbarClose}>
                <Alert onClose={snackbarClose} severity="warning">
                    {snackBarState.title}
                </Alert>
            </Snackbar>

            <Container maxWidth={"lg"}>
                <Header/>
                <form onSubmit={submitTodo}>
                    <TodoType className={classes.todoType}
                              onTodoTypeChanged={setTodoType}
                              errors={errors}
                              {...rest}
                    />

                    <TodoDescription className={classes.todoDescription} {...rest}
                                     errors={errors}
                                     isPersonal={todoState.type === "personal"}
                                     onDescriptionChanged={onDescriptionChanged}
                    />

                    <TodoDetails className={classes.todoDetails} {...rest}
                                 errors={errors}
                                 onDetailsChanged={onDetailsUpdated}
                    />

                    <Preferences className={classes.preferences} {...rest}
                                 errors={errors}
                                 onPreferencesChanged={onPreferencesChanged}/>

                    <Button type="submit"
                            className={classes.submitButton} {...rest}
                            variant="contained" color="primary">
                        Create Todo
                    </Button>
                </form>
            </Container>
        </Page>
    )
};

const matchDispatchToProps = {
    updateCritical,
    updateBackdropState,
    updateCreateTodoValidation
};

const mapStateToProps = (state) => ({
    errors: state.validation.createTodo
});

export default connect(
    mapStateToProps,
    matchDispatchToProps
)(NewTodo);