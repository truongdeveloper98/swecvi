import { useEffect } from "react";
import { useSelector } from "react-redux";
import { getExamNumbersRequest } from "./services";

const useExamNumbers = () => {
  const period = useSelector((state) => state.common.period);
  const examNumbers = useSelector((state) => state.analytic.examNumbers);

  useEffect(() => {
    getExamNumbersRequest({ period });
  }, [period]);

  return {
    examNumbers,
  };
};
export default useExamNumbers;
