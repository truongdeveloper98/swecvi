import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import { useSelector } from "react-redux";
import { Card } from "@mui/material";

function HL7MeasurementsCard() {
  const examHL7Measurements = useSelector((state) => state.exam.examReport?.stressText);
  return (
    <Card>
      <MDBox maxHeight="40vh" display="flex" flexDirection="column">
        <MDBox p={2} pb={0}>
          <MDTypography variant="h6" fontWeight="medium">
            Findings
          </MDTypography>
        </MDBox>

        <MDBox height="100%" overflow="auto" p={2}>
          {examHL7Measurements?.length ? (
            <MDBox textAlign="center">
              <MDTypography variant="body2">{examHL7Measurements}</MDTypography>
            </MDBox>
          ) : (
            <MDBox textAlign="center">
              <MDTypography variant="body2">No measurements to show</MDTypography>
            </MDBox>
          )}
        </MDBox>
      </MDBox>
    </Card>
  );
}

export default HL7MeasurementsCard;
