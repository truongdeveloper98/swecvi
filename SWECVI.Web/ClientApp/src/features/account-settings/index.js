import Grid from "@mui/material/Grid";
import MDBox from "components/MDBox";
import BaseLayout from "components/Customized/BaseLayout";
// import ChangePassword from "features/account-settings/components/ChangePassword";
import Sidenav from "features/account-settings/components/Sidenav";
import Header from "features/account-settings/components/Header";

function Settings() {
  return (
    <BaseLayout>
      <MDBox mt={4}>
        <Grid container spacing={1}>
          <Grid item xs={12} lg={3}>
            <Sidenav />
          </Grid>
          <Grid item xs={12} lg={9}>
            <MDBox mb={3}>
              <Grid container spacing={1}>
                <Grid item xs={12}>
                  <Header />
                </Grid>
              </Grid>
            </MDBox>
          </Grid>
        </Grid>
      </MDBox>
    </BaseLayout>
  );
}

export default Settings;
