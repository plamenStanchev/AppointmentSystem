import axios, { AxiosRequestConfig } from "axios";
import useToken from "./useToken";

const useApi = () => {
  const { token } = useToken();
  const combineConfig = (config?: AxiosRequestConfig): AxiosRequestConfig => ({
    ...{
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    },
    ...config,
  });

  const createUrlCombiner = (baseUrl: string) => (url: string) =>
    `${baseUrl}${url}`;

  const combineApiUrl = createUrlCombiner("https://localhost:5001/api");
  const get = <R>(url: string, config?: AxiosRequestConfig) =>
    axios.get<R>(combineApiUrl(url), combineConfig(config));

  const post = <R, D = unknown>(
    url: string,
    data?: D,
    config?: AxiosRequestConfig
  ) => axios.post<R>(combineApiUrl(url), data, combineConfig(config));

  return { get, post };
};

export default useApi;