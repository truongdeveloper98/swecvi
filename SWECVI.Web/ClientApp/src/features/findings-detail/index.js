/* eslint-disable no-unused-vars */
import { Backdrop, Card, CircularProgress, Grid } from "@mui/material";
import BaseLayout from "components/Customized/BaseLayout";
import MDBox from "components/MDBox";
import MDButton from "components/MDButton";
import { useMaterialUIController } from "context";
import { Formik } from "formik";
import React from "react";
import { useTranslation } from "react-i18next";
import { useParams } from "react-router-dom";
import MDTypography from "components/MDTypography";
import FormField from "components/Customized/FormFiled";
import useFindingsDetail from "./hooks/useFindingsDetail";

export default function FindingDetail() {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const { t } = useTranslation();
  const params = useParams();

  const { finding, handleCancel, handleSubmitForm, openBackdrop } = useFindingsDetail();
  return (
    <BaseLayout>
      <Formik
        enableReinitialize
        initialValues={{
          rowOrder: finding?.rowOrder,
          tabName: finding?.tabName,
          boxHeader: finding?.boxHeader,
          inputLabel: finding?.inputLabel,
          inputType: finding?.inputType,
          inputOptions: finding?.inputOptions,
          orderInReport: finding?.orderInReport,
        }}
        // validationSchema={referencesSchema}
        onSubmit={(values) => {
          handleSubmitForm(values);
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
                    {params?.id ? t("EditReferences") : t("CreateReferences")}
                  </MDTypography>
                </MDBox>
                <MDBox component="form" pb={3} px={3}>
                  <Grid container spacing={1}>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.rowOrder}
                        name="rowOrder"
                        label="RowOrder"
                        placeholder="rowOrder"
                        onChange={handleChange}
                        error={errors.rowOrder}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.tabName}
                        name="tabName"
                        label="Tab Name"
                        placeholder="Tab Name"
                        onChange={handleChange}
                        error={errors.tabName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.boxHeader}
                        name="boxHeader"
                        label="Box Header"
                        placeholder="Box Header"
                        onChange={handleChange}
                        error={errors.boxHeader}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.inputLabel}
                        name="inputLabel"
                        label="Input Label"
                        placeholder="Input Label"
                        onChange={handleChange}
                        error={errors.inputLabel}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.inputType}
                        name="inputType"
                        label="Input Type"
                        placeholder="Input Type"
                        onChange={handleChange}
                        error={errors.inputType}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.inputOptions}
                        name="inputOptions"
                        label="Input Options"
                        placeholder="Input Options"
                        onChange={handleChange}
                        error={errors.inputOptions}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.orderInReport}
                        type="number"
                        name="orderInReport"
                        label="Order In Report"
                        placeholder="Order In Report"
                        onChange={handleChange}
                        error={errors.orderInReport}
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
