import React from "react";
import makeStyles from "@material-ui/core/styles/makeStyles";
import Backdrop from "@material-ui/core/Backdrop";
import CircularProgress from "@material-ui/core/CircularProgress";
import {connect} from "react-redux";

const useStyles = makeStyles(theme => ({
    backdrop: {
        zIndex: theme.zIndex.drawer + 1,
        color: '#f00',
    },
}));

function FullScreenLoading(props) {
    const classes = useStyles();
    return (
        <div>
            {props.children}
            <Backdrop className={classes.backdrop} transitionDuration={{enter: 1000}} open={props.backdrop.show}>
                <CircularProgress color="inherit"/>
            </Backdrop>
        </div>
    )
}

const mapStateToProps = (state) => {
    return({
            backdrop: state.loading.backdrop
        }
    )
};

export default connect(
    mapStateToProps,
)(FullScreenLoading)