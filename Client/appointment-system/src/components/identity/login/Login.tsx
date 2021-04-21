import { Grid, TextField } from "@material-ui/core";
import IdentityService from "../services/IdentityService";
import loginFormConfig from "./Login.config";
import { useForm } from "react-hook-form";

interface Props {
  setToken(loginResponseModel: any): void;
  classesForm: string;
  button: any;
}

const Login = (props: Props) => {
  const { setToken, classesForm, button } = props;

  const { register, handleSubmit } = useForm();

  const onSubmit = async (loginModel: LoginRequestModel) => {
    const token = await IdentityService.login(loginModel);
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
                {...register(f.id)}
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
