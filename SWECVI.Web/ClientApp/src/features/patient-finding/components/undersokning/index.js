import { Box, Grid } from "@mui/material";
import SelectedExamAlert from "features/exam-detail/components/SelectedExamAlert";
import React from "react";
// import MDButton from "components/MDButton";

import BoxFinding from "components/Customized/BoxFinding";
// import useUndersokning from "./hooks/useUndersokning";

const fakeData = {
  patientName: "Patient Demo",
};

export default function Undersokning({ patientFindingBox }) {
  // const { handleBtnSave } = useUndersokning(params);

  return (
    <>
      <Grid container spacing={1} mt={1}>
        <Grid item xs={8}>
          <SelectedExamAlert data={fakeData} />
        </Grid>
      </Grid>
      <Grid container>
        <Grid item xs={12}>
          <Grid container spacing={2}>
            <Grid item xs={6} borderRadius={2}>
              <BoxFinding
                boxHeader="Kontrast"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Kontrast"
                )}
                tabName="Undersökning"
              />
            </Grid>
            <Grid item xs={6} borderRadius={2}>
              <BoxFinding
                boxHeader="TEE"
                inputPatientFinding={patientFindingBox.filter((item) => item.boxHeader === "TEE")}
                tabName="Undersökning"
              />
            </Grid>
          </Grid>
          <Box sx={{ m: 2 }} />
          <Grid container spacing={2}>
            <Grid item xs={6} borderRadius={2}>
              <BoxFinding
                boxHeader="Stresstest 1"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Stresstest 1"
                )}
                tabName="Undersökning"
              />
            </Grid>
            <Grid item xs={6} borderRadius={2}>
              <BoxFinding
                boxHeader="Stresstest 2"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Stresstest 2"
                )}
                tabName="Undersökning"
              />
            </Grid>
          </Grid>
        </Grid>
      </Grid>
      {/* <Box sx={{ m: 2 }} />
      <Box display="flex" justifyContent="flex-end" flexDirection="row">
        <MDButton variant="gradient" color="info">
          Save and return
        </MDButton>
        <Box sx={{ m: 1 }} />
        <MDButton variant="gradient" color="info">
          Cancel and return
        </MDButton>
        <Box sx={{ m: 1 }} />
        <MDButton variant="gradient" color="info" onClick={handleBtnSave}>
          Save
        </MDButton>
      </Box> */}
    </>
  );
}
