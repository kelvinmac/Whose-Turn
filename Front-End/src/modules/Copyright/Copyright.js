import Typography from "@material-ui/core/Typography";
import Link from "@material-ui/core/Link";
import React from "react";
import makeStyles from "@material-ui/core/styles/makeStyles";

//

const useStyles = makeStyles((theme) => ({
    root:{
        marginTop: theme.spacing(3),
    }
}));
/**
 * Copyright component
 * @returns {*}
 */
const Copyright = () => {
    const classes = useStyles();

    return (
        <Typography className={classes.root} variant="body2" color="textSecondary" align="center">
            {'Copyright © '}
            <Link color="inherit" href={process.env.REACT_APP_SELF_URI}>
                WhoseTurn
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
};

export default Copyright;