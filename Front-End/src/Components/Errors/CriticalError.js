import React from "react";
import {connect} from "react-redux";
import {updateCritical} from '../../actions/errorActions'
import Dialog from "@material-ui/core/Dialog";
import DialogTitle from "@material-ui/core/DialogTitle";
import DialogContent from "@material-ui/core/DialogContent";
import DialogContentText from "@material-ui/core/DialogContentText";
import DialogActions from "@material-ui/core/DialogActions";
import Button from "@material-ui/core/Button";
import Enumerable from "linq";
import uuid from 'react-uuid';

function CriticalError(props) {
    const handleClose = (event, id) => {

        if (Enumerable.from(props.errors)
            .firstOrDefault(e => e.id === id) === null) {
            return;
        }

        props.updateCritical({show: false, id});
    };

    return (

        props.errors.map((error) => {
            return (
                <Dialog
                    key={error.id}
                    open={error.show}
                    aria-labelledby="alert-dialog-title"
                    aria-describedby="alert-dialog-description">
                    <DialogTitle key={uuid()} id="alert-dialog-title">
                        {error.title || "Something isn't right"}
                    </DialogTitle>
                    <DialogContent key={uuid()}>
                        <DialogContentText key={uuid()} id="alert-dialog-description">
                            {error.message}
                        </DialogContentText>
                    </DialogContent>
                    <DialogActions key={uuid()}>
                        {error.actions == null &&
                        <Button key={uuid()}
                                onClick={(event => {
                                    handleClose(event, error.id)
                                })} color="primary">
                            Ok
                        </Button>}

                        {error.actions != null && error.actions.map((a) => {
                            const {actionText, clickHandler} = a;

                            return (
                                <Button key={actionText} onClick={(event => {
                                    handleClose(event, error.id);
                                    return clickHandler(event, error.id);
                                })} color="primary">
                                    {actionText}
                                </Button>)
                        })}
                    </DialogActions>
                </Dialog>
            )
        })
    )
}

const matchDispatchToProps = {
    updateCritical
};

const mapStateToProps = (state) => {
    return({
           errors: state.errors.critical
        }
    )
};

export default connect(
    mapStateToProps,
    matchDispatchToProps
)(CriticalError)