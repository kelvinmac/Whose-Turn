import React, {useEffect, useState} from "react";
import Avatar from '@material-ui/core/Avatar';
import Copyright from "../Copyright/Copyright";
import makeStyles from "@material-ui/core/styles/makeStyles";
import {authenticateUser, signUp} from "./actions/authenticationActions";
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import {connect} from "react-redux";
import Page from "../Components/Page/Page";
import Typography from "@material-ui/core/Typography";
import Grid from "@material-ui/core/Grid";
import TextField from "@material-ui/core/TextField";
import CssBaseline from "@material-ui/core/CssBaseline";
import Container from "@material-ui/core/Container";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Button from "@material-ui/core/Button";
import Link from "@material-ui/core/Link";
import {Link as DomLink, Redirect} from 'react-router-dom';
import {updateSignUpValidation} from "../Validation/actions/validationActions";
import Box from "@material-ui/core/Box";
import AlertError from "../Errors/AlertError";
import Alert from "@material-ui/lab/Alert";
import AlertTitle from "@material-ui/lab/Alert";
import Divider from "@material-ui/core/Divider";

const userStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(3),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
    divider: {
        border: "none",
        height: "1px",
        marginTop: theme.spacing(1),
        flexShrink: "0",
        backgroundColor: "rgba(0, 0, 0, 0.12)",
        width: "100%"
    }
}));

const SignUp = (props) => {
    const classes = userStyles();

    const [signUpData, setSignUpData] = useState({});

    const handleSubmit = (event) => {
        event.preventDefault();

        if (signUpData.password !== signUpData.confirm_password) {
            props.updateSignUpValidation({
                password: "Password does not match confirm password."
            });
            return;
        }

        props.updateSignUpValidation({
            password: null
        });
        props.signUp(signUpData);
    };

    const handleChange = (event) => {
        const {name, value, type} = event.target;
        let agreed = false;

        if (type === "checkbox")
            agreed = event.target.checked;

        setSignUpData((signUpData) => {
            return ({
                ...signUpData,
                [name]: type === "checkbox" ? agreed : value
            });
        });
    };

    return (
        <div>
            <Page
                title={"Whoseturn Login"}>
                <Container maxWidth="xs">
                    <CssBaseline/>
                    <div className={classes.paper}>
                        <Avatar className={classes.avatar}>
                            <LockOutlinedIcon/>
                        </Avatar>

                        <Typography component="h1" variant="h5">
                            Sign up
                        </Typography>
                        <Box m={3}>
                            <AlertError/>
                        </Box>

                        {props.user.isSignedUp &&
                        <>
                            <Alert severity="success">
                                You have successfully signed up
                            </Alert>
                            <DomLink component={Link} to={"/login"}>
                                Sign in
                            </DomLink>

                            <Divider className={classes.divider}/>
                        </>
                        }

                        {!props.user.isSignedUp &&
                        <form className={classes.form} onSubmit={handleSubmit}>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6}>
                                    <TextField
                                        autoComplete="First Name"
                                        error={props.errors?.firstName != null}
                                        helperText={props.errors?.firstName}
                                        value={signUpData.firstName}
                                        name="firstName"
                                        variant="outlined"
                                        required fullWidth
                                        id="firstName"
                                        label="First Name"
                                        onChange={handleChange}
                                        autoFocus
                                    />
                                </Grid>

                                <Grid item xs={12} sm={6}>
                                    <TextField
                                        autoComplete="Last Name"
                                        error={props.errors?.lastName != null}
                                        helperText={props.errors?.lastName}
                                        value={signUpData.lastName}
                                        name="lastName"
                                        variant="outlined"
                                        required fullWidth
                                        id="lastName"
                                        label="Last Name"
                                        onChange={handleChange}
                                    />
                                </Grid>

                                <Grid item xs={12}>
                                    <TextField
                                        variant="outlined"
                                        value={signUpData.email}
                                        required fullWidth
                                        id="email"
                                        label="Email Address"
                                        name="email"
                                        autoComplete="email"
                                        onChange={handleChange}
                                    />
                                </Grid>

                                <Grid item xs={12}>
                                    <TextField
                                        error={props.errors?.password != null}
                                        helperText={props.errors?.password}
                                        value={signUpData.password}
                                        variant="outlined"
                                        required fullWidth
                                        type="password"
                                        id="password"
                                        label="Password"
                                        name="password"
                                        onChange={handleChange}
                                    />
                                </Grid>

                                <Grid item xs={12}>
                                    <TextField
                                        variant="outlined"
                                        required fullWidth
                                        error={props.errors?.password_confirm != null}
                                        helperText={props.errors?.password_confirm}
                                        value={signUpData.password_confirm}
                                        type="password"
                                        id="confirm_password"
                                        label="Confirm Password"
                                        name="confirm_password"
                                        onChange={handleChange}
                                    />
                                </Grid>

                                <Grid item xs={12}>
                                    <FormControlLabel
                                        control={<Checkbox name="hasAgreed" onChange={handleChange}
                                                           required
                                                           value="termsAgreed" color="primary"/>}
                                        label="I have read and agree with the Terms and Conditions"
                                    />
                                </Grid>
                            </Grid>

                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                color="primary"
                                className={classes.submit}
                            >
                                Sign Up
                            </Button>
                            <Grid container justify="flex-end">
                                <Grid item>
                                    <DomLink component={Link} to={"/login"}>
                                        Already have an account? Sign in
                                    </DomLink>
                                </Grid>
                            </Grid>
                        </form>
                        }
                        <Copyright/>
                    </div>
                </Container>
            </Page>
        </div>
    )
};

const matchDispatchToProps = {
    updateSignUpValidation,
    signUp
};

const mapStateToProps = (state) => {
    return ({
            errors: state.validation.signUpForm,
            user: state.user
        }
    )
};

export default connect(
    mapStateToProps,
    matchDispatchToProps,
)(SignUp);