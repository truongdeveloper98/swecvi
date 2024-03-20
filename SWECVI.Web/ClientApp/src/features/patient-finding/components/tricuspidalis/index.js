import { Grid } from "@mui/material";
import SelectedExamAlert from "features/exam-detail/components/SelectedExamAlert";
import React from "react";
// import MDButton from "components/MDButton";
import BoxFinding from "components/Customized/BoxFinding";
// import useTricuspidalis from "./hooks/useTricuspidalis";

const fakeData = {
  patientName: "Patient Demo",
};

export default function Tricuspidalis({ patientFindingBox }) {
  // const { handleBtnSave } = useTricuspidalis(params);
  return (
    <>
      <Grid container spacing={1} mt={1}>
        <Grid item xs={6}>
          <SelectedExamAlert data={fakeData} />
        </Grid>
      </Grid>
      <Grid container spacing={2}>
        <Grid item xs={7}>
          <Grid container spacing={2}>
            <Grid item xs={12} borderRadius={2}>
              <BoxFinding
                boxHeader="Allmänt"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Allmänt"
                )}
                tabName="Tricuspidalis"
              />
            </Grid>
            <Grid item xs={12}>
              <Grid container spacing={2}>
                <Grid item xs={6} borderRadius={2}>
                  <BoxFinding
                    boxHeader="Insufficient"
                    inputPatientFinding={patientFindingBox.filter(
                      (item) => item.boxHeader === "Insufficient"
                    )}
                    tabName="Tricuspidalis"
                  />
                </Grid>
                <Grid item xs={6} borderRadius={2}>
                  <BoxFinding
                    boxHeader="Stenos"
                    inputPatientFinding={patientFindingBox.filter(
                      (item) => item.boxHeader === "Stenos"
                    )}
                    tabName="Tricuspidalis"
                  />
                </Grid>
              </Grid>
            </Grid>
            <Grid item xs={12}>
              <BoxFinding
                boxHeader="Vegetation"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Vegetation"
                )}
                tabName="Tricuspidalis"
              />
            </Grid>
            <Grid item xs={12}>
              <BoxFinding
                boxHeader="Protes"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Protes"
                )}
                tabName="Tricuspidalis"
              />
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={5}>
          <Grid item xs={12} borderRadius={2}>
            <BoxFinding
              boxHeader="Utseende"
              inputPatientFinding={patientFindingBox.filter(
                (item) => item.boxHeader === "Utseende"
              )}
              tabName="Tricuspidalis"
            />
          </Grid>
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
