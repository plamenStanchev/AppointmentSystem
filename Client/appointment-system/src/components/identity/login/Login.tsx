import { Grid, TextField } from "@material-ui/core";
import useIdentity from "../hooks/useIdentity";
import loginFormConfig from "./Login.config";
import { useForm } from "react-hook-form";

interface Props {
  setToken(loginResponseModel: any): void;
  classesForm: string;
  button: any;
}

const Login = (props: Props) => {
  const { setToken, classesForm, button } = props;

  const { register: registerForm, handleSubmit } = useForm();
  const { login } = useIdentity();

  const onSubmit = async (loginModel: LoginRequestModel) => {
    const token = await login(loginModel);
    setToken(token);
  };
  return (
    <>
      <form
        className={classesForm}
        noValidate
        onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={2}>
          {loginFormConfig.map((f) => (
            <Grid key={f.id} item xs={12}>
              <TextField
                {...registerForm(f.id)}
                variant='outlined'
                required
                fullWidth
                id={f.id}
                label={f.label}
                name={f.name}
                autoComplete={f.autoComplete}
                type={f.type}
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
