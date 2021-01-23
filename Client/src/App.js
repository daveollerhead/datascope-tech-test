import React, { Component, Fragment } from "react";
import { Route, Redirect, Switch } from "react-router-dom";
import Container from "react-bootstrap/Container";
import Games from "./components/games";
import GameForm from "./components/gameForm";
import NotFound from "./components/notFound";
import Navbar from "./components/navBar";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

function App() {
  return (
    <Fragment>
      <ToastContainer />
      <Navbar />
      <Container>
        <main>
          <Switch>
            <Route path="/game/:id" component={GameForm}></Route>
            <Route path="/game" component={GameForm}></Route>
            <Route path="/games" component={Games}></Route>
            <Route path="/not-found" component={NotFound}></Route>
            <Redirect from="/" exact to="/games" />
            <Redirect to="/not-found" />
          </Switch>
        </main>
      </Container>
    </Fragment>
  );
}

export default App;
