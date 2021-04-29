import React, { useEffect, useState } from "react";

import {
  Grid,
  Avatar,
  Button,
  Select,
  Container,
  TextField,
  InputLabel,
  Typography,
  FormControl,
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import { useForm } from "react-hook-form";

import useDepartment from "../../../../shared/hooks/useDepartment";

import doctorFormConfig from "./Doctor.config";
import useStyles from "./Doctor.styles";

interface Props {}

interface CreateDoctorModel {
  firstName: string;
  secondName: string;
  surName: string;
  pin: string;
  description: string;
  cityId: number;
  departmentId: number;
  accountId: string;
}

const Doctor = (props: Props) => {
  const classes = useStyles();
  const { register: registerForm, handleSubmit } = useForm<CreateDoctorModel>();
  const { getDepartment } = useDepartment();

  let departments;

  //
  const [state, setState] = React.useState<{
    city: string | number;
    name: string;
    department: string;
  }>({
    city: "",
    name: "hai",
    department: "",
  });

  useEffect(() => {});
  useEffect(() => {
    (async () => {
      departments = await getDepartment();
    })();
  });

  const handleChange = (
    event: React.ChangeEvent<{ name?: string; value: unknown }>
  ) => {
    const name = event.target.name as keyof typeof state;
    setState({
      ...state,
      [name]: event.target.value,
    });
  };
  //

  const onSubmit = async (registerModel: any) => {
    console.log("log");
  };

  const renderFormFields = (
    <Grid container spacing={2}>
      {doctorFormConfig.map((field, i) => {
        const fieldComponent = (
          <Grid key={field.id} item md={6} xs={12}>
            <TextField
              {...registerForm(field.name as keyof CreateDoctorModel)}
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
        );

        const lastComponentIsInTheMiddle = () => {
          const isLast = i === doctorFormConfig.length - 1;
          const isEven = i % 2 === 0;
          return isEven && isLast;
        };

        return lastComponentIsInTheMiddle() ? (
          <>
            <Grid md={3} />
            {fieldComponent}
            <Grid md={3} />
          </>
        ) : (
          fieldComponent
        );
      })}
      <Grid item md={6} xs={12}>
        <FormControl variant='outlined' className={classes.formControl}>
          <InputLabel required htmlFor='outlined-city'>
            City
          </InputLabel>
          <Select
            native
            value={state.city}
            onChange={handleChange}
            label='City'
            required
            inputProps={{
              name: "city",
              id: "outlined-city",
            }}>
            <option aria-label='None' value='' />
            <option value={10}>Burgas</option>
            <option value={20}>Varna</option>
            <option value={30}>Sofia</option>
          </Select>
        </FormControl>
      </Grid>
      <Grid item md={6} xs={12}>
        <FormControl variant='outlined' className={classes.formControl}>
          <InputLabel required htmlFor='outlined-department'>
            Department
          </InputLabel>
          <Select
            native
            value={state.city}
            onChange={handleChange}
            label='Department'
            required
            inputProps={{
              name: "department",
              id: "outlined-city",
            }}>
            <option aria-label='None' value='' />
            <option value={10}>Burgas</option>
            <option value={20}>Varna</option>
            <option value={30}>Sofia</option>
          </Select>
        </FormControl>
      </Grid>
    </Grid>
  );

  return (
    <Container maxWidth='sm'>
      <Avatar className={classes.avatar}>
        <LockOutlinedIcon />
      </Avatar>
      <Typography component='h1' variant='h5'>
        Be a doctor
      </Typography>
      <form
        className={classes.form}
        noValidate
        onSubmit={handleSubmit(onSubmit)}>
        {renderFormFields}
        <Grid container spacing={1}>
          <Grid md={5} xs={5} />
          <Grid item md={2} xs={2}>
            <Button
              className={classes.submit}
              type='submit'
              variant='contained'
              color='primary'
              size='large'
              fullWidth>
              Submit
            </Button>
          </Grid>
          <Grid md={5} xs={5} />
        </Grid>
      </form>
    </Container>
  );
};

export default Doctor;
