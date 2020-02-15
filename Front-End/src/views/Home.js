import React from "react";
import Todos from "../Components/Todo/Todos";
import Page from "../Components/Page";

export default function Home() {
    return(
        <Page
        title={"Home"}>
            <Todos />
        </Page>
    )
}