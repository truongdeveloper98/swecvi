import React from "react";
import { AgGridReact } from "ag-grid-react";
import MDBox from "components/MDBox";
import { useMaterialUIController } from "context";
import PageSize from "components/Customized/PageSize";
import { defaultColDef } from "constants/table";
import useParameters from "../hooks/useParameters";

const getRowStyle = (params) => {
  if (params.data?.value && params.data?.reference && params.data?.isOutsideReferenceRange) {
    return { background: "#e74c3c" };
  }
  if (params.data?.value && params.data?.reference && !params.data?.isOutsideReferenceRange) {
    return { background: "#2ecc71" };
  }
  if (params.data?.value && !params.data?.reference) {
    return { background: "#95a5a6" };
  }
};

function ParametersTable({ filter }) {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const {
    // states
    measurements,
    pageSize,

    // funcs
    onGridReady,
    setEntriesPerPage,
  } = useParameters(filter);

  const parameterColumnDefs = [
    { field: "name", headerName: "Parameter" },
    { field: "unit", headerName: "Unit" },
    { field: "value", headerName: "Value" },
    { field: "reference", headerName: "Reference" },
  ];

  return (
    <MDBox height="30vh" className={darkMode ? "ag-theme-alpine-dark" : "ag-theme-alpine"}>
      <MDBox display="flex" alignItems="center" mb={0.5}>
        <PageSize
          pageSize={500}
          onChange={(event, value) => {
            setEntriesPerPage(value);
          }}
        />
      </MDBox>
      <AgGridReact
        rowData={measurements}
        defaultColDef={defaultColDef}
        rowHeight={30}
        getRowStyle={getRowStyle}
        columnDefs={parameterColumnDefs}
        pagination
        paginationPageSize={pageSize}
        onGridReady={onGridReady}
        domLayout="autoHeight"
      />
    </MDBox>
  );
}

export default ParametersTable;
