import useApi from "../../../shared/hooks/useApi";

interface CreateDoctorModel {
  firstName: string;
  secondName: string;
  surName: string;
  pin: string;
  cityId: number;
  departmentId: number;
  description: string;
  accountId: string;
}

const baseUrl = "/doctors";

const useDoctor = () => {
  const { get, post } = useApi();

  const createDoctor = async (model: CreateDoctorModel) => {
    try {
      let doctorModel;
      const doctorResponse = await post(`${baseUrl}/create`, model);

      if (!doctorResponse) {
        throw new Error("Unknown error");
      }

      doctorModel = doctorResponse.data;

      return doctorModel;
    } catch (error) {
      throw error;
    }
  };

  const getDoctor = async (accountId: string | undefined) => {
    try {
      let patientModel;
      const patientResponse = await get(`${baseUrl}/get?accountId${accountId}`);

      if (!patientResponse) {
        throw new Error("Unknown error");
      }

      patientModel = patientResponse.data;

      return patientModel;
    } catch (error) {
      throw error;
    }
  };

  return { createDoctor, getDoctor };
};

export default useDoctor;
