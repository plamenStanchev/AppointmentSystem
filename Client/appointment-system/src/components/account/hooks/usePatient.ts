import useApi from "../../../shared/hooks/useApi";

interface CreatePatientModel {
  fistName: string;
  secondName: string;
  surName: string;
  address: string;
  pin: string;
  cityId: number;
  accountId: string;
}

const baseUrl = "/patient";

const usePatient = () => {
  const { get, post } = useApi();

  const createPatient = async (model: CreatePatientModel) => {
    try {
      let patientModel;
      const patientResponse = await post(`${baseUrl}/create`, model);

      if (!patientResponse) {
        throw new Error("Unknown error");
      }

      patientModel = patientResponse.data;

      return patientModel;
    } catch (error) {
      throw error;
    }
  };

  const getPatient = async (accountId: string | undefined) => {
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

  return { createPatient, getPatient };
};

export default usePatient;
