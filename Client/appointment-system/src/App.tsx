import React from "react";
import {
  BrowserRouter as Router,
  Route,
  Switch,
  Redirect,
} from "react-router-dom";
import { Container } from "@material-ui/core";

import NavBar from "./components/navbar/NavBar";
import Login from "./components/identity/Login";
import useToken from "./components/identity/hooks/useToken";

const App: React.FC = () => {
  const { token, setToken } = useToken();

  if (!token) {
    return <Login setToken={setToken} />;
  }

  return (
    <Container>
      <NavBar />
      <Router>
        <Switch>
          <Route path='/'>
            <a href='/'>awdawd</a>
          </Route>
          <Route path='/2'>
            <a href='/'>awdawd</a>
          </Route>
        </Switch>
      </Router>
    </Container>
  );
};

export default App;
