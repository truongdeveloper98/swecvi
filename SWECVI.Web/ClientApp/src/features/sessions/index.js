import { useSelector } from "react-redux";
import { Grid } from "@mui/material";
import React, { useMemo } from "react";
import BaseLayout from "components/Customized/BaseLayout";
import AgGridTable from "components/Customized/AgGridTable";
import useSessions from "./hooks/useSessions";
import FormDialog from "./components/FormDialog";
import DetailCellRenderer from "./components/DetailCellRenderer";
import { getSessionsRequest } from "./services";

const parentColumnDefs = [
  {
    field: "name",
    headerName: "Name",
    cellRenderer: "agGroupCellRenderer",
  },
  {
    field: "english",
    headerName: "English Label",
  },
  {
    field: "swedish",
    headerName: "Swedish Label",
  },
];

function Sessions() {
  const sessions = useSelector((state) => state.session.sessions);
  const {
    dialogRef,
    handleSelectSession,
    handleOpenSessionModal,
    handleOpenSessionFieldModal,
    handleDeleteSession,
    handleDeleteSessionField,
    handleCreate,
    handleUpdate,
  } = useSessions();

  const detailCellRenderer = useMemo(() => DetailCellRenderer, []);
  return (
    <BaseLayout>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <AgGridTable
            entity={sessions}
            onFetching={getSessionsRequest}
            columnDefs={parentColumnDefs}
            onDelete={handleDeleteSession}
            onEdit={handleOpenSessionModal}
            onCreate={handleOpenSessionModal}
            detailCellRenderer={detailCellRenderer}
            detailCellRendererParams={{
              onDidMount: handleSelectSession,
              onEdit: handleOpenSessionFieldModal,
              onDelete: handleDeleteSessionField,
              onCreate: handleOpenSessionFieldModal,
            }}
            entityName="Section"
          />
        </Grid>
      </Grid>

      <FormDialog ref={dialogRef} onCreate={handleCreate} onUpdate={handleUpdate} />
    </BaseLayout>
  );
}

export default Sessions;
