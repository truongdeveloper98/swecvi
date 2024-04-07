/* eslint-disable react/no-unstable-nested-components */
/* eslint-disable no-unused-vars */
import BaseLayout from "components/Customized/BaseLayout";
import React from "react";
import { useTranslation } from "react-i18next";
import { createColumnHelper } from "@tanstack/react-table";
import TsGridTable from "components/Customized/TsGridTable";
import { useSelector } from "react-redux";
import { Box, Button } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import useHospital from "./hooks/useHospital";
import { getHospitalRequest } from "./services";

const columnHelper = createColumnHelper();

export default function Hospital() {
  const { agRef, onCreateHospital, handleEditHospital } = useHospital();
  const { t } = useTranslation();

  const hospitals = useSelector((state) => state.hospital.hospitals);

  const columns = [
    columnHelper.accessor("id", {
      id: "id",
      header: "Id",
    }),
    columnHelper.accessor("hospitalName", {
      id: "hospitalName",
      header: "Hospital Name",
    }),
    // columnHelper.accessor("connectionString", {
    //   id: "connectionString",
    //   header: "Connection String",
    // }),
    columnHelper.accessor("", {
      id: "action",
      header: () => null,
      cell: ({ row }) => (
        <Box style={{ textAlign: "right" }}>
          <Button onClick={() => handleEditHospital(row.original.id)}>
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
        entity={hospitals}
        onFetching={getHospitalRequest}
        onCreate={onCreateHospital}
        entityName={t("Hospital")}
        isExpand={false}
      />
    </BaseLayout>
  );
}
