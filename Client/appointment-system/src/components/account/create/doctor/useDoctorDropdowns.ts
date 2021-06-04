import { useEffect, useState } from "react";

import useDepartment from "../../../../shared/hooks/useDepartment";
import useCity from "../../../../shared/hooks/useCity";
import useDoctorConfig from "./useDoctorConfig";

interface OptionsModel {
  name: string;
  id: number;
}

const useDoctorDropdowns = () => {
  const { doctorDropdownConfig } = useDoctorConfig();

  const [departments, setDepartments] = useState<OptionsModel[]>([]);
  const [cities, setCities] = useState<OptionsModel[]>([]);

  const { getAllDepartments } = useDepartment();
  const { getAllCities } = useCity();

  const getValues = () => {
    const stateArray = [departments, cities];

    doctorDropdownConfig.forEach(
      (x: any, i: number) => (x.state = () => stateArray[i])
    );
  };

  useEffect(() => {
    (async () => {
      const responseDepartments = (await getAllDepartments()) as OptionsModel[];
      setDepartments(responseDepartments);

      const responseCities = (await getAllCities()) as OptionsModel[];
      setCities(responseCities);

      getValues();
    })();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return { doctorDropdownConfig };
};
export default useDoctorDropdowns;
