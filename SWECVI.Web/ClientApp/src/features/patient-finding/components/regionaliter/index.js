import { Grid } from "@mui/material";
import React from "react";
// import MDButton from "components/MDButton";
import BoxFinding from "components/Customized/BoxFinding";
// import useRegionaliter from "./hooks/useRegionaliter";

export default function Regionaliteter({ patientFindingBox }) {
  // const { handleBtnSave } = useRegionaliter();
  return (
    <>
      <Grid container spacing={1} mt={1}>
        <Grid item xs={12} borderRadius={2}>
          <BoxFinding
            boxHeader="Väggrörlighet"
            inputPatientFinding={patientFindingBox.filter(
              (item) => item.boxHeader === "Väggrörlighet"
            )}
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
