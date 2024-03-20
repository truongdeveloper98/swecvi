import React, { useMemo } from "react";
import PieChart from "examples/Charts/PieChart";
import { useTranslation } from "react-i18next";
// import useExamTypes from "./useExamTypes";

export default function ExamTypesChart() {
  // const { examTypes } = useExamTypes();

  const { t } = useTranslation();

  const chart = useMemo(
    () => (
      <PieChart
        title={t("ExamTypes")}
        chart={{
          // labels: examTypes?.map((x) => x?.type),
          labels: [],
          datasets: {
            label: "Projects",
            backgroundColors: [
              "primary",
              "secondary",
              "info",
              "success",
              "warning",
              "error",
              "light",
              "dark",
            ],
            // data: examTypes?.map((x) => x?.count),
            data: [],
          },
        }}
      />
    )
    // [JSON.stringify(examTypes)]
  );
  return chart;
}
