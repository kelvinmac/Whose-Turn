/* eslint-disable no-undef */
import React, {useEffect} from 'react';
import makeStyles from "@material-ui/core/styles/makeStyles";
import {Helmet} from 'react-helmet'
import PropTypes from 'prop-types';

const {NODE_ENV, REACT_APP_GA_MEASUREMENT_ID: GA_MEASUREMENT_ID} = process.env;

const useStyles = makeStyles((theme) => ({
        root: {
            paddingBottom: theme.spacing(3)
        }
    }));

function Page({title, children, location, ...rest}) {

    const classes = useStyles();
    useEffect(() => {
        if (NODE_ENV !== 'production') {
            return;
        }

        // eslint-disable-next-line
    }, []);

    return (
        <div
            className={classes.root}
            {...rest}>
            <Helmet>
                <title>{title}</title>
            </Helmet>
            {children}
        </div>
    );
}

Page.propTypes = {
    children: PropTypes.node,
    title: PropTypes.string
};

export default Page;
