import React from "react";
import { Grid } from "@mui/material";
import { useSelector } from "react-redux";
import BubbleChart from "examples/Charts/BubbleChart";
import Selector from "components/Customized/Selector";
import { useTranslation } from "react-i18next";
import useParameter from "../hooks/useParameter";

export default function ParametersChart() {
  const { xAxisSelected, setParameterValueSelected, setXAxisSelected } = useParameter();
  const parameterStatics = useSelector((state) => state.statistic.parameterValues);
  const parameterNames = useSelector((state) => state.statistic.parameterNames);
  const xSelectors = useSelector((state) => state.statistic.xSelectors);

  const { t } = useTranslation();

  return (
    <Grid container spacing={1}>
      <Grid item xs={12}>
        <BubbleChart
          title={t("Parameters")}
          chart={{
            datasets: !xAxisSelected
              ? []
              : [
                  {
                    label: "Dataset",
                    color: "info",
                    data: parameterStatics?.map((x) => ({ x: x.xValue, y: x.yValue })),
                  },
                ],
          }}
        />
      </Grid>

      <Grid item xs={12}>
        <Grid container display="flex" justifyContent="space-around" spacing={1}>
          <Grid item xs={6}>
            <Selector
              onChange={setParameterValueSelected}
              options={parameterNames}
              label={t("Parameter")}
              property="parameterName"
            />
          </Grid>
          <Grid item xs={6}>
            <Selector onChange={setXAxisSelected} options={xSelectors} label="X Axis" />
          </Grid>
        </Grid>
      </Grid>
    </Grid>
  );
}
