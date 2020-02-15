import React, {useEffect, useState} from "react";
import clsx from "clsx";
import makeStyles from "@material-ui/core/styles/makeStyles";
import CardHeader from "@material-ui/core/CardHeader";
import CardContent from "@material-ui/core/CardContent";
import Card from "@material-ui/core/Card";
import RichEditor from '../../Components/RichEditor'

const useStyles = makeStyles((theme) => ({
    root: {}
}));

const TodoDetails = ({className, onDetailsChanged, ...rest}) => {
    const classes = useStyles();
    const [details, setDetails] = useState("");

    const onEditorUpdate = (html) => {
        setDetails(html);
    };

    useEffect(() => {
        if (onDetailsChanged) {
            onDetailsChanged(details);
        }
    }, [details]);

    return (
        <Card
            className={clsx(classes.root, className)}
            {...rest}>
            <CardHeader title="More details about this todo"/>
            <CardContent>
                <RichEditor onHtmlUpdated={onEditorUpdate} placeholder="Give some more details about the todo..."/>
            </CardContent>
        </Card>
    )
};

export default TodoDetails;