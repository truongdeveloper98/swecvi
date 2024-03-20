import React, { useEffect, useState } from "react";
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import { Backdrop, Card, CircularProgress, TextareaAutosize } from "@mui/material";
import { useSelector } from "react-redux";
import Grid from "@mui/material/Grid";
import MDButton from "components/MDButton";

function PatientDetails() {
  const assessmentText = useSelector((state) => state.exam.examReport?.assessmentText);
  const [editorValue, setEditorValue] = useState("");

  useEffect(() => {
    setEditorValue(assessmentText);
  }, [assessmentText]);

  const handleSubmit = async () => {
    await navigator.clipboard.writeText(editorValue);
  };

  const handleChange = (e) => {
    const { value } = e.target;
    setEditorValue(value);
  };

  return (
    <>
      <Card>
        <MDBox p={2} display="flex" justifyContent="space-between">
          <MDTypography xs={6} variant="h6" fontWeight="medium">
            Patient Details
          </MDTypography>
          <Grid item xs={6} textAlign="right">
            <MDButton onClick={handleSubmit} variant="gradient" color="info">
              Copy
            </MDButton>
          </Grid>
        </MDBox>
        <MDBox p={2} pt={0} height="100%" overflow="auto">
          <TextareaAutosize
            value={editorValue}
            onChange={handleChange}
            aria-label="empty textarea"
            placeholder="Patient Detail"
            style={{
              width: "100%",
              padding: 16,
              outline: "none",
              fontSize: "1rem",
            }}
          />
        </MDBox>
      </Card>
      {!assessmentText && (
        <Backdrop sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }} open>
          <CircularProgress color="inherit" />
        </Backdrop>
      )}
    </>
  );
}

export default PatientDetails;
