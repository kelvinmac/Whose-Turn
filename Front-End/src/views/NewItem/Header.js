import React from "react";
import Typography from "@material-ui/core/Typography";
import {makeStyles} from '@material-ui/styles';
import clsx from "clsx";

const useStyles = makeStyles(() => ({
    root: {},
    header: {
        fontSize: "24px",
        fontWeight: 500
    }
}));


export const Header = ({className,...rest}) => {
    const classes = useStyles();

    return (
        <div
            className={clsx(className, classes.root)}
            {...rest}
        >
            <Typography
                component="h2"
                gutterBottom
                variant="overline"
            >
                New Todo
            </Typography>
            <Typography
                component="h4"
                variant="h4"
                className={classes.header}
            >
                Add a new todo, and assign it to someone
            </Typography>
        </div>
    )
};
