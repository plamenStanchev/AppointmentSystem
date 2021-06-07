import axios, { AxiosRequestConfig } from "axios";

import { IApiResponse } from "../models/IApiResponse";
import useToken from "./useToken";
import Constants from "../constants";

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

  const combineApiUrl = createUrlCombiner(`${Constants.ApiUrl}/api`);
  const get = <R>(url: string, config?: AxiosRequestConfig) =>
    axios
      .get<IApiResponse<R>>(combineApiUrl(url), combineConfig(config))
      .then((d) => d.data);

  const post = <R, D = unknown>(
    url: string,
    data?: D,
    config?: AxiosRequestConfig
  ) =>
    axios
      .post<IApiResponse<R>>(combineApiUrl(url), data, combineConfig(config))
      .then((d) => d.data);

  return { get, post };
};

export default useApi;
