import { Box, Grid } from "@mui/material";
import SelectedExamAlert from "features/exam-detail/components/SelectedExamAlert";
import React from "react";
import BoxFinding from "components/Customized/BoxFinding";

const fakeData = {
  patientName: "Patient Demo",
};
export default function Ovrigt({ patientFindingBox }) {
  return (
    <>
      <Grid container spacing={1} mt={1}>
        <Grid item xs={6}>
          <SelectedExamAlert data={fakeData} />
        </Grid>
      </Grid>
      <Grid container display="flex" justifyContent="space-between" spacing={2}>
        <Grid item xs={6} borderRadius={2}>
          <BoxFinding
            boxHeader="Perikardvätska/Pleuravätska"
            inputPatientFinding={patientFindingBox.filter(
              (item) => item.boxHeader === "Perikardvätska/Pleuravätska"
            )}
            tabName="Övrigt"
          />
        </Grid>
        <Grid item xs={6} borderRadius={2}>
          <BoxFinding
            boxHeader="Övrigt"
            inputPatientFinding={patientFindingBox.filter((item) => item.boxHeader === "Övrigt")}
            tabName="Övrigt"
          />
        </Grid>
        <Grid item xs={6} borderRadius={2}>
          <BoxFinding
            boxHeader="Tromb"
            inputPatientFinding={patientFindingBox.filter((item) => item.boxHeader === "Tromb")}
            tabName="Övrigt"
          />
        </Grid>
        {/* <Grid item xs={6} borderRadius={2}>
          <BoxFinding
            boxHeader="Aorta"
            inputPatientFinding={patientFindingBox.filter((item) => item.boxHeader === "Aorta")}
          />
        </Grid> */}
      </Grid>
      <Box sx={{ m: 2 }} />
    </>
  );
}
