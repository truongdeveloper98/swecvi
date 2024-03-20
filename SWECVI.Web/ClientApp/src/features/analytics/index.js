import { Grid } from "@mui/material";
import BaseLayout from "components/Customized/BaseLayout";

import ExamTypesChart from "components/Customized/Charts/ExamTypes";
import ExamNumbersChart from "components/Customized/Charts/ExamNumbers";
import PeriodFiltersCard from "components/Customized/PeriodFilter";

function Analytics() {
  return (
    <BaseLayout>
      <Grid container spacing={1}>
        <Grid item xs={12} sm={6}>
          <PeriodFiltersCard />
        </Grid>
        <Grid item xs={12} sm={6} />
        <Grid item xs={12} sm={6}>
          <ExamNumbersChart />
        </Grid>
        <Grid item xs={12} sm={6}>
          <ExamTypesChart />
        </Grid>
      </Grid>
    </BaseLayout>
  );
}
export default Analytics;
