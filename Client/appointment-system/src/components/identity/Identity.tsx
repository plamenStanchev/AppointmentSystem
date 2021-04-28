import { useState } from "react";
import {
  Button,
  Container,
  CssBaseline,
  FormControlLabel,
  Grid,
  Switch,
} from "@material-ui/core";

import Login from "./login/Login";
import Register from "./register/Register";

import useStyles from "./Identity.styles";
import useIdentityVisual from "./Identity.visual";

const FormNames = {
  login: "Login",
  register: "Register",
};

interface Props {
  setToken(loginResponseModel: any): void;
}

const Identity = (props: Props) => {
  const classes = useStyles();
  const { loadPaper, openRegister, openLogin } = useIdentityVisual();

  const { setToken } = props;

  const [existAccount, setExistAccount] = useState(true);

  const changeForm = () => {
    loadPaper();
    if (existAccount) {
      openRegister();
    } else {
      openLogin();
    }
    setTimeout(() => setExistAccount(!existAccount), 250);
  };

  const getButtonForChildren = () => (
    <Button
      type='submit'
      fullWidth
      variant='contained'
      color='primary'
      className={classes.submit}>
      {getFormName}
    </Button>
  );

  const getFormName = existAccount ? FormNames.login : FormNames.register;

  return (
    <Container component='main' maxWidth='xs'>
      <CssBaseline />
      <div className={classes.paper} id='paper'>
        {existAccount ? (
          <Login setToken={setToken} button={getButtonForChildren()} />
        ) : (
          <Register setToken={setToken} button={getButtonForChildren()} />
        )}
      </div>
      <Grid container justify='flex-end'>
        <Grid item>
          <FormControlLabel
            control={
              <Switch
                size='small'
                checked={existAccount}
                onChange={changeForm}
              />
            }
            color='primary'
            label="Don't have an account?"
          />
        </Grid>
      </Grid>
    </Container>
  );
};

export default Identity;
