import React from 'react'
import '../Badge/badge.css'
import moment from 'moment';
import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import CardHeader from "@material-ui/core/CardHeader";
import Button from "@material-ui/core/Button";
import AddIcon from '@material-ui/icons/Add';
import { makeStyles } from '@material-ui/styles';
import Divider from "@material-ui/core/Divider";
import List from "@material-ui/core/List";
import ListItem from "@material-ui/core/ListItem";
import ListItemIcon from "@material-ui/core/ListItemIcon";
import Radio from "@material-ui/core/Radio";
import ListItemText from "@material-ui/core/ListItemText";
import Typography from "@material-ui/core/Typography";
import Tooltip from "@material-ui/core/Tooltip";
import IconButton from "@material-ui/core/IconButton";
import ArchiveIcon from '@material-ui/icons/Archive';
import Badge from "../Badge/Badge";
import red from "@material-ui/core/colors/red";
import orange from "@material-ui/core/colors/orange";
import grey from "@material-ui/core/colors/grey";
import clsx from 'clsx';
import {Link} from "react-router-dom";

const useStyles = makeStyles((theme) => ({
    root: {},
    content: {
        padding: 0
    },
    addIcon: {
        marginRight: "2px"
    },
    done: {
        textDecoration: 'line-through',
        color: grey[600]
    },
    danger:{
        backgroundColor: red[600],
        color: 'white'
    },
    warning:{
        backgroundColor: orange[600],
        color: 'whites'
    }
}));


export default function TodoItems(props){
    const classes = useStyles();

    const handleChange = (ev, todo) =>{
        ev.persist();
        props.onTodoChanged(todo);
    };

    const getLabel = (todo) => {
        if (todo.isCompleted) {
            return null;
        }

        if (moment(todo.dueOn).isBefore(moment(), 'day')) {
            return <Badge className={`badge--pill ${classes.danger}`}
                          content={`Due ${moment(todo.dueOn).fromNow()}`}/>
        }

        if (moment(todo.dueOn).isSame(moment(), 'day')) {
            return <Badge className={`badge--pill ${classes.warning}`}
                          content={`Due ${moment(todo.dueOn).fromNow()}`}/>
        }

        return  <Badge className={`badge--pill`}
                       content={`Due ${moment(todo.dueOn).fromNow()}`}/>
    };

    return(
        <Card>
            <CardHeader
                action={(
                    <Button
                        color="primary"
                        component={Link}
                        to={"/newItem"}
                        size="small"
                    >
                        <AddIcon className={classes.addIcon} />
                        Add
                    </Button>
                )}
                title="My todos"
            />
            <Divider />

            <CardContent className={classes.content}>
                <List>
                    {
                        props.todos.map((todo, i) => (
                            <ListItem
                                divider={i < props.todos.length - 1}
                                key={todo.id}
                            >
                                <ListItemIcon>
                                    <Radio
                                        checked={todo.isCompleted}
                                        onClick={(event) => handleChange(event, todo)}
                                    />
                                </ListItemIcon>
                                <ListItemText>
                                    <Typography
                                        className={clsx({
                                            [classes.done]: todo.isCompleted
                                        })}
                                        variant="body1">
                                        {todo.task}
                                    </Typography>
                                </ListItemText>
                                {getLabel(todo)}
                                <Tooltip title="Archive">
                                    <IconButton>
                                        <ArchiveIcon />
                                    </IconButton>
                                </Tooltip>
                            </ListItem>
                        ))
                    }
                </List>
            </CardContent>
        </Card>
    )
}