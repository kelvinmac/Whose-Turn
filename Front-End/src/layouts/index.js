import React from "react";
import MainMenu from "./MainMenu";
import { makeStyles } from '@material-ui/core/styles';


const useStyles = makeStyles(theme => ({
    container: {
        minHeight: '100vh',
        display: 'flex',
        '@media all and (-ms-high-contrast:none)': {
            height: 0 // IE11 fix
        }
    },
    content: {
        paddingTop: 34,
        flexGrow: 1,
        maxWidth: '100%',
        overflowX: 'hidden',
        width: '100%',
        paddingRight: 35,
        paddingLeft: 35,
        marginRight: 'auto',
        marginLeft: 'auto',
    }
}));


export default function MainLayout({children}){
    const classes = useStyles();

    return (
        <div>
            <MainMenu />
                <div className={classes.container}>
                    <div className={classes.content}>
                        {children}
                    </div>
                </div>
        </div>

    )
}