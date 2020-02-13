import React, {useState} from "react";
import Alert from "@material-ui/lab/Alert";
import {connect} from "react-redux";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from '@material-ui/icons/Close';
import Collapse from "@material-ui/core/Collapse";
import {updateAlert} from "../../actions/errorActions";
import uuid from 'react-uuid'

function AlertError(props) {

    return (
        props.alerts.map((alert) => {
            return (
                <Collapse key={uuid()} in={alert.show}>
                    <Alert
                        action={
                            <IconButton
                                aria-label="close"
                                color="inherit"
                                size="small"
                                onClick={() => {
                                   props.updateAlert({remove: true, id: alert.id});
                                }}
                            >
                                <CloseIcon fontSize="inherit"/>
                            </IconButton>
                        }
                        key={alert.id}
                        variant={alert.variant == null ? "outlined" : alert.variant}
                        severity={alert.severity == null ? "warning" : alert.severity}>
                        {alert.message == null ? "Oops, something isn't right" : alert.message}
                    </Alert>
                </Collapse>
            )
        })
    )
}

const matchDispatchToProps = {
    updateAlert
};

const mapStateToProps = (state) => {
    return({
          alerts:  state.errors.alert
        }
    )
};

export default connect(
    mapStateToProps,
    matchDispatchToProps
)(AlertError)