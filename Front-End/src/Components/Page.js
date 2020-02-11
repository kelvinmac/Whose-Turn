/* eslint-disable no-undef */
import React, { useEffect } from 'react';
import { Helmet } from 'react-helmet'
import PropTypes from 'prop-types';

const { NODE_ENV, REACT_APP_GA_MEASUREMENT_ID: GA_MEASUREMENT_ID } = process.env;

function Page({ title, children, location, ...rest }) {

    useEffect(() => {
        if (NODE_ENV !== 'production') {
            return;
        }

        // eslint-disable-next-line
    }, []);

    return (
        <div {...rest}>
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
