import CardHeader from "@material-ui/core/CardHeader";
import CardContent from "@material-ui/core/CardContent";
import FormGroup from "@material-ui/core/FormGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Card from "@material-ui/core/Card";
import React, {useEffect, useState} from "react";
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import Select from "@material-ui/core/Select";
import MenuItem from "@material-ui/core/MenuItem";
import FormHelperText from "@material-ui/core/FormHelperText";
import makeStyles from "@material-ui/core/styles/makeStyles";

const useStyles = makeStyles((theme) => ({
    select: {
        minWidth: "200px",
        marginRight: theme.spacing(2)

    },
    formGroup: {
        marginBottom: theme.spacing(3),
    },
}));

const Preferences = ({className, onPreferencesChanged, errors, ...rest}) => {

    const classes = useStyles();

    const [preferences, setPreferences] = useState({
        allowEdits: false, acceptUpdates: true,
        priority: "normal",
        repeat: "none"
    });

    useEffect(() => {
        if (onPreferencesChanged != null) {
            onPreferencesChanged(preferences);
        }
    }, [preferences]);

    const handleChangeEvent = (event) => {
        const {name, value, type} = event.target;
        let isChecked = false;

        if (type === "checkbox")
            isChecked = event.target.checked;

        setPreferences((oldState) => ({
            ...oldState,
            [name]: type === "checkbox" ? isChecked : value
        }));
    };

    return (
        <Card {...rest} className={className}>
            <CardHeader title="Preferences"/>

            <CardContent>
                <div className={classes.formGroup}>
                    <FormControl error={errors["preferences.repeat"] != null}
                                 helperText={errors["preferences.repeat"]}
                                 className={classes.formControl}>
                        <InputLabel id="demo-simple-select-outlined-label">Repeat</InputLabel>
                        <Select
                            className={classes.select}
                            value={preferences.repeat}
                            onChange={handleChangeEvent}
                            name="Repeat"
                            label="Repeat"
                            required
                        >
                            <MenuItem value={"none"}>None</MenuItem>
                            <MenuItem value={"daily"}>Every Day</MenuItem>
                            <MenuItem value={"weekly"}>Every Week</MenuItem>
                            <MenuItem value={"monthly"}>Every Month</MenuItem>
                            <MenuItem value={"yearly"}>Every Year</MenuItem>
                        </Select>
                        <FormHelperText>Select the todo's repeat.</FormHelperText>
                    </FormControl>

                    <FormControl error={errors["preferences.priority"] != null}
                                 helperText={errors["preferences.priority"]}
                                 className={classes.formControl}>
                        <InputLabel id="demo-simple-select-outlined-label">Priority</InputLabel>
                        <Select
                            className={classes.select}
                            value={preferences.priority}
                            onChange={handleChangeEvent}
                            name="Priority"
                            label="Priority"
                            required
                        >
                            <MenuItem value={"normal"}>Low</MenuItem>
                            <MenuItem value={"medium"}>Medium</MenuItem>
                            <MenuItem value={"high"}>High</MenuItem>
                        </Select>
                        <FormHelperText>Select the todo's priority</FormHelperText>
                    </FormControl>
                </div>

                <FormGroup>
                    <FormControlLabel
                        control={
                            <Checkbox
                                error={errors["preferences.acceptUpdates"] != null}
                                helperText={errors["preferences.acceptUpdates"]}
                                checked={preferences.acceptUpdates}
                                onChange={handleChangeEvent}
                                name="acceptUpdates"
                                color="primary"
                            />
                        }
                        label="Get notified about any updates to this Todo"
                    />
                </FormGroup>

                <FormGroup>
                    <FormControlLabel
                        control={
                            <Checkbox
                                error={errors["preferences.allowEdits"] != null}
                                helperText={errors["preferences.allowEdits"]}
                                checked={preferences.allowEdits}
                                onChange={handleChangeEvent}
                                name="allowEdits"
                                color="primary"
                            />
                        }
                        label="Allow others to edit the Todo"
                    />
                </FormGroup>

            </CardContent>
        </Card>
    )
};


export default Preferences;