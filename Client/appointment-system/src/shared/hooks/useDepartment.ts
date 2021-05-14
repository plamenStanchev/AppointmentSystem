import useApi from "./useApi";

const baseUrl = "/department";

const useDepartment = () => {
  const { get } = useApi();

  const getAllDepartments = async () => {
    try {
      let departmentModel;

      const departmentResponse = await get(`${baseUrl}/all`);

      if (!departmentResponse) {
        throw new Error("Unknown error");
      }

      departmentModel = departmentResponse.data;

      return departmentModel;
    } catch (e) {
      throw e;
    }
  };

  return { getAllDepartments };
};

export default useDepartment;
