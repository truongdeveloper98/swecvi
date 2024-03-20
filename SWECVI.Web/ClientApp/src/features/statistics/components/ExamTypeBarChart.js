import VerticalBarChart from "examples/Charts/VerticalBarChart";
import React, { useState } from "react";
import { Grid } from "@mui/material";
import { useSelector } from "react-redux";
import Selector from "components/Customized/Selector";
import { useTranslation } from "react-i18next";

export default function ExamTypeBarChart() {
  const examTypes = useSelector((state) => state.analytic.examTypes);

  const [xAxisLabel, setXAxisLabel] = useState(undefined);

  const { t } = useTranslation();

  return (
    <>
      <Grid item xs={12}>
        <VerticalBarChart
          title={t("ExamTypes")}
          chart={{
            labels: xAxisLabel?.map((x) => x?.type),
            datasets: [
              {
                label: "exams",
                color: "dark",
                data: examTypes?.map((x) => x?.count),
              },
            ],
          }}
        />
      </Grid>
      <Grid item xs={12} py={2}>
        <Selector
          multiple
          onChange={setXAxisLabel}
          options={examTypes}
          label={t("ExamTypes")}
          property="type"
        />
      </Grid>
    </>
  );
}
