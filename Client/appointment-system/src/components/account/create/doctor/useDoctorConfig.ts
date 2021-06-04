interface DropdownModel {
  id: string;
  htmlFor: string;
  name: string;
  label: string;
  state: () => OptionsModel[];
}
interface OptionsModel {
  name: string;
  id: number;
}

const useDoctorConfig = () => {
  const doctorFormConfig = [
    {
      id: "firstName",
      label: "First Name",
      name: "firstName",
      autoComplete: "first-name",
      type: "text",
    },
    {
      id: "secondName",
      label: "Second Name",
      name: "secondName",
      autoComplete: "second-name",
      type: "text",
    },
    {
      id: "surName",
      label: "Sur Name",
      name: "surName",
      autoComplete: "sur-name",
      type: "text",
    },
    {
      id: "pin",
      label: "PIN",
      name: "pin",
      autoComplete: "pin",
      type: "text",
    },
    {
      id: "description",
      label: "Description",
      name: "description",
      autoComplete: "description",
      type: "text",
    },
  ];

  const doctorDropdownConfig: DropdownModel[] = [
    {
      id: "department",
      htmlFor: "outlined-department",
      name: "departmentId",
      label: "Department",
      state: () => [],
    },
    {
      id: "city",
      htmlFor: "outlined-city",
      name: "cityId",
      label: "City",
      state: () => [],
    },
  ];

  return { doctorFormConfig, doctorDropdownConfig };
};

export default useDoctorConfig;
