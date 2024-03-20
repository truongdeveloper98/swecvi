import React from "react";
import { AgGridReact } from "ag-grid-react";
import { ServerSideRowModelModule } from "@ag-grid-enterprise/server-side-row-model";
import { RowGroupingModule } from "@ag-grid-enterprise/row-grouping";
import { useMaterialUIController } from "context";
import Grid from "@mui/material/Grid";
import { useSelector } from "react-redux";
import MDBox from "components/MDBox";
import PageSize from "components/Customized/PageSize";
import SearchInput from "components/Customized/SearchInput";
import Pagination from "components/Customized/Pagination";
import BaseLayout from "components/Customized/BaseLayout";
import { defaultColDef } from "constants/table";
import CellCheckbox from "components/Customized/CellCheckbox";
import Selector from "components/Customized/Selector";
import useParameters from "./hooks/useParameters";

function Parameters() {
  const {
    sortingOrder,

    handleInputPagination,
    handleInputPaginationValue,

    pageOptions,
    textSearch,
    setTextSearch,

    setEntriesPerPage,
    previousPage,
    gotoPage,
    nextPage,
    handleSortChanged,

    handleUpdateParameter,
  } = useParameters();

  const pageSize = useSelector((state) => state.common.pageSize);
  const functions = useSelector((state) => state.parameter.functionSelectors);

  const parentColumnDefs = [
    {
      field: "id",
      headerName: "Parameter ID",
    },
    {
      field: "databaseName",
      headerName: "Database Name",
      editable: true,
      valueSetter: ({ data, newValue }) => {
        handleUpdateParameter(data.id, { ...data, databaseName: newValue });
      },
    },
    {
      field: "showInChart",
      headerName: "Show In Chart",
      cellRenderer: CellCheckbox,
      cellRendererParams: {
        onChange: handleUpdateParameter,
      },
    },
    {
      field: "showInParameterTable",
      headerName: "Show In Parameter Table",
      cellRenderer: CellCheckbox,
      cellRendererParams: {
        onChange: handleUpdateParameter,
      },
    },
    {
      field: "showInAssessmentText",
      headerName: "Show In Assessment Text",
      cellRenderer: CellCheckbox,
      cellRendererParams: {
        onChange: handleUpdateParameter,
      },
    },
    {
      field: "tableFriendlyName",
      headerName: "Table Friendly Name",
      editable: true,
      valueSetter: ({ data, newValue }) => {
        handleUpdateParameter(data.id, { ...data, tableFriendlyName: newValue });
      },
    },
    {
      field: "displayDecimal",
      headerName: "Display Decimal",
      editable: true,
      valueSetter: ({ data, newValue }) => {
        handleUpdateParameter(data.id, { ...data, displayDecimal: newValue });
      },
    },
    {
      field: "unitName",
      headerName: "Unit",
      editable: true,
      valueSetter: ({ data, newValue }) => {
        handleUpdateParameter(data.id, { ...data, unitName: newValue });
      },
    },
    {
      field: "poh",
      headerName: "POH",
      editable: true,
      valueSetter: ({ data, newValue }) => {
        handleUpdateParameter(data.id, { ...data, poh: newValue });
      },
    },
    {
      field: "srt",
      headerName: "SRT",
      editable: true,
      valueSetter: ({ data, newValue }) => {
        handleUpdateParameter(data.id, { ...data, srt: newValue });
      },
    },
    {
      field: "description",
      headerName: "Description",
      editable: true,
      valueSetter: ({ data, newValue }) => {
        handleUpdateParameter(data.id, { ...data, description: newValue });
      },
    },
    {
      field: "functionSelector",
      headerName: "Function",
      cellRenderer: Selector,
      cellRendererParams: ({ data, value }) => ({
        onChange: (val) =>
          handleUpdateParameter(data.id, { ...data, functionSelector: val?.value ?? null }),
        options: functions,
        property: "functionName",
        label: "Default",
        value: functions.find((option) => option.value === value)?.functionName,
        hideLabel: true,
        style: { width: "100%" },
      }),
      minWidth: 130,
    },
  ];

  const parameters = useSelector((state) => state.parameter.parameters);
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;

  return (
    <BaseLayout>
      <Grid container spacing={1}>
        <Grid item xs={12} justifyContent="space-between" display="flex">
          <MDBox display="flex" alignItems="center">
            <PageSize
              pageSize={pageSize}
              onChange={(event, value) => {
                setEntriesPerPage(value);
              }}
            />
            <SearchInput
              label="Enter Parameter's Name"
              value={textSearch}
              onChange={setTextSearch}
            />
          </MDBox>
        </Grid>
        <Grid item xs={12}>
          <MDBox className={darkMode ? "ag-theme-alpine-dark" : "ag-theme-alpine"}>
            <AgGridReact
              rowData={parameters.items}
              columnDefs={parentColumnDefs}
              defaultColDef={defaultColDef}
              modules={[ServerSideRowModelModule, RowGroupingModule]}
              sortingOrder={sortingOrder}
              accentedSort
              domLayout="autoHeight"
              onSortChanged={handleSortChanged}
            />
          </MDBox>
        </Grid>
        <Grid item xs={12}>
          <Pagination
            entity={parameters}
            previousPage={previousPage}
            gotoPage={gotoPage}
            nextPage={nextPage}
            pageOptions={pageOptions}
            onChange={(handleInputPagination, handleInputPaginationValue)}
          />
        </Grid>
      </Grid>
    </BaseLayout>
  );
}

export default Parameters;
