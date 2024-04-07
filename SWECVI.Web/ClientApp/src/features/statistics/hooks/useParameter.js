import { useEffect, useState } from "react";
import { parameterNamesRequest } from "features/statistics/services";
import { useSelector } from "react-redux";
import { parameterValuesRequest, xSelectorsRequest } from "../services";

const useParameter = () => {
  const period = useSelector((state) => state.common.period);
  const [parameterValueSelected, setParameterValueSelected] = useState(undefined);
  const [xAxisSelected, setXAxisSelected] = useState(undefined);

  useEffect(() => {
    parameterNamesRequest();
    xSelectorsRequest();
  }, []);

  useEffect(() => {
    if (parameterValueSelected?.id && xAxisSelected) {
      parameterValuesRequest({
        ySelectorId: parameterValueSelected?.id,
        xValueSelector: xAxisSelected,
        period,
      });
    }
  }, [period, xAxisSelected, parameterValueSelected?.id]);

  return {
    // state
    parameterValueSelected,
    xAxisSelected,

    // func
    setParameterValueSelected,
    setXAxisSelected,
  };
};

export default useParameter;
