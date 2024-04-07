/* eslint-disable react/no-unstable-nested-components */
import BaseLayout from "components/Customized/BaseLayout";
import TsGridTable from "components/Customized/TsGridTable";
import React from "react";
import { useSelector } from "react-redux";
import { createColumnHelper } from "@tanstack/react-table";
import { useTranslation } from "react-i18next";
import { Box, Button } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import useDepartment from "./hooks/useDepartment";
import { getDepartmentRequest } from "./services";

const columnHelper = createColumnHelper();

export default function Department() {
  const { agRef, onCreateDepartment, handleEditDepartment, handleDeleteDepartment } =
    useDepartment();
  const { t } = useTranslation();
  const departments = useSelector((state) => state.department.departments);
  const columns = [
    columnHelper.accessor("id", {
      id: "id",
      header: "Id",
    }),
    columnHelper.accessor("name", {
      id: "name",
      header: "Department Name",
    }),
    columnHelper.accessor("", {
      id: "edit",
      header: () => "Edit",
      maxSize: 20,
      cell: ({ row }) => (
        <Box>
          <Button onClick={() => handleEditDepartment(row.original.id)}>
            <EditIcon />
          </Button>
        </Box>
      ),
    }),
  ];

  return (
    <BaseLayout>
      <TsGridTable
        ref={agRef}
        columns={columns}
        entity={departments}
        onFetching={getDepartmentRequest}
        onCreate={onCreateDepartment}
        onDelete={handleDeleteDepartment}
        entityName={t("Department")}
        isExpand={false}
      />
    </BaseLayout>
  );
}
