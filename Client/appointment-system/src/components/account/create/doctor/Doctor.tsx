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

import doctorFormConfig from "./Doctor.config";
import useStyles from "./Doctor.styles";
import useWindowDimensions from "../../../../shared/hooks/useWindowDimensions";

interface OptionsModel {
  name: string;
  id: number;
}

interface CreateDoctorModel {
  firstName: string;
  secondName: string;
  surName: string;
  pin: string;
  description: string;
  cityId: number;
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
  } = useForm<CreateDoctorModel>();
  const { getAllDepartments } = useDepartment();
  const { getAllCities } = useCity();
  const { width } = useWindowDimensions();

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

  const onSubmit = async (registerModel: any) => {
    console.log("log");
  };

  const renderFormFields = (
    <Grid key={"t1"} container spacing={2}>
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

        const isHidden = width < 960;

        return lastComponentIsInTheMiddle() ? (
          <>
            <Grid key='temp1' item md={3} hidden={isHidden} />
            {fieldComponent}
            <Grid key='temp2' item md={3} hidden={isHidden} />
          </>
        ) : (
          fieldComponent
        );
      })}
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
        <Grid key={"t2"} container spacing={2}>
          <Grid key='temp11' item md={5} xs={5} />
          <Grid key='temp12' item md={2} xs={2}>
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
          <Grid key='temp13' item md={5} xs={5} />
        </Grid>
      </form>
    </Container>
  );
};

export default Doctor;
