import { Grid, TextField } from "@material-ui/core";
import registerFormConfig from "./Register.config";
import { useForm } from "react-hook-form";
import useIdentity from "../hooks/useIdentity";

interface Props {
  setToken(loginResponseModel: any): void;
  classesForm: string;
  button: any;
}

const Regsiter = (props: Props) => {
  const { setToken, classesForm, button } = props;
  const { register: registerForm, handleSubmit } = useForm();
  const { register } = useIdentity();

  const onSubmit = async (registerModel: any) => {
    const token = await register(registerModel);
    setToken(token);
  };

  return (
    <>
      <form
        className={classesForm}
        noValidate
        onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={2}>
          {registerFormConfig.map((f) => (
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

export default Regsiter;
