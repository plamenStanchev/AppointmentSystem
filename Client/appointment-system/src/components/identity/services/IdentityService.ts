import ApiService from "../../../shared/services/ApiService";

const baseUrl = "/identity";

const IdentityService = {
  login: async (credentials: any) => {
    try {
      let loginModel;

      const loginResponse = await ApiService.post(
        `${baseUrl}/login`,
        credentials
      );

      if (!loginResponse) {
        throw new Error("Unknown error");
      }

      loginModel = loginResponse.data;

      return loginModel;
    } catch (e) {
      throw e;
    }
  },
  register: async (credentials: any) => {
    try {
      let loginModel;

      const registerResponse = await ApiService.post(
        `${baseUrl}/register`,
        credentials
      );

      if (!registerResponse) {
        throw new Error("Unknown error");
      }

      loginModel = registerResponse.data;

      return loginModel;
    } catch (e) {
      throw e;
    }
  },
};

export default IdentityService;
