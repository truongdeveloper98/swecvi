import React, { useState } from "react";
import { Box, Grid } from "@mui/material";
import BaseLayout from "components/Customized/BaseLayout";
import MDButton from "components/MDButton";
import { useNavigate } from "react-router-dom";
import PAGES from "navigation/pages";
import { useSelector } from "react-redux";
import useExam from "./hooks/useExam";
import SelectedExamAlert from "./components/SelectedExamAlert";
import ParameterFiltersCard from "./components/ParameterFiltersCard";
import ParametersTable from "./components/ParametersTable";
import PatientDetails from "./components/PatientDetailsCard";
import DiagramChart from "./components/DiagramChart";

function ExamDetail() {
  const { params } = useExam();
  const targetExam = useSelector((state) => state.exam.examReport);

  const [poh, setPOH] = useState(undefined);
  const navigate = useNavigate();

  const handleBackPatientList = () => {
    navigate(PAGES.patients);
  };

  const handlePatientFindingt = () => {
    navigate(`${PAGES.patientFinding}/${params.hospitalId}/${params.id}`);
  };

  return (
    <BaseLayout>
      <Grid container spacing={1}>
        <Grid item xs={12} display="flex">
          <MDButton onClick={handleBackPatientList} variant="gradient" color="secondary">
            Back to patient list
          </MDButton>
          <Box sx={{ m: 2 }} />
          <MDButton onClick={handlePatientFindingt} variant="gradient" color="primary">
            Patient Finding
          </MDButton>
        </Grid>
        <Grid item xs={12}>
          <SelectedExamAlert data={targetExam} />
        </Grid>
      </Grid>
      <Grid container spacing={1}>
        <Grid item sm={6} xs={12}>
          <Grid item container spacing={1}>
            <Grid item xs={12}>
              <DiagramChart />
            </Grid>
            <Grid item xs={12}>
              <ParameterFiltersCard onChange={setPOH} />
            </Grid>
            <Grid item xs={12}>
              <ParametersTable filter={poh} />
            </Grid>
          </Grid>
        </Grid>

        <Grid item sm={6} xs={12}>
          <Grid container spacing={1}>
            <Grid item xs={12}>
              <PatientDetails />
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </BaseLayout>
  );
}

export default ExamDetail;
