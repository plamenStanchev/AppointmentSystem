import useDoctor from "./useDoctor";
import usePatient from "./usePatient";

const useAccount = () => {
  const { getPatient } = usePatient();
  const { getDoctor } = useDoctor();

  return { getDoctor, getPatient };
};

export default useAccount;
