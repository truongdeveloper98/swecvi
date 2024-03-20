/* eslint-disable react/no-unstable-nested-components */
/* eslint-disable no-unused-vars */
import { Box, Button, Card, FormControlLabel, FormGroup, Grid, Switch } from "@mui/material";
import { createColumnHelper } from "@tanstack/react-table";
import BaseLayout from "components/Customized/BaseLayout";
import TsGridTable from "components/Customized/TsGridTable";
import React from "react";
import { useTranslation } from "react-i18next";
import { useSelector } from "react-redux";
import useSettings from "./hooks/useSettings";
import { getSettingsRequest } from "./services";

const columnHelper = createColumnHelper();

export default function ParameterSettings() {
  const { t } = useTranslation();

  const { agRef, onCreateSettings, handleEditSettings } = useSettings();

  const settings = useSelector((state) => state.settings.settings);

  const columns = [
    columnHelper.accessor("id", {
      id: "id",
      header: "Id",
    }),
    columnHelper.accessor("parameterId", {
      id: "parameterId",
      header: "Parameter Id",
    }),
    columnHelper.accessor("showInChart", {
      id: "showInChart",
      header: "Show In Chart",
    }),
    columnHelper.accessor("showInParameterTable", {
      id: "showInParameterTable",
      header: "show In Parameter Table",
    }),
    columnHelper.accessor("showInAssessmentText", {
      id: "showInAssessmentText",
      header: "show In Assessment Text",
    }),
    columnHelper.accessor("parameterId", {
      id: "parameterId",
      header: "Parameter Id",
    }),
    columnHelper.accessor("textFriendlyName", {
      id: "textFriendlyName",
      header: "Text Friendly Name",
    }),
    columnHelper.accessor("parameterHeader", {
      id: "parameterHeader",
      header: "Parameter Header",
    }),
    columnHelper.accessor("parameterSubHeader", {
      id: "parameterSubHeader",
      header: "Parameter Sub Header",
    }),
    columnHelper.accessor("displayDecimal", {
      id: "displayDecimal",
      header: "Display Decimal",
    }),
    columnHelper.accessor("parameterOrder", {
      id: "parameterOrder",
      header: "Parameter Order",
    }),
    columnHelper.accessor("parameterHeaderOrder", {
      id: "parameterHeaderOrder",
      header: "Parameter Header Order",
    }),
    columnHelper.accessor("poh", {
      id: "poh",
      header: "POH",
    }),
    columnHelper.accessor("description", {
      id: "description",
      header: "Description",
    }),
    columnHelper.accessor("functionSelector", {
      id: "functionSelector",
      header: "Function Selector",
    }),
    columnHelper.accessor("", {
      id: "action",
      header: () => null,
      cell: ({ row }) => (
        <Box style={{ textAlign: "right" }}>
          <Button onClick={() => handleEditSettings(row.original.id)}>Edit</Button>
        </Box>
      ),
    }),
  ];

  return (
    <BaseLayout>
      <TsGridTable
        ref={agRef}
        columns={columns}
        onCreate={onCreateSettings}
        onFetching={getSettingsRequest}
        entity={settings}
        entityName={t("References")}
        isExpand={false}
      />
    </BaseLayout>
  );
}
