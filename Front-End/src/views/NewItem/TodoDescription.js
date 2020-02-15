import React, {useEffect, useState} from "react";
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardHeader from "@material-ui/core/CardHeader";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import Input from '@material-ui/core/Input';
import FormControl from "@material-ui/core/FormControl";
import InputLabel from "@material-ui/core/InputLabel";
import clsx from "clsx";
import Alert from "@material-ui/lab/Alert";
import TextField from "@material-ui/core/TextField";
import uuid from 'react-uuid'
import Checkbox from "@material-ui/core/Checkbox";
import ListItemText from "@material-ui/core/ListItemText";
import FormHelperText from "@material-ui/core/FormHelperText";
import axios from "axios";
import {connect} from "react-redux";
import {updateCritical} from "../../actions/errorActions";
import Chip from "@material-ui/core/Chip";
import {DateTimePicker} from "@material-ui/pickers";
import moment from "moment";

const useStyles = makeStyles((theme) => ({
    root: {},
    alert: {
        marginBottom: theme.spacing(3)
    },
    chips: {
        display: 'flex',
        flexWrap: 'wrap',
    },
    chip: {
        margin: 2,
    },
    selectEmpty: {
        marginTop: theme.spacing(2),
    },
    formGroup: {
        marginBottom: theme.spacing(3)
    },
    fieldGroup: {
        display: 'flex',
        alignItems: 'center'
    },
    select: {
        minWidth: "200px"
    },
    dateField: {
        '& + &': {
            marginLeft: theme.spacing(2)
        }
    }
}));

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
    PaperProps: {
        style: {
            maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
            width: 250,
        },
    },
};

const TodoDescription = ({updateCritical, onDescriptionChanged, className, isPersonal, ...rest}) => {
    const classes = useStyles();

    const [description, setDescription] = useState({
        members: [],
        startDate: moment(),
        endDate: moment().add(1, 'day')
    });

    const [calendarTrigger, setCalendarTrigger] = useState(null);
    const [houseMembers, setHouseMembers] = useState([]);
    const calendarOpen = Boolean(calendarTrigger);

    // Initially created
    useEffect(() => {
        axios.get(`${process.env.REACT_APP_API_URI}houseHold/Mine`)
            .then(result => {
                setHouseMembers(result.data);
            })
            .catch(error => {
                updateCritical({
                    title: "Connection Error",
                    message: "There was an error retrieving house members, try again shortly"
                })
            });
    }, []);

    // Description updated
    useEffect(() => {
        if (onDescriptionChanged != null) {
            onDescriptionChanged(description);
        }
    }, [description]);

    const handleChangeEvent = (event) => {
        const {name, value} = event.target;

        setDescription((oldState) => ({
            ...oldState,
            [name]: value
        }));
    };

    const handleMemberAdded = (event) => {
        const {value} = event.target;

        setDescription((oldState) => {
            return ({
                ...oldState,
                members: [...value]
            })
        });
    };

    const handleCalendarOpen = (trigger) => {
        setCalendarTrigger(trigger);
    };

    const handleCalenderClosed = () => {
        setCalendarTrigger(false);
    };

    const handleCalenderAccept = (date) => {
        setDescription((oldState) => ({
            ...oldState,
            [calendarTrigger]: date
        }))
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
                    Once the description is set, it cannot be changed
                </Alert>

                {!isPersonal &&
                <div className={classes.formGroup}>
                    <FormControl>
                        <InputLabel>Household members</InputLabel>
                        <Select
                            multiple
                            className={classes.select}
                            value={description.members}
                            onChange={handleMemberAdded}
                            input={<Input/>}
                            renderValue={
                                selected => (
                                    <div className={classes.chips}>
                                        {selected.map(value => (
                                            <Chip key={value} label={value} className={classes.chip}/>
                                        ))}
                                    </div>
                                )
                            }
                            MenuProps={MenuProps}
                        >
                            {houseMembers.map(member => {
                                const name = `${member.firstName} ${member.lastName}`;

                                return (
                                    <MenuItem key={uuid()} value={name}>
                                        <Checkbox checked={description.members.includes(name)}/>
                                        <ListItemText primary={name}/>
                                    </MenuItem>
                                )
                            })}
                        </Select>
                        <FormHelperText>Select house members to assign task</FormHelperText>
                    </FormControl>
                </div>}

                <div className={classes.formGroup}>
                    <div className={classes.fieldGroup}>
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
                </div>

                <div className={classes.formGroup}>
                    <div className={classes.fieldGroup}>
                        <TextField
                            className={classes.dateField}
                            label="Start Date"
                            name="startDate"
                            onClick={() => handleCalendarOpen('startDate')}
                            value={moment(description.startDate).format('DD/MM/YYYY hh:mm')}
                            variant="outlined"
                        />
                        <TextField
                            className={classes.dateField}
                            label="End Date"
                            name="endDate"
                            onClick={() => handleCalendarOpen('endDate')}
                            value={moment(description.endDate).format('DD/MM/YYYY hh:mm')}
                            variant="outlined"
                        />
                    </div>
                </div>
            </CardContent>
            <DateTimePicker
                minDate={moment()}
                label="DateTimePicker"
                style={{display: 'none'}} // Hide the input element
                open={calendarOpen}
                onChange={() => {
                }}
                onClose={handleCalenderClosed}
                onAccept={handleCalenderAccept}
                inputVariant="outlined"
                variant="dialog"
            />
        </Card>
    )
};

const matchDispatchToProps = {
    updateCritical
};

const mapStateToProps = (state) => {
    return ({
            user: state.user.profile
        }
    )
};

export default connect(
    mapStateToProps,
    matchDispatchToProps,
)(TodoDescription)