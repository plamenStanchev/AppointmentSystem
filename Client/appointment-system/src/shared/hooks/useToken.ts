import { useState } from "react";

interface LoginResponseModel {
  token: string;
  hasRole: boolean;
  role: string;
  succeeded: boolean;
  errorMesage: string;
}

const useToken = () => {
  const localStorageTokenKey = "token";

  const getToken = () => {
    const model = localStorage.getItem(localStorageTokenKey);

    return model ? (JSON.parse(model) as LoginResponseModel).token : undefined;
  };

  const [token, setToken] = useState<string | undefined>(getToken());

  const saveToken = (userToken: LoginResponseModel) => {
    localStorage.setItem(localStorageTokenKey, JSON.stringify(userToken));
    setToken(userToken.token);
  };

  return {
    setToken: saveToken,
    token,
  };
};

export default useToken;
