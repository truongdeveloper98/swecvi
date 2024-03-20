import React from "react";
import DefaultLineChart from "examples/Charts/LineCharts/DefaultLineChart";
import { useTranslation } from "react-i18next";
// import useExamNumbers from "./useExamNumbers";

export default function ExamNumbersChart() {
  // const { examNumbers } = useExamNumbers();

  const { t } = useTranslation();

  return (
    <DefaultLineChart
      title={t("NumberOfExams")}
      chart={{
        labels: [],
        datasets: [
          {
            label: "Exams",
            color: "secondary",
            data: [],
          },
        ],
      }}
    />
  );
}
