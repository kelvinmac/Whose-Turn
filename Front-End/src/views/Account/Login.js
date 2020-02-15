import React, {useState} from "react";
import {makeStyles} from '@material-ui/core/styles';
import {connect} from 'react-redux'
import {authenticateUser} from "../../actions/authenticationActions";
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import {CssBaseline} from "@material-ui/core";
import Typography from "@material-ui/core/Typography";
import Link from '@material-ui/core/Link';
import Box from '@material-ui/core/Box';
import Grid from "@material-ui/core/Grid";
import Container from '@material-ui/core/Container';
import Avatar from "@material-ui/core/Avatar";
import TextField from "@material-ui/core/TextField";
import Checkbox from "@material-ui/core/Checkbox";
import Button from "@material-ui/core/Button";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import AlertError from "../../Components/Errors/AlertError";
import {Redirect} from 'react-router-dom';
import Page from "../../Components/Page";

function Copyright() {
    return (
        <Typography variant="body2" color="textSecondary" align="center">
            {'Copyright © '}
            <Link color="inherit" href={process.env.REACT_APP_SELF_URI}>
                WhoseTurn
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const useStyles = makeStyles(theme => ({
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
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));

const Login = (props) => {
    const classes = useStyles();
    const [loginData, setLoginData] = useState({
        remember: true
    });

    const {from} = props.location?.state || {from: '/'};

    const handleSubmit = (event) => {
        event.preventDefault();
        props.authenticateUser(loginData);
    };

    const handleChange = (event) => {
        const {name, value, type} = event.target;
        let remember = false;

        if (type === "checkbox")
            remember = event.target.checked;

        setLoginData({
            ...loginData,
            [name]: type === "checkbox" ? remember : value
        });
    };

    if (props.user.isAuthenticated) {
        return (<Redirect to={from}/>)
    } else {
        return (
            <Page
            title={"Whoseturn Login"}>
                <Container maxWidth="xs">
                    <CssBaseline/>

                    <div className={classes.paper}>
                        <Avatar className={classes.avatar}>
                            <LockOutlinedIcon/>
                        </Avatar>
                        <Typography component="h1" variant="h5">
                            Sign in
                        </Typography>
                        <Box m={3}>
                            <AlertError/>
                        </Box>
                        <form className={classes.form} onSubmit={handleSubmit}>
                            <TextField
                                error={props.errors?.email != null}
                                helperText={props.errors?.email}
                                variant="outlined"
                                margin="normal"
                                fullWidth
                                id="email"
                                label="Email Address"
                                name="email"
                                autoComplete="email"
                                autoFocus
                                onChange={handleChange}
                            />

                            <TextField
                                error={props.errors?.password != null}
                                helperText={props.errors?.password}
                                variant="outlined"
                                margin="normal"
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="current-password"
                                onChange={handleChange}
                            />
                            <FormControlLabel
                                control={<Checkbox
                                    value="remember"
                                    name="remember"
                                    checked={loginData.remember}
                                    color="primary"
                                    onChange={handleChange}/>}
                                label="Remember me"
                            />
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                color="primary"
                                className={classes.submit}
                            >
                                Sign In
                            </Button>

                            <Grid container>
                                <Grid item xs>
                                    <Link href="#" variant="body2">
                                        Forgot password?
                                    </Link>
                                </Grid>
                                <Grid item>
                                    <Link href="#" variant="body2">
                                        {"Don't have an account? Sign Up"}
                                    </Link>
                                </Grid>
                            </Grid>
                        </form>
                    </div>
                    <Box mt={8}>
                        <Copyright/>
                    </Box>
                </Container>
            </Page>
        )
    }
};

const matchDispatchToProps = {
    authenticateUser
};

const mapStateToProps = (state) => {
    return ({
            user: state.user.authentication,
            errors: state.validation.loginForm
        }
    )
};

export default connect(
    mapStateToProps,
    matchDispatchToProps,
)(Login)