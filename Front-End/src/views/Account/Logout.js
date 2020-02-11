import {authenticateUser} from "../../actions/authenticationActions";
import {connect} from "react-redux";

function Logout(props){

}

const matchDispatchToProps = {
    authenticateUser
};

const mapStateToProps = (state) => {};

export default connect(
    mapStateToProps,
    matchDispatchToProps,
)(Logout)