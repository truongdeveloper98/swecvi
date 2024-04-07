import React, { useMemo } from "react";
import PieChart from "examples/Charts/PieChart";
import useExamTypes from "./useExamTypes";

export default function ExamTypesChart() {
  const { examTypes } = useExamTypes();
  const chart = useMemo(
    () => (
      <PieChart
        title="Exam Types"
        chart={{
          labels: examTypes?.map((x) => x?.type),
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
            data: examTypes?.map((x) => x?.count),
          },
        }}
      />
    ),
    [JSON.stringify(examTypes)]
  );
  return chart;
}
