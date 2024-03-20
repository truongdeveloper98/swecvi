import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import { useSelector } from "react-redux";
import { Card, Grid } from "@mui/material";

function Divider() {
  return <hr className="solid" style={{ marginTop: 8, marginBottom: 8 }} />;
}

function HL7MeasurementsCard() {
  const examHL7Measurements = useSelector((state) => state.exam.examHL7Measurements);

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
            examHL7Measurements?.map((hl7, i) => (
              <MDBox p={2} bgColor={i % 2 === 0 ? "light" : "none"}>
                <Grid container spacing={2}>
                  <Grid item xs={3}>
                    <MDTypography variant="body2" fontWeight="medium">
                      {hl7.el}
                    </MDTypography>
                  </Grid>
                  <Grid item xs={9} container>
                    {hl7.fs?.map((field, j) => (
                      <>
                        <Grid item xs={12} container spacing={2}>
                          <Grid item xs={6}>
                            <MDTypography variant="body2">{field?.el || field?.sl}</MDTypography>
                          </Grid>
                          <Grid item xs={6}>
                            <MDTypography fontWeight="regular" variant="body2">
                              {field?.v ?? ""}
                            </MDTypography>
                          </Grid>
                        </Grid>
                        {j < hl7.fs.length - 1 && (
                          <Grid item xs={12}>
                            <Divider />
                          </Grid>
                        )}
                      </>
                    ))}
                  </Grid>
                </Grid>
              </MDBox>
            ))
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
