import useApi from "./useApi";
import Constants from "../constants";

interface IApplicationRequest {
  accountId: string;
  data: any;
  status: StatusEnum;
  requestType: TypeRequestEnum;
}

enum TypeRequestEnum {
  DoctorCreation = "DoctorCreation",
}

enum StatusEnum {
  Sended = "Sended",
  Approved = "Approved",
  Closed = "Closed",
}

const useApplicationRequest = () => {
  const { post } = useApi();

  const createDoctorRequest = async (model: any) => {
    try {
      const requestModel: IApplicationRequest = {
        accountId: model.accountId,
        status: StatusEnum.Sended,
        data: JSON.stringify(model),
        requestType: TypeRequestEnum.DoctorCreation,
      };

      const doctorRequestResponse = await post(
        `/${Constants.ApplicationRequest}`,
        requestModel
      );

      if (!doctorRequestResponse.data) {
        throw new Error("Unknown error");
      }

      return doctorRequestResponse.data;
    } catch (error) {
      throw error;
    }
  };

  return { createDoctorRequest };
};

export default useApplicationRequest;
