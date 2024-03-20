import { Box, CircularProgress, Grid } from "@mui/material";
import BaseLayout from "components/Customized/BaseLayout";
import PatientFindingFiltersCard from "components/Customized/PatientFindingFilter";
import React from "react";
import { useSelector } from "react-redux";
import Undersokning from "./components/undersokning";
import Aorta from "./components/aorta";
import Mitralis from "./components/mitralis";
import Ovrigt from "./components/ovrigt";
import Pulmonalis from "./components/pulmonalis";
import Tricuspidalis from "./components/tricuspidalis";
import Allmant from "./components/allmant";
import usePatientFinding from "./hooks/usePatientFinding";
import Regionaliteter from "./components/regionaliter";

export default function PatientFinding() {
  usePatientFinding();
  const patientFind = useSelector((state) => state.common.patientFinding);
  const patientFindingBox = useSelector((state) => state.patient.patientFindingBox);
  const isLoading = useSelector((state) => state.patient.isLoading);

  return (
    <BaseLayout>
      <Grid container spacing={1}>
        <Grid item xs={12}>
          <PatientFindingFiltersCard />
        </Grid>
      </Grid>
      {isLoading ? (
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            height: "50vh",
          }}
        >
          <CircularProgress style={{ color: "blue" }} fourColor variant="indeterminate" />
        </Box>
      ) : (
        <>
          {patientFind === "allmänt" && (
            <Allmant
              patientFindingBox={patientFindingBox.filter((item) => item.tabName === "Allmänt")}
            />
          )}
          {patientFind === "undersokning" && (
            <Undersokning
              patientFindingBox={patientFindingBox.filter(
                (item) => item.tabName === "Undersökning"
              )}
            />
          )}
          {patientFind === "aorta" && (
            <Aorta
              patientFindingBox={patientFindingBox.filter((item) => item.tabName === "Aorta")}
            />
          )}
          {patientFind === "mitralis" && (
            <Mitralis
              patientFindingBox={patientFindingBox.filter((item) => item.tabName === "Mitralis")}
            />
          )}
          {patientFind === "pulmonalis" && (
            <Pulmonalis
              patientFindingBox={patientFindingBox.filter((item) => item.tabName === "Pulmonalis")}
            />
          )}
          {patientFind === "tricuspidalis" && (
            <Tricuspidalis
              patientFindingBox={patientFindingBox.filter(
                (item) => item.tabName === "Tricuspidalis"
              )}
            />
          )}
          {patientFind === "regionaliter" && (
            <Regionaliteter
              patientFindingBox={patientFindingBox.filter(
                (item) => item.tabName === "Regionalitet"
              )}
            />
          )}
          {patientFind === "ovrigt" && (
            <Ovrigt
              patientFindingBox={patientFindingBox.filter((item) => item.tabName === "Övrigt")}
            />
          )}
        </>
      )}
    </BaseLayout>
  );
}
