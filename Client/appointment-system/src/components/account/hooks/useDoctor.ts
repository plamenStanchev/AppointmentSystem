import useApi from "../../../shared/hooks/useApi";

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

const baseUrl = "/doctors";

const useDoctor = () => {
  const { post, get } = useApi();

  const createDoctor = async (model: ICreateDoctorModel) => {
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
      const patientResponse = await get(
        `${baseUrl}/get?accountId=${accountId}`
      );

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
