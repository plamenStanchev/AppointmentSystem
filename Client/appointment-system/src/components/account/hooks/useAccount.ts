import useApi from "../../../shared/hooks/useApi";
import useDoctor from "./useDoctor";
import usePatient from "./usePatient";

const baseUrl = "/appointment";

const useAccount = () => {
  const { get } = useApi();
  const { getPatient } = usePatient();
  const { getDoctor } = useDoctor();

  return { getDoctor, getPatient };
};

export default useAccount;
