import React, { forwardRef, useImperativeHandle } from "react";
import { AgGridReact } from "ag-grid-react";
import { ServerSideRowModelModule } from "@ag-grid-enterprise/server-side-row-model";
import { RowGroupingModule } from "@ag-grid-enterprise/row-grouping";
import { useMaterialUIController } from "context";
import Grid from "@mui/material/Grid";
import MDButton from "components/MDButton";
import Icon from "@mui/material/Icon";
import { useSelector } from "react-redux";
import MDBox from "components/MDBox";
import PageSize from "components/Customized/PageSize";
import SearchInput from "components/Customized/SearchInput";
import Pagination from "components/Customized/Pagination";
import { defaultColDef } from "constants/table";
import compact from "lodash/compact";
import DeleteButton from "../DeleteButton";
import EditButton from "../EditButton";
import usePaging from "./hooks/usePaging";

const AgGridTable = forwardRef(
  (
    {
      // states
      entity,

      onFetching,
      onDelete,
      onCreate,
      onEdit,

      entityName,

      // ag grid
      columnDefs,
      detailCellRenderer,
      detailCellRendererParams,
    },
    ref
  ) => {
    const pageSize = useSelector((state) => state.common.pageSize);
    const [controller] = useMaterialUIController();
    const { darkMode } = controller;

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

      shrinkRows,
    } = usePaging(entity, onFetching);

    useImperativeHandle(ref, () => ({
      fetchData: () => {
        fetchData();
      },
    }));

    return (
      <Grid container spacing={1}>
        <Grid item xs={12} justifyContent="space-between" display="flex">
          <MDBox display="flex" alignItems="center">
            {"totalPages" in entity && (
              <>
                <PageSize
                  pageSize={pageSize}
                  onChange={(event, value) => {
                    setEntriesPerPage(value);
                  }}
                />
                <SearchInput label="Search" value={textSearch} onChange={setTextSearch} />
              </>
            )}
          </MDBox>
          {onCreate && (
            <MDButton onClick={onCreate} variant="gradient" color="info">
              <Icon>add</Icon>&nbsp; Add New {entityName}
            </MDButton>
          )}
        </Grid>
        <Grid item xs={12}>
          <MDBox className={darkMode ? "ag-theme-alpine-dark" : "ag-theme-alpine"}>
            <AgGridReact
              rowData={entity.items ?? []}
              columnDefs={compact([
                ...columnDefs,
                onEdit && {
                  cellRenderer: EditButton,
                  cellRendererParams: {
                    onClick: onEdit,
                  },
                  cellStyle: { justifyContent: "center" },
                  maxWidth: 80,
                },
                onDelete && {
                  cellRenderer: DeleteButton,
                  cellRendererParams: {
                    confirmTitle: `Are you sure you want to delete this "${entityName}"?`,
                    onClick: ({ id }) => onDelete(id, fetchData),
                  },
                  cellStyle: { justifyContent: "center" },
                  maxWidth: 80,
                },
              ])}
              defaultColDef={defaultColDef}
              sortingOrder={sortingOrder}
              accentedSort
              domLayout="autoHeight"
              detailRowAutoHeight={!!detailCellRendererParams}
              masterDetail={!!detailCellRendererParams}
              detailCellRenderer={detailCellRenderer}
              detailCellRendererParams={detailCellRendererParams}
              onRowGroupOpened={({ node: { id } }) => shrinkRows(id)}
              modules={[ServerSideRowModelModule, RowGroupingModule]}
              onSortChanged={handleSortChanged}
              onRowClicked={(params) => params.node.setExpanded(!params.node.expanded)}
            />
          </MDBox>
        </Grid>
        <Grid item xs={12}>
          <Pagination
            entity={entity}
            previousPage={previousPage}
            gotoPage={gotoPage}
            nextPage={nextPage}
            pageOptions={pageOptions}
            onChange={(handleInputPagination, handleInputPaginationValue)}
          />
        </Grid>
      </Grid>
    );
  }
);

export default AgGridTable;
