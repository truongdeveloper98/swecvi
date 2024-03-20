import { Grid } from "@mui/material";
import BoxFinding from "components/Customized/BoxFinding";
import SelectedExamAlert from "features/exam-detail/components/SelectedExamAlert";
import React from "react";
// import MDButton from "components/MDButton";
// import useAorta from "./hooks/useAorta";

const fakeData = {
  patientName: "Patient Demo",
};

export default function Aorta({ patientFindingBox }) {
  // const { handleBtnSave } = useAorta(params);

  return (
    <>
      <Grid container spacing={1} mt={1}>
        <Grid item xs={6}>
          <SelectedExamAlert data={fakeData} />
        </Grid>
      </Grid>
      <Grid container>
        <Grid item xs={12} mb={2}>
          <Grid container display="flex" justifyContent="space-between">
            <Grid item xs={6.95} borderRadius={2}>
              <BoxFinding
                boxHeader="Allmänt"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Allmänt"
                )}
                tabName="Aorta"
              />
            </Grid>
            <Grid item xs={4.95} borderRadius={2}>
              <BoxFinding
                boxHeader="Utseende"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Utseende"
                )}
                tabName="Aorta"
              />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={6.95} mb={2}>
          <Grid container display="flex" justifyContent="space-between">
            <Grid item xs={5.9} borderRadius={2}>
              <BoxFinding
                boxHeader="Insufficient"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Insufficient"
                )}
                tabName="Aorta"
              />
            </Grid>
            <Grid item xs={5.9} borderRadius={2}>
              <BoxFinding
                boxHeader="Stenos"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Stenos"
                )}
                tabName="Aorta"
              />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={6.95} mb={2} borderRadius={2}>
          <BoxFinding
            boxHeader="Vegetation"
            inputPatientFinding={patientFindingBox.filter(
              (item) => item.boxHeader === "Vegetation"
            )}
            tabName="Aorta"
          />
        </Grid>
        <Grid item xs={6.95} borderRadius={2}>
          <BoxFinding
            boxHeader="Protes"
            inputPatientFinding={patientFindingBox.filter((item) => item.boxHeader === "Protes")}
            tabName="Aorta"
          />
        </Grid>
      </Grid>
      {/* <Box sx={{ m: 1 }} />
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
