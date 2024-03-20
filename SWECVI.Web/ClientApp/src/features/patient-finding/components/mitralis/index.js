import React from "react";

import SelectedExamAlert from "features/exam-detail/components/SelectedExamAlert";
import { Grid } from "@mui/material";
import BoxFinding from "components/Customized/BoxFinding";
// import MDButton from "components/MDButton";
// import useMitralis from "./hooks/useMitralis";

const fakeData = {
  patientName: "Patient Demo",
};

export default function Mitralis({ patientFindingBox }) {
  // const { handleBtnSave } = useMitralis(params);
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
                tabName="Mitralis"
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
                    tabName="Mitralis"
                  />
                </Grid>
                <Grid item xs={6} borderRadius={2}>
                  <BoxFinding
                    boxHeader="Stenos"
                    inputPatientFinding={patientFindingBox.filter(
                      (item) => item.boxHeader === "Stenos"
                    )}
                    tabName="Mitralis"
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
                tabName="Mitralis"
              />
            </Grid>
            <Grid item xs={12}>
              <BoxFinding
                boxHeader="Protes"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Protes"
                )}
                tabName="Mitralis"
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
              tabName="Mitralis"
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
