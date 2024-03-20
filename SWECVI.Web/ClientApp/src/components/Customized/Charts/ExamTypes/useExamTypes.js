import { useEffect } from "react";
import { useSelector } from "react-redux";
import { getExamTypesRequest } from "./services";

const useExamTypes = () => {
  const examTypes = useSelector((state) => state.analytic.examTypes);
  const period = useSelector((state) => state.common.period);

  useEffect(() => {
    getExamTypesRequest({ period });
  }, [period]);

  return {
    examTypes,
  };
};
export default useExamTypes;
