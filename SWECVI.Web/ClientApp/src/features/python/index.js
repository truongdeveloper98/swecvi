import BaseLayout from "components/Customized/BaseLayout";
import React from "react";
import MDBox from "components/MDBox";
import { Card, TextareaAutosize } from "@mui/material";
import MDTypography from "components/MDTypography";
import MDButton from "components/MDButton";
import Grid from "@mui/material/Grid";
import Tabs from "@mui/material/Tabs";
import Tab from "@mui/material/Tab";
import DeleteButton from "components/Customized/DeleteButton";
import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import { styled } from "@mui/material/styles";
import List from "@mui/material/List";
import ListItemButton from "@mui/material/ListItemButton";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import InsertDriveFileIcon from "@mui/icons-material/InsertDriveFile";
import usePython from "./hooks/usePython";

const AntTabs = styled(Tabs)({
  "& .MuiTabs-indicator": {
    borderBottom: "2px solid #3498db",
    boxShadow: "none",
    borderRadius: 0,
  },
  borderRadius: 0,
  height: 48,
  padding: 0,
});

const ListItem = styled(ListItemText)({
  "& .MuICollectionItemText-secondary": {
    fontWeight: 400,
    color: "#000",
  },
});

export default function Python() {
  const {
    editorValue,
    pythons,
    version,
    selectedFile,

    handleDelete,

    handleReset,
    handleSetAsDefault,
    handleChange,
    handleCreate,

    setSelectedFile,
    setVersion,
  } = usePython();

  return (
    <BaseLayout>
      <Card>
        <MDBox height="100%" display="flex" flexDirection="column">
          <MDBox p={2} display="flex" justifyContent="space-between">
            <Grid item>
              <MDBox mb={2}>
                <MDTypography variant="h6" fontWeight="medium">
                  Scripts
                </MDTypography>
              </MDBox>
            </Grid>

            <Grid item textAlign="right">
              <Grid container spacing={1}>
                <Grid item>
                  <MDButton onClick={handleSetAsDefault} variant="gradient" color="success">
                    Set As Default
                  </MDButton>
                </Grid>
                <Grid item>
                  <MDButton onClick={handleReset} variant="gradient" color="error">
                    Reset
                  </MDButton>
                </Grid>
                <Grid item>
                  <MDButton onClick={handleCreate} variant="gradient" color="info">
                    Update
                  </MDButton>
                </Grid>
                <Grid item>
                  <DeleteButton
                    onClick={handleDelete}
                    confirmTitle='Are you sure you want to delete this "Python Code"?'
                  />
                </Grid>
              </Grid>
            </Grid>
          </MDBox>

          <Grid container>
            <Grid item xs={3}>
              <MDBox p={2}>
                <List component="nav" aria-label="main mailbox folders">
                  {pythons.map((x, index) => (
                    <ListItemButton
                      selected={selectedFile === index}
                      onClick={() => setSelectedFile(index)}
                    >
                      <ListItemIcon>
                        <InsertDriveFileIcon />
                      </ListItemIcon>

                      <ListItem secondary={x?.fileName} />
                    </ListItemButton>
                  ))}
                </List>
              </MDBox>
            </Grid>
            <Grid item xs={9}>
              <MDBox p={2} pt={0}>
                <AntTabs
                  value={version}
                  onChange={(e, i) => setVersion(i)}
                  variant="scrollable"
                  scrollButtons={false}
                  aria-label="scrollable prevent tabs example"
                >
                  {pythons[selectedFile]?.versions?.map((script) =>
                    script.isCurrentVersion ? (
                      <Tab
                        key={script.version}
                        label={`Version ${script.version}`}
                        icon={<CheckCircleIcon />}
                        iconPosition="end"
                      />
                    ) : (
                      <Tab key={script.version} label={`Version ${script.version}`} />
                    )
                  )}
                </AntTabs>
              </MDBox>
              <MDBox p={2} pt={0} height="100%" overflow="auto">
                <TextareaAutosize
                  aria-label="empty textarea"
                  placeholder="Python Script"
                  style={{ width: "100%", padding: 16, outline: "none", fontSize: "1rem" }}
                  minRows={25}
                  value={editorValue}
                  onChange={handleChange}
                />
              </MDBox>
            </Grid>
          </Grid>
        </MDBox>
      </Card>
    </BaseLayout>
  );
}
