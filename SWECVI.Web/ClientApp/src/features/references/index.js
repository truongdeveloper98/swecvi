/* eslint-disable react/no-unstable-nested-components */
import { createColumnHelper } from "@tanstack/react-table";
import BaseLayout from "components/Customized/BaseLayout";
import TsGridTable from "components/Customized/TsGridTable";
import React from "react";
import { useTranslation } from "react-i18next";
import { Box, Button } from "@mui/material";
import { useSelector } from "react-redux";
import useReferences from "./hooks/useReferences";
import { getReferencesRequest } from "./services";

const columnHelper = createColumnHelper();

export default function References() {
  const { agGref, onCreateReferences, handleEditReferences } = useReferences();
  const { t } = useTranslation();
  const references = useSelector((state) => state.references.references);

  const columns = [
    columnHelper.accessor("id", {
      id: "id",
      header: "Id",
    }),
    columnHelper.accessor("parameterId", {
      id: "parameterId",
      header: "Parameter Id",
    }),
    columnHelper.accessor("parameterNameLogic", {
      id: "parameterNameLogic",
      header: "Parameter Name Logic",
    }),
    columnHelper.accessor("displayUnit", {
      id: "displayUnit",
      header: "Display Unit",
    }),
    columnHelper.accessor("departmentName", {
      id: "departmentName",
      header: "Department Name",
    }),
    columnHelper.accessor("normalRangeLower", {
      id: "normalRangeLower",
      header: "Normal Lower",
    }),
    columnHelper.accessor("normalRangeUpper", {
      id: "normalRangeUpper",
      header: "Normal Upper",
    }),
    columnHelper.accessor("mildlyAbnormalRangeLower", {
      id: "mildlyAbnormalRangeLower",
      header: "Mildly Lower",
    }),
    columnHelper.accessor("mildlyAbnormalRangeUpper", {
      id: "mildlyAbnormalRangeUpper",
      header: "Mildly Upper",
    }),
    columnHelper.accessor("moderatelyAbnormalRangeLower", {
      id: "moderatelyAbnormalRangeLower",
      header: "Moderately Lower",
    }),
    columnHelper.accessor("moderatelyAbnormalRangeUpper", {
      id: "moderatelyAbnormalRangeUpper",
      header: "Moderately Upper",
    }),
    columnHelper.accessor("severelyAbnormalRangeMoreThan", {
      id: "severelyAbnormalRangeMoreThan",
      header: "Severely Lower",
    }),
    columnHelper.accessor("severelyAbnormalRangeLessThan", {
      id: "severelyAbnormalRangeLessThan",
      header: "Severely Upper",
    }),
    columnHelper.accessor("gender", {
      id: "gender",
      header: "Gender",
    }),
    columnHelper.accessor("", {
      id: "action",
      header: () => null,
      cell: ({ row }) => (
        <Box style={{ textAlign: "right" }}>
          <Button onClick={() => handleEditReferences(row.original.id)}>Edit</Button>
        </Box>
      ),
    }),
  ];
  return (
    <BaseLayout>
      <TsGridTable
        ref={agGref}
        columns={columns}
        onCreate={onCreateReferences}
        onFetching={getReferencesRequest}
        entity={references}
        entityName={t("References")}
        isExpand={false}
      />
    </BaseLayout>
  );
}
