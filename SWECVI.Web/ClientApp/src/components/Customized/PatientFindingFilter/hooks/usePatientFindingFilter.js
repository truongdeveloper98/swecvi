import { useSelector, useDispatch } from "react-redux";
import { useState } from "react";
import { setPatientFinding } from "stores/reducers/common.reducer";
import { patientFinding } from "../constants";

const usePatientFindingFilters = () => {
  const patientFindingSelector = useSelector((state) => state.common.patientFinding);
  const [filter, setFilter] = useState(
    patientFinding.filter((i) => i.value === patientFindingSelector)?.title ?? "Allmänt"
  );
  const dispatch = useDispatch();

  const handleFilter = (title, value) => {
    setFilter(title);
    dispatch(setPatientFinding(value));
  };

  return {
    filter,
    handleFilter,
  };
};
export default usePatientFindingFilters;
