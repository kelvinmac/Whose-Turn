import React, {useState} from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardHeader from "@material-ui/core/CardHeader";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Radio from "@material-ui/core/Radio";
import clsx from "clsx";
import {Typography} from "@material-ui/core";
import {colors} from '@material-ui/core'
import {connect} from "react-redux";
import Alert from "@material-ui/lab/Alert";
import TextField from "@material-ui/core/TextField";


const useStyles = makeStyles((theme) => ({
    root: {},
    alert: {
        marginBottom: theme.spacing(3)
    },
    formGroup: {}
}));

export const TodoDescription = ({className, setTodoDescription, ...rest}) => {
    const classes = useStyles();

    const [description, setDescription] = useState({});

    const handleChangeEvent = (event) => {
        const {name, value} = event.target;
        setDescription({
            ...description,
            [name]: value
        });

    };

    return (
        <Card
            className={clsx(classes.root, className)}
            {...rest}>
            <CardHeader title="About this todo"/>
            <CardContent>
                <Alert
                    variant="outlined" severity="info"
                    className={classes.alert}
                >
                    Once you choose the todo name you can’t change it
                </Alert>

                <div className={classes.formGroup}>
                    <TextField
                        fullWidth
                        label={"Todo name"}
                        name={"todoName"}
                        value={description.name}
                        variant={"outlined"}
                        onChange={handleChangeEvent}
                    >

                    </TextField>
                </div>
            </CardContent>
        </Card>
    )
};
