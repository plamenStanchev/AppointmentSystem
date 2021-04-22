import { useState } from "react";
import Login from "./login/Login";
import Register from "./register/Register";
import {
  Avatar,
  Button,
  Container,
  CssBaseline,
  FormControlLabel,
  Grid,
  Switch,
  Typography,
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import useStyles from "./Identity.styles";

const FormNames = {
  login: "Login",
  register: "Register",
};

interface Props {
  setToken(loginResponseModel: any): void;
}

const Identity = (props: Props) => {
  const classes = useStyles();

  const { setToken } = props;

  const [existAccount, setExistAccount] = useState(false);

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

  const getFormName = existAccount ? FormNames.register : FormNames.login;

  return (
    <Container component='main' maxWidth='xs'>
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component='h1' variant='h5'>
          {getFormName}
        </Typography>
        {existAccount ? (
          <Register
            setToken={setToken}
            classesForm={classes.form}
            button={getButtonForChildren()}
          />
        ) : (
          <Login
            setToken={setToken}
            classesForm={classes.form}
            button={getButtonForChildren()}
          />
        )}
        <Grid container justify='flex-end'>
          <Grid item>
            <FormControlLabel
              control={
                <Switch
                  size='small'
                  checked={existAccount}
                  onChange={() => setExistAccount(!existAccount)}
                />
              }
              color='primary'
              label="Don't have an account?"
            />
          </Grid>
        </Grid>
      </div>
    </Container>
  );
};

export default Identity;
