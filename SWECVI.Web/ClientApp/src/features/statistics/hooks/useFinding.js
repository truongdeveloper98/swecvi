import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { findingNamesRequest, findingValuesRequest } from "../services";

const useFinding = () => {
  const period = useSelector((state) => state.common.period);
  const [findingsValueSelected, setFindingsValueSelected] = useState(undefined);
  const [findingsValue, setFindingsValue] = useState([]);

  useEffect(() => {
    findingNamesRequest();
  }, []);

  useEffect(() => {
    findingValuesRequest({ codeMeaning: findingsValueSelected, period }, (data) =>
      setFindingsValue(data)
    );
  }, [period, findingsValueSelected]);

  const getFindingsValues = (value) => {
    setFindingsValueSelected(value);
    findingValuesRequest({ codeMeaning: value, period }, (data) => setFindingsValue(data));
  };

  return {
    // state
    findingsValueSelected,
    findingsValue,

    // func
    getFindingsValues,
  };
};

export default useFinding;
