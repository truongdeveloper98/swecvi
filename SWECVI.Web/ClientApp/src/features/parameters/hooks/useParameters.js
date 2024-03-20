import usePaging from "components/Customized/AgGridTable/hooks/usePaging";
import { examPatientsRequest } from "features/patients/services";
import { useEffect } from "react";
import { useSelector } from "react-redux";
import {
  functionSelectorsRequest,
  getParametersRequest,
  updateParameterRequest,
} from "../services";

const useParameters = () => {
  const parameters = useSelector((state) => state.parameter.parameters);

  const {
    // states
    textSearch,
    pageOptions,
    sortingOrder,

    // funcs
    setTextSearch,

    // paging
    handleInputPagination,
    handleInputPaginationValue,
    previousPage,
    gotoPage,
    nextPage,
    setEntriesPerPage,
    handleSortChanged,

    fetchData,
  } = usePaging(parameters, getParametersRequest);

  const handleUpdateParameter = (id, params) => {
    updateParameterRequest(id, params, () => {
      fetchData();
      examPatientsRequest();
    });
  };

  useEffect(() => {
    functionSelectorsRequest();
  }, []);

  return {
    // states
    pageOptions,
    textSearch,
    sortingOrder,

    // funcs
    setTextSearch,
    handleUpdateParameter,

    // paging
    previousPage,
    gotoPage,
    nextPage,
    handleInputPagination,
    handleInputPaginationValue,
    setEntriesPerPage,
    handleSortChanged,
  };
};
export default useParameters;
