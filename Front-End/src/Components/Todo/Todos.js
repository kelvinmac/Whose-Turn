import React, {useEffect} from "react";
import axios from 'axios';
import TodoItems from './TodoItem'
import Button from "@material-ui/core/Button";
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import {Container} from "@material-ui/core";

export default function Todos(props) {
    const [todoData, setTodoData] = React.useState(null);
    const [openDialog, setOpenDialog] = React.useState(false);

    const handleClose = () => {
        setOpenDialog(false);
    };

    useEffect(() => {
        axios.get(`${process.env.REACT_APP_API_URI}todos/2`)
            .then(result => {
                setTodoData(result.data);
            })
            .catch(error => {
                setOpenDialog(true);
            });
    }, []);

    const todoChanged =(todo) => {
        setTodoData((prevTodos) => prevTodos.map((prevTodo) => {
            if (prevTodo.id === todo.id) {
                return {
                    ...todo,
                    isCompleted: !todo.isCompleted
                };
            }
            else{
                return null;
            }
        }));
    };

    return (
        <Container  maxWidth="md">
            <Dialog
                open={openDialog}
                aria-labelledby="alert-dialog-title"
                aria-describedby="alert-dialog-description">
                <DialogTitle id="alert-dialog-title">{"Connection error"}</DialogTitle>
                <DialogContent>
                    <DialogContentText id="alert-dialog-description">
                        There was an error while retrieving data
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Ok
                    </Button>
                </DialogActions>
            </Dialog>

            {todoData != null && <TodoItems onTodoChanged={todoChanged} todos={todoData}/>}
        </Container>
    )
}