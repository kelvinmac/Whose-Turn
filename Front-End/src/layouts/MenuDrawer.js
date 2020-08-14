import React from 'react';
import {Link} from 'react-router-dom'
import {makeStyles} from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import CssBaseline from '@material-ui/core/CssBaseline';
import List from '@material-ui/core/List';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import TodoIcon from '@material-ui/icons/PlaylistAddCheckOutlined';
import AssignmentIcon from '@material-ui/icons/AssignmentOutlined';
import MyItemsIcon from '@material-ui/icons/AllInboxOutlined';
import HistoryIcon from '@material-ui/icons/History';
import HouseIcon from '@material-ui/icons/HouseOutlined';
import HomeIcon from '@material-ui/icons/Home'

const drawerWidth = 240;

const useStyles = makeStyles(theme => ({
    root: {
        display: 'flex',
    },
    appBar: {
        transition: theme.transitions.create(['margin', 'width'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
    },
    appBarShift: {
        width: `calc(100% - ${drawerWidth}px)`,
        marginLeft: drawerWidth,
        transition: theme.transitions.create(['margin', 'width'], {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
        }),
    },
    menuButton: {
        marginRight: theme.spacing(2),
    },
    hide: {
        display: 'none',
    },
    drawer: {
        width: drawerWidth,
        flexShrink: 0,
    },
    drawerPaper: {
        width: drawerWidth,
    },
    drawerHeader: {
        display: 'flex',
        alignItems: 'center',
        padding: theme.spacing(0, 1),
        ...theme.mixins.toolbar,
        justifyContent: 'flex-end',
    },
    content: {
        flexGrow: 1,
        padding: theme.spacing(3),
        transition: theme.transitions.create('margin', {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen,
        }),
        marginLeft: -drawerWidth,
    },
    contentShift: {
        transition: theme.transitions.create('margin', {
            easing: theme.transitions.easing.easeOut,
            duration: theme.transitions.duration.enteringScreen,
        }),
        marginLeft: 0,
    },
}));

export default function PersistentDrawerLeft(props) {
    const classes = useStyles();

    const handleDrawerClose = () => {
        props.setDrawerOpen(false);
    };

    const drawerItemClicked = (ev) => {
        // console.log(ev)
    };

    return (
        <div className={classes.root}>
            <CssBaseline/>
            <Drawer
                className={classes.drawer}
                anchor="left"
                open={props.drawerOpen}
                classes={{
                    paper: classes.drawerPaper,
                }}
                onClose={handleDrawerClose}
            >
                <div onClick={handleDrawerClose}
                     role="presentation">
                    <div className={classes.drawerHeader}>
                        <IconButton onClick={handleDrawerClose}>
                            <ChevronLeftIcon/>
                        </IconButton>
                    </div>
                    <Divider/>
                    <List>
                        <ListItem onClick={drawerItemClicked}
                                  component={Link}
                                  to="/"
                                  button key={"Home"}>
                            <ListItemIcon> <HomeIcon/> </ListItemIcon>
                            <ListItemText primary={"Home"}/>
                        </ListItem>

                        <ListItem onClick={drawerItemClicked}
                                  component={Link}
                                  to="/items/myitems"
                                  button key={"My-Turns"}>
                            <ListItemIcon> <MyItemsIcon/> </ListItemIcon>
                            <ListItemText primary={"My TodoItem/Tasks"}/>
                        </ListItem>
                    </List>

                    <Divider/>

                    <List>
                        <ListItem onClick={drawerItemClicked}
                                  component={Link}
                                  to="/todo/new"
                                  button key={"Add-Todo"}>
                            <ListItemIcon> <TodoIcon/> </ListItemIcon>
                            <ListItemText primary={"Create A Todo"}/>
                        </ListItem>

                        <ListItem onClick={drawerItemClicked}
                                  component={Link}
                                  to="/todo/new"
                                  button key={"Add-Todo"}>
                            <ListItemIcon> <AssignmentIcon/> </ListItemIcon>
                            <ListItemText primary={"Create A Task"}/>
                        </ListItem>

                        <ListItem onClick={drawerItemClicked}
                                  component={Link}
                                  to="/items/household"
                                  button key={"All-TodoItem"}>
                            <ListItemIcon> <HouseIcon/> </ListItemIcon>
                            <ListItemText primary={"Household TodoItem/Tasks"}/>
                        </ListItem>

                        <ListItem onClick={drawerItemClicked}
                                  component={Link}
                                  to="/history"
                                  button key={"Past-TodoItem"}>
                            <ListItemIcon> <HistoryIcon/> </ListItemIcon>
                            <ListItemText primary={"My history"}/>
                        </ListItem>
                    </List>
                </div>
            </Drawer>
        </div>
    );
}