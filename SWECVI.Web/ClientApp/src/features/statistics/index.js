import React from "react";
import { Grid } from "@mui/material";
import BaseLayout from "components/Customized/BaseLayout";

import PeriodFiltersCard from "components/Customized/PeriodFilter";
import ParametersChart from "./components/ParametersChart";
import ExamTypeBarChart from "./components/ExamTypeBarChart";
import FindingBarChart from "./components/FindingBarChart";

export default function Statistics() {
  return (
    <BaseLayout>
      <Grid container spacing={1}>
        <Grid item xs={12} sm={6}>
          <PeriodFiltersCard />
        </Grid>
        <Grid item xs={12} sm={6} />
        <Grid item xs={12} sm={6}>
          <ParametersChart />
        </Grid>
        <Grid item xs={12} sm={3}>
          <ExamTypeBarChart />
        </Grid>
        <Grid item xs={12} sm={3}>
          <FindingBarChart />
        </Grid>
      </Grid>
    </BaseLayout>
  );
}
