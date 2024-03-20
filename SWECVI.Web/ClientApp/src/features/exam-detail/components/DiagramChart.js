import DefaultLineChart from "examples/Charts/LineCharts/DefaultLineChart";
import React, { useMemo } from "react";
import { useSelector } from "react-redux";
import { Grid } from "@mui/material";
import Selector from "components/Customized/Selector";
import useDiagramChart from "../hooks/useDiagramChart";
import { colors } from "../constants";

export default function DiagramChart() {
  const { getArrLabels, data } = useDiagramChart();
  const parameterName = useSelector((state) => state.statistic.parameterNames);

  const chart = useMemo(
    () => (
      <DefaultLineChart
        title="Diagram"
        chart={{
          labels: [null, ...getArrLabels(), null] || [],
          datasets: data?.map((x, index) => ({
            label: x?.parameterName,
            color: colors[index],
            data: [
              null,
              ...(data?.map(({ valueByTimes }) => valueByTimes.map((item) => item?.value))[index] ??
                []),
            ],
          })),
        }}
        axisTitle={{
          x: "Dates",
        }}
        legend
      />
    ),
    [data]
  );

  return (
    <Grid container spacing={2}>
      <Grid item xs={12}>
        {chart}
      </Grid>
      <Grid item xs={12}>
        <Selector multiple options={parameterName} label="Parameters" property="parameterName" />
      </Grid>
    </Grid>
  );
}
