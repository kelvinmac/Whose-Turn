

import React from "react";
import Page from "../Components/Page";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import {Link } from 'react-router-dom'

const useStyles = makeStyles((theme) => ({
    root: {
        padding: theme.spacing(3),
        paddingTop: '10vh',
        display: 'flex',
        flexDirection: 'column',
        alignContent: 'center'
    },
    imageContainer: {
        marginTop: theme.spacing(6),
        display: 'flex',
        justifyContent: 'center'
    },
    image: {
        maxWidth: '100%',
        width: 560,
        maxHeight: 300,
        height: 'auto'
    },
    buttonContainer: {
        marginTop: theme.spacing(6),
        display: 'flex',
        justifyContent: 'center'
    }
}));

export default function (props) {
    const classes = useStyles();

    return (
        <Page
            className={classes.root}
            title="Error 404"
        >
            <Typography
                align="center"
                variant={'h1'}
            >
                404: The page you are looking for isn’t here
            </Typography>

            <Typography
                align="center"
                variant="subtitle2"
            >
                You either tried some shady link or you came here by mistake. Whichever
                it is, try navigating back
            </Typography>

            <div className={classes.buttonContainer}>
                <Button
                    color="primary"
                    component={Link}
                    to="/"
                    variant="outlined"
                >
                    Back to home
                </Button>
            </div>
        </Page>
    )
}