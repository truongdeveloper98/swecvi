import Grid from "@mui/material/Grid";
import Divider from "@mui/material/Divider";
// import FacebookIcon from "@mui/icons-material/Facebook";
// import TwitterIcon from "@mui/icons-material/Twitter";
// import InstagramIcon from "@mui/icons-material/Instagram";
import MDBox from "components/MDBox";
import ProfileInfoCard from "examples/Cards/InfoCards/ProfileInfoCard";
import Header from "features/profile/components/Header";
import BaseLayout from "components/Customized/BaseLayout";

function Profile() {
  return (
    <BaseLayout>
      <MDBox mb={2} />
      <Header>
        <MDBox mt={5} mb={3}>
          <Grid container spacing={1}>
            <Grid item xs={12} md={6} xl={4} sx={{ display: "flex" }}>
              <Divider orientation="vertical" sx={{ ml: -2, mr: 1 }} />
              <ProfileInfoCard
                title="profile information"
                description=""
                info={{
                  fullName: "",
                  mobile: "",
                  email: "",
                  location: "",
                }}
                // social={[
                //   {
                //     link: "https://www.facebook.com/CreativeTim/",
                //     icon: <FacebookIcon />,
                //     color: "facebook",
                //   },
                //   {
                //     link: "https://twitter.com/creativetim",
                //     icon: <TwitterIcon />,
                //     color: "twitter",
                //   },
                //   {
                //     link: "https://www.instagram.com/creativetimofficial/",
                //     icon: <InstagramIcon />,
                //     color: "instagram",
                //   },
                // ]}
                action={{ route: "", tooltip: "Edit Profile" }}
                shadow={false}
              />
            </Grid>
          </Grid>
        </MDBox>
      </Header>
    </BaseLayout>
  );
}

export default Profile;
