/* eslint-disable no-unused-vars */
import BaseLayout from "components/Customized/BaseLayout";
import { useMaterialUIController } from "context";
import { Formik } from "formik";
import React from "react";
import { useTranslation } from "react-i18next";
import * as Yup from "yup";
import { Backdrop, Card, CircularProgress, Grid } from "@mui/material";
import MDButton from "components/MDButton";
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import { useParams } from "react-router-dom";
import FormField from "components/Customized/FormFiled";
import Selector from "components/Customized/Selector";
import useDepartmentDetail from "./hooks/useDepartmentDetail";

const departmentSchema = Yup.object().shape({
  name: Yup.string().required("Required"),
  // description: Yup.string().required("Required"),
  modality: Yup.string().required("Required"),
  location: Yup.string().required("Required"),
  // hospital: Yup.string().required("Required"),
});

export default function DepartmentDetail() {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const { t } = useTranslation();
  const params = useParams();
  const { department, handleCancel, handleSubmitForm, hospitalSuperAdmin, openBackdrop } =
    useDepartmentDetail();

  return (
    <BaseLayout>
      <Formik
        enableReinitialize
        initialValues={{
          name: department?.name,
          // description: department?.description,
          modality: department?.modality,
          location: department?.location,
          // hospital: hospitalSuperAdmin.find((item) => item.id === department?.indexHospital)?.name,
        }}
        validationSchema={departmentSchema}
        onSubmit={(values) => {
          // console.log(values);
          const data = {
            ...values,
            indexHospital: hospitalSuperAdmin[0].id,
          };
          handleSubmitForm(data);
        }}
      >
        {({ values, errors, handleChange, handleSubmit, setFieldValue }) => (
          <>
            <Grid item xs={12} md={5} sx={{ textAlign: "right", paddingBottom: 1 }}>
              <MDButton
                onClick={handleCancel}
                variant="gradient"
                color="error"
                sx={{ marginRight: 1 }}
              >
                {t("Cancel")}
              </MDButton>
              <MDButton variant="gradient" color="info" onClick={handleSubmit}>
                {t("SaveChanged")}
              </MDButton>
            </Grid>
            <MDBox
              height="80vh"
              mb={2}
              className={darkMode ? "ag-theme-alpine-dark" : "ag-theme-alpine"}
            >
              <Card id="basic-info" sx={{ overflow: "visible" }}>
                <MDBox p={3}>
                  <MDTypography variant="h5">
                    {params?.id ? t("EditDepartment") : t("CreateDepartment")}
                  </MDTypography>
                </MDBox>
                <MDBox component="form" pb={3} px={3}>
                  <Grid container spacing={1}>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.name}
                        name="name"
                        label="Department Name"
                        placeholder="Department Name"
                        onChange={handleChange}
                        error={errors.name}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.modality}
                        name="modality"
                        label="Modality"
                        placeholder="Modality"
                        onChange={handleChange}
                        error={errors.modality}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.location}
                        name="location"
                        label="Location"
                        placeholder="Location"
                        onChange={handleChange}
                        error={errors.location}
                      />
                    </Grid>
                    {/* <Grid item xs={12} sm={6}>
                      <Selector
                        label="Hospital"
                        options={hospitalSuperAdmin.map((item) => item.name)}
                        value={values.hospital}
                        onChange={(value) => setFieldValue("hospital", value)}
                      />
                    </Grid> */}
                  </Grid>
                </MDBox>
              </Card>
            </MDBox>
          </>
        )}
      </Formik>
      <Backdrop
        sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={openBackdrop}
      >
        <CircularProgress color="inherit" />
      </Backdrop>
    </BaseLayout>
  );
}
