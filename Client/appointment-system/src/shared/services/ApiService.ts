import axios, { AxiosRequestConfig } from "axios";

const combineConfig = (config?: AxiosRequestConfig): AxiosRequestConfig => ({
  ...{
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  },
  ...config,
});

const createUrlCombiner = (baseUrl: string) => (url: string) =>
  `${baseUrl}${url}`;

const combineApiUrl = createUrlCombiner("https://localhost:5001/api");

const ApiService = {
  get: <R>(url: string, config?: AxiosRequestConfig) =>
    axios.get<R>(combineApiUrl(url), combineConfig(config)),

  post: <R, D = unknown>(url: string, data?: D, config?: AxiosRequestConfig) =>
    axios.post<R>(combineApiUrl(url), data, combineConfig(config)),
};

export default ApiService;
