import './badge.css'
import React from 'react';

export default function Badge(props) {
    return(
        <span className={"badge " + props.className} {...props.attr}>{props.content}</span>
    )
}