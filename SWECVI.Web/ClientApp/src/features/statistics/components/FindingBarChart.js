import VerticalBarChart from "examples/Charts/VerticalBarChart";
import React from "react";
import { Grid } from "@mui/material";
import { useSelector } from "react-redux";
import Selector from "components/Customized/Selector";
import { useTranslation } from "react-i18next";
import useFinding from "../hooks/useFinding";

export default function FindingBarChart() {
  const { getFindingsValues, findingsValue } = useFinding();
  const findingNames = useSelector((state) => state.statistic.findingNames);

  const { t } = useTranslation();

  return (
    <>
      <Grid item xs={12}>
        <VerticalBarChart
          title={t("Findings")}
          chart={{
            labels: findingsValue[0]?.textValueByCodeMeanings?.map((x) => x?.textValue) || [],
            datasets: [
              {
                label: "findings",
                color: "dark",
                data: findingsValue[0]?.textValueByCodeMeanings?.map((x) => x?.count) || [],
              },
            ],
          }}
        />
      </Grid>
      <Grid item xs={12} py={2}>
        <Selector
          onChange={getFindingsValues}
          options={findingNames}
          label={t("Findings")}
          property="textValue"
        />
      </Grid>
    </>
  );
}
