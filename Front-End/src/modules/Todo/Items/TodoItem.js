import React, {useEffect} from "react";
import {Container} from "@material-ui/core";

export default function TodoItem({todoData, ...rest}) {
    const [todoData, setTodoData] = React.useState(null);

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
        }
        ));
    };

    return (
        <Container  maxWidth="md">
            {todoData != null && <TodoItem onTodoChanged={todoChanged} todos={todoData}/>}
        </Container>
    )
}