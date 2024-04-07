/* eslint-disable react/no-unstable-nested-components */
import { createColumnHelper } from "@tanstack/react-table";
import BaseLayout from "components/Customized/BaseLayout";
import TsGridTable from "components/Customized/TsGridTable";
import React from "react";
import { useTranslation } from "react-i18next";
import { Box, Button } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import { useSelector } from "react-redux";
import useAssessment from "./hooks/useAssessment";
import { getAssessmentRequest } from "./services";

const columnHelper = createColumnHelper();

export default function Assessment() {
  const { agRef, onCreateAssessment, handleAssessment } = useAssessment();
  const { t } = useTranslation();
  const assessments = useSelector((state) => state.assessment.assessments);

  const columns = [
    columnHelper.accessor("id", {
      id: "id",
      header: "Id",
    }),
    columnHelper.accessor("descriptionReportText", {
      id: "descriptionReportText",
      header: "Description Report Text",
    }),
    columnHelper.accessor("callFunction", {
      id: "callFunction",
      header: "Call Function",
    }),
    columnHelper.accessor("dCode", {
      id: "dCode",
      header: "DCode",
    }),
    columnHelper.accessor("aCode", {
      id: "aCode",
      header: "ACode",
    }),
    columnHelper.accessor("", {
      id: "action",
      header: () => null,
      cell: ({ row }) => (
        <Box style={{ textAlign: "right" }}>
          <Button onClick={() => handleAssessment(row.original.id)}>
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
        onCreate={onCreateAssessment}
        onFetching={getAssessmentRequest}
        entity={assessments}
        entityName={t("Assessment")}
        isExpand={false}
      />
    </BaseLayout>
  );
}
