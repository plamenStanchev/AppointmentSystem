import { Avatar, Grid, TextField, Typography } from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import { useForm } from "react-hook-form";
import useIdentity from "../hooks/useIdentity";
import loginFormConfig from "./Login.config";
import useStyles from "./Login.styles";

interface LoginModel {
  email: string;
  password: string;
}

interface Props {
  setToken(loginResponseModel: any): void;
  button: any;
}

const Login = (props: Props) => {
  const classes = useStyles();
  const { setToken, button } = props;
  const { register: registerForm, handleSubmit } = useForm<LoginModel>();
  const { login } = useIdentity();

  const onSubmit = async (loginModel: LoginRequestModel) => {
    const token = await login(loginModel);
    setToken(token);
  };
  return (
    <>
      <Avatar className={classes.avatar}>
        <LockOutlinedIcon />
      </Avatar>
      <Typography component='h1' variant='h5'>
        Login
      </Typography>
      <form
        className={classes.form}
        noValidate
        onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={2}>
          {loginFormConfig.map((field) => (
            <Grid key={field.id} item xs={12}>
              <TextField
                {...registerForm(field.id as keyof LoginModel)}
                variant='outlined'
                required
                fullWidth
                id={field.id}
                label={field.label}
                name={field.name}
                autoComplete={field.autoComplete}
                type={field.type}
              />
            </Grid>
          ))}
        </Grid>
        {button}
      </form>
    </>
  );
};

export default Login;

interface LoginRequestModel {
  email: string;
  password: string;
}
