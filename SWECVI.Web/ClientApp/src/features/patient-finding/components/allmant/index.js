import { Box, Grid } from "@mui/material";
import BoxFinding from "components/Customized/BoxFinding";
// import MDButton from "components/MDButton";
import SelectedExamAlert from "features/exam-detail/components/SelectedExamAlert";
import React from "react";
// import MDSnackbar from "components/MDSnackbar";
// import useAllmant from "./hooks/useAllmant";

const fakeData = {
  patientName: "Patient Demo",
};

function Allmant({ patientFindingBox }) {
  // const { handleBtnSave, openSuccess, openError } = useAllmant();

  return (
    <>
      <Grid container spacing={1} mt={1}>
        <Grid item xs={8}>
          <SelectedExamAlert data={fakeData} />
        </Grid>
      </Grid>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <Grid container spacing={2}>
            <Grid item xs={6}>
              <BoxFinding
                boxHeader="Allmänt"
                inputPatientFinding={patientFindingBox.filter(
                  (item) => item.boxHeader === "Allmänt"
                )}
                tabName="Allmänt"
              />
            </Grid>
            <Grid item xs={6} display="flex" justifyContent="space-between" flexDirection="column">
              <Grid item xs={12}>
                <BoxFinding
                  boxHeader="Orsak"
                  inputPatientFinding={patientFindingBox.filter(
                    (item) => item.boxHeader === "Orsak"
                  )}
                  tabName="Allmänt"
                />
              </Grid>
              <Box sx={{ m: 1 }} />
              <Grid item xs={12}>
                <BoxFinding
                  boxHeader="Rytm"
                  inputPatientFinding={patientFindingBox.filter(
                    (item) => item.boxHeader === "Rytm"
                  )}
                  tabName="Allmänt"
                />
              </Grid>
            </Grid>
          </Grid>
        </Grid>
        <Grid item xs={12}>
          <BoxFinding
            boxHeader="Bedömning stresstest"
            inputPatientFinding={patientFindingBox.filter(
              (item) => item.boxHeader === "Bedömning stresstest"
            )}
            tabName="Allmänt"
          />
        </Grid>
      </Grid>
      {/* <Box sx={{ m: 2 }} />
      <Box display="flex" justifyContent="flex-end" flexDirection="row">
        <Box sx={{ m: 1 }} />
        <MDButton variant="gradient" color="info">
          Cancel and return
        </MDButton>
        <Box sx={{ m: 1 }} />
        <MDButton variant="gradient" color="info" onClick={handleBtnSave}>
          Save
        </MDButton>
      </Box>
      <MDSnackbar
        open={openSuccess}
        color="success"
        content="Success"
        title="Success"
        anchorOrigin={{ horizontal: "right", vertical: "top" }}
      />
      <MDSnackbar
        open={openError}
        color="error"
        content="Error"
        title="Error"
        anchorOrigin={{ horizontal: "right", vertical: "top" }}
      /> */}
    </>
  );
}

export default Allmant;
