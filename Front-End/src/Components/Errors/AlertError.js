import React from "react";
import Alert from "@material-ui/lab/Alert";
import {Box} from "@material-ui/core";
import {connect} from "react-redux";

function AlertError(props) {
    return (
        props.alerts.map((alert) =>{
            return(
                <Box display={alert.show ? "inherit" : "none"}>
                    <Alert
                        variant={alert.variant == null ? "outlined" : alert.variant}
                        severity={alert.severity == null ? "warning" : alert.severity}>
                        {alert.message == null ? "Oops, something isn't right" : alert.message}
                    </Alert>
                </Box>
            )
        })
    )
}

const mapStateToProps = (state) => {
    return({
          alerts:  state.errors.alert
        }
    )
};

export default connect(
    mapStateToProps
)(AlertError)