import React from "react";
import MDAlert from "components/MDAlert";
import moment from "moment";
import MDTypography from "components/MDTypography";

function SelectedExamAlert({ data }) {
  return data ? (
    <MDAlert>
      <MDTypography variant="body2" color="white" fontWeight="regular">
        SELECTED PATIENT AND EXAM: {data?.patientName}, {data?.dicomPatientId}{" "}
        {data?.date && data?.time
          ? `,${moment(data?.date).format("YYYY-MM-DD")}, ${moment(data?.time, "HHmmss").format(
              "HH:mm"
            )}`
          : ""}
      </MDTypography>
    </MDAlert>
  ) : null;
}
export default SelectedExamAlert;
