/* eslint-disable no-unused-vars */
import {
  Autocomplete,
  Backdrop,
  Card,
  CircularProgress,
  FormHelperText,
  Grid,
  TextField,
  TextareaAutosize,
  Typography,
} from "@mui/material";
import BaseLayout from "components/Customized/BaseLayout";
import FormField from "components/Customized/FormFiled";
import Selector from "components/Customized/Selector";
import MDBox from "components/MDBox";
import MDButton from "components/MDButton";
import MDTypography from "components/MDTypography";
import { useMaterialUIController } from "context";
import { Formik } from "formik";
import React, { useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import * as Yup from "yup";
import { useParams } from "react-router-dom";
import REG_EXP from "constants/regExp";
import useHospitalDetail from "./hooks/useHospitalDetail";

const hospitalSchema = Yup.object().shape({
  hospitalName: Yup.string()
    .matches(REG_EXP.hospitalName, "Hospital Name is not valid")
    .required("Required"),
  adminEmail: Yup.string().email().required("Required"),
  adminPassword: Yup.string().required("Required"),
});

const regions = [{ id: 1, value: "Sweden" }];

function HospitalDetail() {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const { t } = useTranslation();
  const params = useParams();
  const { hospital, handleCancel, handleSubmitForm, openBackdrop, regionValue } =
    useHospitalDetail();

  return (
    <BaseLayout>
      <Formik
        enableReinitialize
        initialValues={{
          hospitalName: hospital?.hospitalName,
          // description: hospital?.decription,
          adminEmail: !params?.id ? hospital?.adminEmail : "demo@gmail.com",
          adminPassword: !params?.id ? hospital?.adminPassword : "Asdfgh1@3",
          region: params?.id ? regionValue : "Sweden",
          connectionString: hospital?.connectionString,
        }}
        validationSchema={hospitalSchema}
        onSubmit={(values) => {
          const { hospitalName, adminEmail, adminPassword, region, connectionString } = values;

          const data = params?.id
            ? {
                hospitalName,
                indexRegion: regions.find((item) => item.value === region).id,
                connectionString,
              }
            : {
                hospitalName,
                adminEmail,
                adminPassword,
                indexRegion: regions.find((item) => item.value === region).id,
                connectionString: `Server=${
                  process.env?.REACT_APP_CONNECTION_IP
                };Database=${hospitalName.replace(
                  /\s/g,
                  ""
                )};Persist Security Info=True;User ID=sa;Password=Asdfgh1@3;TrustServerCertificate=true`,
              };
          handleSubmitForm(data);
          // console.log(data);
        }}
        // validateOnChange={false}
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
                    {params?.id ? t("EditHospital") : t("CreateHospital")}
                  </MDTypography>
                </MDBox>
                <MDBox component="form" pb={3} px={3}>
                  <Grid container spacing={1}>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.hospitalName}
                        name="hospitalName"
                        label="Hospital Name"
                        placeholder="Hospital Name"
                        onChange={handleChange}
                        error={errors.hospitalName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <Selector
                        label="Region"
                        options={regions.map((item) => item.value)}
                        value={values.region}
                        onChange={(value) => setFieldValue("region", value)}
                      />
                      {/* {errors?.region && <FormHelperText error>{errors?.region}</FormHelperText>} */}
                    </Grid>
                    {!params?.id && (
                      <Grid item xs={12} sm={6}>
                        <FormField
                          value={values.adminEmail}
                          name="adminEmail"
                          label="Admin Email"
                          placeholder="Admin Email"
                          onChange={handleChange}
                          error={errors.adminEmail}
                        />
                      </Grid>
                    )}
                    {!params?.id && (
                      <Grid item xs={12} sm={6}>
                        <FormField
                          value={values.adminPassword}
                          type="password"
                          name="adminPassword"
                          label="Admin Password"
                          placeholder="Admin Password"
                          onChange={handleChange}
                          error={errors.adminPassword}
                        />
                      </Grid>
                    )}
                    {params?.id && (
                      <Grid item xs={12}>
                        <FormField
                          value={values.connectionString}
                          name="connectionString"
                          label="Connection String"
                          placeholder="Connection String"
                          onChange={handleChange}
                          error={errors.connectionString}
                        />
                      </Grid>
                    )}
                    {/* 
                    <Grid item xs={12}>
                      <Typography fontSize={14} color="#7b809a">
                        Description
                      </Typography>
                      <TextareaAutosize
                        aria-label="minimum height"
                        style={{
                          width: "100%",
                          borderRadius: "8px",
                          border: "1px solid #C7D0DD",
                          padding: "5px",
                        }}
                        minRows={3}
                        value={values.description}
                        name="description"
                        placeholder="Thompson"
                        onChange={handleChange}
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

export default HospitalDetail;
