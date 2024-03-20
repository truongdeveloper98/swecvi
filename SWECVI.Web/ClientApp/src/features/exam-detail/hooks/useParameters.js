// import { functionSelectorsRequest } from "features/parameters/services";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { setPageSize } from "stores/reducers/common.reducer";

const useParameters = (filter) => {
  const pageSize = useSelector((state) => state.common.pageSize);
  const dispatch = useDispatch();
  const [gridApi, setGridApi] = useState(null);

  // useEffect(() => {
  //   functionSelectorsRequest();
  // }, []);

  const measurements = useSelector(({ exam }) =>
    filter
      ? exam.examReport?.measurements?.filter(
          ({ poh, name }) => name && poh?.includes(`${filter}`)
        ) ?? []
      : exam.examReport?.measurements?.filter(
          ({ name, value, reference, isOutsideReferenceRange }) => {
            const isPathologicalValues = filter === 0;
            if (isPathologicalValues) {
              return name && value && reference && isOutsideReferenceRange;
            }
            return name;
          }
        ) ?? []
  );

  const onGridReady = (params) => {
    setGridApi(params.api);
  };

  const setEntriesPerPage = (value) => {
    dispatch(setPageSize(value));
    gridApi.paginationSetPageSize(Number(value));
  };
  return {
    // states
    measurements,
    pageSize,

    // funcs
    onGridReady,
    setEntriesPerPage,
  };
};
export default useParameters;
