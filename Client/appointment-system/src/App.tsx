import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import { Container, ThemeProvider } from "@material-ui/core";
import Account from "./components/account/Account";

import NavBar from "./components/navbar/NavBar";
import Identity from "./components/identity/Identity";

import useToken from "./components/identity/hooks/useToken";

import theme from "./theme/theme";

const App: React.FC = () => {
  const { token, setToken } = useToken();

  if (!token) {
    return <Identity setToken={setToken} />;
  }

  return (
    <ThemeProvider theme={theme}>
      <Container>
        <>
          <Router>
            <NavBar />
            <Switch>
              <Route path='/account' exact>
                <Account />
              </Route>
            </Switch>
          </Router>
        </>
      </Container>
    </ThemeProvider>
  );
};

export default App;
