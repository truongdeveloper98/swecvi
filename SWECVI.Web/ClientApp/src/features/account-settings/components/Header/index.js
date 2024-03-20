import Card from "@mui/material/Card";
import Grid from "@mui/material/Grid";
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import MDAvatar from "components/MDAvatar";
import karolinska from "assets/images/karolinska-institutet.png";
import { LOGOUT } from "constants/actionTypes";
import { useDispatch } from "react-redux";

function Header() {
  const dispatch = useDispatch();

  const handleLogout = () => {
    localStorage.removeItem("hospitalId");
    dispatch({ type: LOGOUT });
  };

  return (
    <Card id="profile">
      <MDBox p={2}>
        <Grid container spacing={1} alignItems="center">
          <Grid item>
            <MDAvatar src={karolinska} alt="profile-image" size="xl" shadow="sm" />
          </Grid>
          <Grid item>
            <MDBox height="100%" mt={0.5} lineHeight={1}>
              <MDTypography variant="h5" fontWeight="medium">
                Karolinska Institutet
              </MDTypography>
              <MDTypography variant="button" color="text" fontWeight="medium">
                {" "}
              </MDTypography>
            </MDBox>
          </Grid>
          <Grid item xs={12} md={6} lg={3} sx={{ ml: "auto" }}>
            <MDBox
              display="flex"
              justifyContent={{ md: "flex-end" }}
              alignItems="center"
              lineHeight={1}
            >
              <MDTypography
                variant="button"
                fontWeight="medium"
                color="error"
                onClick={handleLogout}
              >
                Logout
              </MDTypography>
            </MDBox>
          </Grid>
        </Grid>
      </MDBox>
    </Card>
  );
}

export default Header;
