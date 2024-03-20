import React, { useEffect } from "react";
import { AgGridReact } from "ag-grid-react";
import MDBox from "components/MDBox";
import { useMaterialUIController } from "context";
import DeleteButton from "components/Customized/DeleteButton";
import EditButton from "components/Customized/EditButton";
import MDButton from "components/MDButton";
import { Grid, Icon } from "@mui/material";

function DetailCellRenderer({ data, node, api, onDidMount, onDelete, onEdit, onCreate }) {
  const rowId = node.id;
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;

  useEffect(() => {
    onDidMount(data);
    return () => {
      api.removeDetailGridInfo(rowId);
    };
  }, []);

  const colDefs = [
    {
      field: "name",
      headerName: "Field Name",
    },
    {
      field: "english",
      headerName: "English Label",
    },
    {
      field: "swedish",
      headerName: "Swedish Label",
    },
    {
      field: "",
      cellRenderer: EditButton,
      cellRendererParams: {
        onClick: onEdit,
      },
      cellStyle: { justifyContent: "center" },
      maxWidth: 80,
    },
    {
      field: "",
      cellRenderer: DeleteButton,
      cellRendererParams: {
        confirmTitle: `Are you sure you want to delete this "Session Field"?`,
        onClick: (field) => {
          onDelete(field?.id);
        },
      },
      cellStyle: { justifyContent: "center" },
    },
  ];

  const defaultColDef = {
    flex: 1,
  };

  const onGridReady = (params) => {
    const gridInfo = {
      id: node.id,
      api: params.api,
      columnApi: params.columnApi,
    };
    api.addDetailGridInfo(rowId, gridInfo);
  };

  return (
    <Grid container spacing={1} p={4}>
      <Grid item xs={12}>
        <MDBox className={darkMode ? "ag-theme-alpine-dark" : "ag-theme-alpine"}>
          <AgGridReact
            columnDefs={colDefs}
            defaultColDef={defaultColDef}
            rowData={data.fields ?? []}
            domLayout="autoHeight"
            onGridReady={onGridReady}
          />
        </MDBox>
      </Grid>
      <Grid item xs={12} textAlign="end">
        <MDButton onClick={onCreate} size="small">
          <Icon>add</Icon>Add new section field
        </MDButton>
      </Grid>
    </Grid>
  );
}

export default DetailCellRenderer;
