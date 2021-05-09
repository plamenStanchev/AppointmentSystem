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
  MenuItem,
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import { Controller, useForm } from "react-hook-form";

import useDepartment from "../../../../shared/hooks/useDepartment";
import useCity from "../../../../shared/hooks/useCity";
import useAccountDetails from "../../hooks/useAccountDetails";

import useDoctorConfig from "./useDoctorConfig";

import useStyles from "./Doctor.styles";
import useDoctor from "../../hooks/useDoctor";

interface OptionsModel {
  name: string;
  id: number;
}
interface ICreateDoctorModel {
  firstName: string;
  secondName: string;
  surName: string;
  pin: string;
  description: string;
  cityId: string;
  departmentId: string;
  accountId: string;
}

interface Props {}

const Doctor = (props: Props) => {
  const classes = useStyles();

  const {
    register: registerForm,
    handleSubmit,
    control,
  } = useForm<ICreateDoctorModel>();

  const { doctorFormConfig } = useDoctorConfig();

  const { accountId } = useAccountDetails();
  const { getAllDepartments } = useDepartment();
  const { getAllCities } = useCity();
  const { createDoctor } = useDoctor();

  const [departments, setDepartments] = useState<OptionsModel[]>([]);
  const [cities, setCities] = useState<OptionsModel[]>([]);

  useEffect(() => {
    (async () => {
      const responseDepartments = (await getAllDepartments()) as OptionsModel[];
      setDepartments(responseDepartments);

      const responseCities = (await getAllCities()) as OptionsModel[];
      setCities(responseCities);
    })();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const onSubmit = async (registerModel: ICreateDoctorModel) => {
    if (accountId) {
      registerModel.accountId = accountId;

      var response = await createDoctor(registerModel);
    }
    console.log("log");
  };

  const renderFormFields = (
    <Grid container spacing={2}>
      {doctorFormConfig.map((field) => (
        <Grid key={field.id} item md={12} xs={12}>
          <TextField
            {...registerForm(field.name as keyof ICreateDoctorModel)}
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
      <Grid key={"department"} item md={6} xs={12}>
        <FormControl variant='outlined' className={classes.formControl}>
          <InputLabel required htmlFor='outlined-city'>
            Department
          </InputLabel>
          <Controller
            name='departmentId'
            control={control}
            render={({ field }) => {
              const { onChange, value } = field;
              return (
                <Select
                  value={value}
                  onChange={onChange}
                  required
                  label='Department'>
                  <MenuItem aria-label='None' value='' />
                  {departments.map((d) => (
                    <MenuItem key={d.id} value={d.id}>
                      {d.name}
                    </MenuItem>
                  ))}
                </Select>
              );
            }}
            defaultValue=''
          />
        </FormControl>
      </Grid>
      <Grid key={"city"} item md={6} xs={12}>
        <FormControl variant='outlined' className={classes.formControl}>
          <InputLabel required htmlFor='outlined-department'>
            City
          </InputLabel>
          <Controller
            name='cityId'
            control={control}
            render={({ field }) => {
              const { onChange, value } = field;
              return (
                <Select value={value} onChange={onChange} required label='City'>
                  <MenuItem aria-label='None' value='' />
                  {cities.map((d) => (
                    <MenuItem key={d.id} value={d.id}>
                      {d.name}
                    </MenuItem>
                  ))}
                </Select>
              );
            }}
            defaultValue=''
          />
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
        <Grid container spacing={2}>
          <Grid item md={5} xs={5} />
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
          <Grid item md={5} xs={5} />
        </Grid>
      </form>
    </Container>
  );
};

export default Doctor;
