import useApi from "./useApi";

const baseUrl = "/department";

const useDepartment = () => {
  const { get } = useApi();

  const getDepartment = async () => {
    try {
      let demartpmetModel;

      const departmentResponse = await get(`${baseUrl}/get`);

      if (!departmentResponse) {
        throw new Error("Unknown error");
      }

      demartpmetModel = departmentResponse.data;

      return demartpmetModel;
    } catch (e) {
      throw e;
    }
  };
  return { getDepartment };
};

export default useDepartment;
