import React from "react";
import DefaultLineChart from "examples/Charts/LineCharts/DefaultLineChart";
import useExamNumbers from "./useExamNumbers";

export default function ExamNumbersChart() {
  const { examNumbers } = useExamNumbers();

  return (
    <DefaultLineChart
      title="Number of exams"
      chart={{
        labels: examNumbers?.map((i) => i.time) ?? [],
        datasets: [
          {
            label: "Exams",
            color: "secondary",
            data: examNumbers?.map((i) => i.count) ?? [],
          },
        ],
      }}
    />
  );
}
