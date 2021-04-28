import { Avatar, Grid, TextField, Typography } from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import { useForm } from "react-hook-form";
import useIdentity from "../hooks/useIdentity";
import registerFormConfig from "./Register.config";
import useStyles from "./Register.styles";

interface Props {
  setToken(loginResponseModel: any): void;
  button: any;
}

const Regsiter = (props: Props) => {
  const classes = useStyles();
  const { setToken, button } = props;
  const { register: registerForm, handleSubmit } = useForm();
  const { register } = useIdentity();

  const onSubmit = async (registerModel: any) => {
    const token = await register(registerModel);
    setToken(token);
  };

  return (
    <div className={classes.view}>
      <Avatar className={classes.avatar}>
        <LockOutlinedIcon />
      </Avatar>
      <Typography component='h1' variant='h5'>
        Register
      </Typography>
      <form
        className={classes.form}
        noValidate
        onSubmit={handleSubmit(onSubmit)}>
        <Grid container spacing={2}>
          {registerFormConfig.map((field) => (
            <Grid key={field.id} item xs={12}>
              <TextField
                {...registerForm(field.id)}
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
    </div>
  );
};

export default Regsiter;
