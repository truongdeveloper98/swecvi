import React from "react";
import MDAlert from "components/MDAlert";
// import moment from "moment";
import MDTypography from "components/MDTypography";

function SelectedExamAlert({ data }) {
  return data ? (
    <MDAlert>
      <MDTypography variant="body2" color="white" fontWeight="regular">
        SELECTED PATIENT AND EXAM: Patient Name : {data?.patientName}, SSN :{" "}
        {data.ssn === undefined || data.ssn === "" ? "No data" : data.ssn}, Date : {data?.examDate},
        Study Type :{" "}
        {data.studyType === undefined || data.studyType === "" ? "No data" : data.studyType}
      </MDTypography>
    </MDAlert>
  ) : null;
}
export default SelectedExamAlert;
