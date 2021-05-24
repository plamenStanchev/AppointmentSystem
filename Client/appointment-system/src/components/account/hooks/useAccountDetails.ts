import useToken from "../../../shared/hooks/useToken";

export interface AccountModel {
  email: string;
  nameid: string;
  role: string;
  nbf: number;
  exp: number;
  iat: number;
}

const useAccountDetails = () => {
  const { token: getToken } = useToken();

  const jwtDecode = (token: string | undefined) =>
    token ? (JSON.parse(atob(token.split(".")[1])) as AccountModel) : null;

  const account = jwtDecode(getToken);

  const accountId = account?.nameid;
  const role = account?.role;

  return { accountId, role };
};

export default useAccountDetails;
