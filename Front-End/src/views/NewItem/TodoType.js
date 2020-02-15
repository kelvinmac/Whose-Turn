import React, {useEffect, useState} from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardHeader from "@material-ui/core/CardHeader";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Radio from "@material-ui/core/Radio";
import clsx from "clsx";
import {Typography} from "@material-ui/core";
import {colors} from '@material-ui/core'

const useStyles = makeStyles((theme) => ({
    root: {},
    header: {
        fontSize: "20px",
        fontWeight: 500
    },
    option: {
        border: `1px solid ${theme.palette.divider}`,
        display: 'flex',
        alignItems: 'flex-start',
        padding: theme.spacing(2),
        maxWidth: 600,
        '& + &': {
            marginTop: theme.spacing(2)
        }
    },
    selectedOption: {
        backgroundColor: colors.grey[50]
    },
    title:{
        fontSize: "18px;",
        fontWeight: 500
    }
}));

const options = [
    {
        value: 'personal',
        title: 'Personal Todo',
        description: 'Only you will be able to see the todo'
    },
    {
        value: 'house',
        title: 'Household',
        description: 'Everyone in the household will see the todo and who it\'s assigned to.'
    }
];


export const TodoType = ({onTodoTypeChanged, className, ...rest}) => {
    const classes = useStyles();
    const [selected, setSelected] = useState("");

    useEffect(() => {
        if(onTodoTypeChanged != null)
            onTodoTypeChanged(selected);

    }, [selected]);

    const handleChange = (event, option) => {
        setSelected(option);
    };

    return (
        <Card
            {...rest}
            className={clsx(className, classes.root)}>
            <CardHeader title="What type of Todo?"/>
            <CardContent>
                {options.map(({value, title, description}) => {
                    return(
                        <div
                            className={clsx(classes.option, {
                                [classes.selectedOption]: selected === value
                            })}
                            key={value}
                        >
                            <Radio
                                checked={selected === value}
                                className={classes.optionRadio}
                                color="primary"
                                onClick={(event) => handleChange(event, value)}
                            />
                            <div className={classes.optionDetails}>
                                <Typography
                                    gutterBottom
                                    className={classes.title}
                                    variant="h5"
                                >
                                    {title}
                                </Typography>
                                <Typography variant="body1">{description}</Typography>
                            </div>
                        </div>
                    )
                })}
            </CardContent>
        </Card>
    )
};

