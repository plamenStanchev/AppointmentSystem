import useApi from "../../../shared/hooks/useApi";

const baseUrl = "/identity";

const useIdentity = () => {
  const { post } = useApi();

  const login = async (credentials: any) => {
    try {
      let loginModel;

      const loginResponse = await post(`${baseUrl}/login`, credentials);

      if (!loginResponse) {
        throw new Error("Unknown error");
      }

      loginModel = loginResponse.data;

      return loginModel;
    } catch (e) {
      throw e;
    }
  };

  const register = async (credentials: any) => {
    try {
      let loginModel;

      const registerResponse = await post(`${baseUrl}/register`, credentials);

      if (!registerResponse) {
        throw new Error("Unknown error");
      }

      loginModel = registerResponse.data;

      return loginModel;
    } catch (e) {
      throw e;
    }
  };

  return { login, register };
};

export default useIdentity;
