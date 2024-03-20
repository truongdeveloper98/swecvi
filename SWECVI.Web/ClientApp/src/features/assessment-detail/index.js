/* eslint-disable no-unused-vars */
import BaseLayout from "components/Customized/BaseLayout";
import { useMaterialUIController } from "context";
import { Formik } from "formik";
import React from "react";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import * as Yup from "yup";
import { Backdrop, Card, CircularProgress, Grid } from "@mui/material";
import MDButton from "components/MDButton";
import MDBox from "components/MDBox";
import MDTypography from "components/MDTypography";
import FormField from "components/Customized/FormFiled";
import useAssessmentDetail from "./hooks/useAssessment";

const assessmentSchema = Yup.object().shape({
  descriptionReportText: Yup.string().required("Required"),
  callFunction: Yup.string().required("Required"),
  dCode: Yup.number().required("Required"),
  aCode: Yup.number().required("Required"),
});

export default function AssessmentDetail() {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const { t } = useTranslation();
  const params = useParams();

  const { handleCancel, assessment, handleSubmitForm, openBackdrop } = useAssessmentDetail();

  return (
    <BaseLayout>
      <Formik
        enableReinitialize
        initialValues={{
          descriptionReportText: assessment?.descriptionReportText,
          callFunction: assessment?.callFunction,
          dCode: assessment?.dCode,
          aCode: assessment?.aCode,
        }}
        validationSchema={assessmentSchema}
        onSubmit={(values) => {
          handleSubmitForm(values);
          // console.log(values);
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
                    {params?.id ? t("EditAssessment") : t("CreateAssessment")}
                  </MDTypography>
                </MDBox>
                <MDBox component="form" pb={3} px={3}>
                  <Grid container spacing={1}>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.descriptionReportText}
                        name="descriptionReportText"
                        label="Description Report Text"
                        placeholder="Description Report Text"
                        onChange={handleChange}
                        error={errors.descriptionReportText}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.callFunction}
                        name="callFunction"
                        label="Call Function"
                        placeholder="Call Function"
                        onChange={handleChange}
                        error={errors.callFunction}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.dCode}
                        type="number"
                        name="dCode"
                        label="DCode"
                        placeholder="DCode"
                        onChange={handleChange}
                        error={errors.dCode}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.aCode}
                        type="number"
                        name="aCode"
                        label="ACode"
                        placeholder="ACode"
                        onChange={handleChange}
                        error={errors.aCode}
                      />
                    </Grid>
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
