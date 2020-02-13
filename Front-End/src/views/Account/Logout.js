import React from "react";
import {logout} from "../../actions/authenticationActions";
import {connect} from "react-redux";
import {Redirect} from "react-router-dom";

const Logout = (props) => {

    props.logout();
    return(
        <Redirect to='login'/>
    );
};

const matchDispatchToProps = {
    logout
};

const mapStateToProps = (state) => {};

export default connect(
    mapStateToProps,
    matchDispatchToProps,
)(Logout)