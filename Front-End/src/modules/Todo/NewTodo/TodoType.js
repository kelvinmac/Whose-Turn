import React, {useEffect, useState} from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardHeader from "@material-ui/core/CardHeader";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Radio from "@material-ui/core/Radio";
import clsx from "clsx";
import {Typography} from "@material-ui/core";
import {colors} from '@material-ui/core'
import FormHelperText from "@material-ui/core/FormHelperText";
import FormGroup from "@material-ui/core/FormGroup";
import FormControl from "@material-ui/core/FormControl";

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
        flexDirection: "row",
        padding: theme.spacing(2),
        maxWidth: 600,
        '& + &': {
            marginTop: theme.spacing(2)
        }
    },
    selectedOption: {
        backgroundColor: colors.grey[50]
    },
    title: {
        fontSize: "18px;",
        fontWeight: 500
    }
}));

const options = [
    {
        value: 'private',
        title: 'Personal Todo',
        description: 'Only you will be able to see the todo'
    },
    {
        value: 'household',
        title: 'House',
        description: 'Everyone in the household will see the todo and who it\'s assigned to.'
    }
];


export const TodoType = ({onTodoTypeChanged, className, errors, ...rest}) => {
    const classes = useStyles();
    const [selected, setSelected] = useState({
        policy: "household"
    });

    useEffect(() => {
        if (onTodoTypeChanged != null)
            onTodoTypeChanged(selected);

    }, [selected]);

    const handleChange = (event, option) => {
        setSelected({
            policy: option
        });
    };

    return (
        <Card
            {...rest}
            className={clsx(className, classes.root)}>
            <CardHeader title="What type of Todo?"/>

            <CardContent>
                {options.map(({value, title, description}) => {
                    return (
                        <FormControl
                            className={clsx(classes.option, {
                                [classes.selectedOption]: selected === value
                            })}
                            key={value}
                        >
                            <Radio
                                checked={selected.policy === value}
                                className={classes.optionRadio}
                                name={value}
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
                        </FormControl>
                    )
                })}
                <FormControl error={errors["privacy.type"] != null}>
                    <FormHelperText>{errors["privacy.type"]}</FormHelperText>
                </FormControl>
            </CardContent>
        </Card>
    )
};

