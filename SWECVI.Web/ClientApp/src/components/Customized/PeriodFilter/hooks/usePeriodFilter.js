import { useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { setPeriod } from "stores/reducers/common.reducer";
import { filters } from "../constants";

const usePeriodFilter = () => {
  const period = useSelector((state) => state.common.period);
  const [filter, setFilter] = useState(filters.filter((i) => i.value === period)?.title ?? "All");
  const dispatch = useDispatch();

  const handleFilter = (title, value) => {
    setFilter(title);
    dispatch(setPeriod(value));
  };

  return {
    filter,
    handleFilter,
  };
};
export default usePeriodFilter;
