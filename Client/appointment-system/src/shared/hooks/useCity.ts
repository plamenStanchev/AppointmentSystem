import useApi from "./useApi";

const baseUrl = "/city";

const useCity = () => {
  const { get } = useApi();

  const getAllCities = async () => {
    try {
      let cityModel;

      const cityResponse = await get(`${baseUrl}/all`);

      if (!cityResponse) {
        throw new Error("Unknown error");
      }

      cityModel = cityResponse.data;

      return cityModel;
    } catch (e) {
      throw e;
    }
  };
  return { getAllCities };
};

export default useCity;
