import ApiService from "../../../shared/services/ApiService";

const IdentityService = {
  login: async (credentials: any) => {
    try {
      let loginModel;

      const loginResponse = await ApiService.post(
        "/identity/login",
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
};

export default IdentityService;
