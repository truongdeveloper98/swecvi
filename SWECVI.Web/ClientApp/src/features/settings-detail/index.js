/* eslint-disable react/no-unstable-nested-components */
/* eslint-disable no-unused-vars */
import {
  Backdrop,
  Card,
  CircularProgress,
  FormControlLabel,
  FormGroup,
  Grid,
  Switch,
} from "@mui/material";
import BaseLayout from "components/Customized/BaseLayout";
import FormField from "components/Customized/FormFiled";
import Selector from "components/Customized/Selector";
import MDBox from "components/MDBox";
import MDButton from "components/MDButton";
import MDTypography from "components/MDTypography";
import { useMaterialUIController } from "context";
import { Formik } from "formik";
import React from "react";
import { useTranslation } from "react-i18next";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import useSettingsDetail from "./hooks/useSettingsDetail";

const functionSelector = [
  { name: "MIN", value: 1 },
  { name: "AVG", value: 2 },
  { name: "MAX", value: 3 },
  { name: "LATEST", value: 4 },
];

export default function ParameterSettingsDetail() {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const { t } = useTranslation();

  const params = useParams();
  const departments = useSelector((state) => state.department.departments);

  const { handleCancel, settings, handleSubmitForm, openBackdrop } = useSettingsDetail();

  return (
    <BaseLayout>
      <Formik
        enableReinitialize
        initialValues={{
          parameterId: settings?.parameterId,
          showInChart: settings?.showInChart,
          showInParameterTable: settings?.showInParameterTable,
          showInAssessmentText: settings?.showInAssessmentText,
          tableFriendlyName: settings?.tableFriendlyName,
          textFriendlyName: settings?.textFriendlyName,
          parameterHeader: settings?.parameterHeader,
          parameterSubHeader: settings?.parameterSubHeader,
          parameterOrder: settings?.parameterOrder,
          parameterHeaderOrder: settings?.parameterHeaderOrder,
          displayDecimal: settings?.displayDecimal,
          poh: settings?.poh,
          description: settings?.description,
          functionSelector: functionSelector.find(
            (item) => item.value === settings?.functionSelector
          )?.name,
          departmentName: settings?.departmentName,
        }}
        onSubmit={(values) => {
          // console.log({
          //   ...values,
          //   functionSelector: functionSelector.find((item) => item.name === values.functionSelector)
          //     ?.value,
          //   departmentId: departments.items.find((item) => item.name === values.departmentName)?.id,
          // });
          handleSubmitForm({
            ...values,
            functionSelector: functionSelector.find((item) => item.name === values.functionSelector)
              ?.value,
            departmentId: departments.items.find((item) => item.name === values.departmentName)?.id,
            showInChart: values.showInChart || false,
            showInParameterTable: values.showInParameterTable || false,
            showInAssessmentText: values.showInAssessmentText || false,
          });
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
                  <MDTypography variant="h5">{t("Settings")}</MDTypography>
                </MDBox>
                <MDBox component="form" pb={3} px={3}>
                  <Grid container spacing={1}>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.parameterId}
                        name="parameterId"
                        label="Parameter Id"
                        placeholder="Parameter Id"
                        onChange={handleChange}
                        error={errors.parameterId}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.tableFriendlyName}
                        name="tableFriendlyName"
                        label="Table Friendly Name"
                        placeholder="Table Friendly Name"
                        onChange={handleChange}
                        error={errors.tableFriendlyName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.textFriendlyName}
                        name="textFriendlyName"
                        label="Text Friendly Name"
                        placeholder="Text Friendly Name"
                        onChange={handleChange}
                        error={errors.textFriendlyName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.parameterHeader}
                        name="parameterHeader"
                        label="Parameter Header"
                        placeholder="Parameter Header"
                        onChange={handleChange}
                        error={errors.parameterHeader}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.parameterSubHeader}
                        name="parameterSubHeader"
                        label="Parameter Sub Header"
                        placeholder="Parameter Sub Header"
                        onChange={handleChange}
                        error={errors.parameterSubHeader}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.displayDecimal}
                        type="number"
                        name="displayDecimal"
                        label="Display Decimal"
                        placeholder="Display Decimal"
                        onChange={handleChange}
                        error={errors.displayDecimal}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.parameterOrder}
                        type="number"
                        name="parameterOrder"
                        label="Parameter Order"
                        placeholder="Parameter Order"
                        onChange={handleChange}
                        error={errors.parameterOrder}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.parameterHeaderOrder}
                        type="number"
                        name="parameterHeaderOrder"
                        label="Parameter Header Order"
                        placeholder="Parameter Header Order"
                        onChange={handleChange}
                        error={errors.parameterHeaderOrder}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.poh}
                        name="poh"
                        label="POH"
                        placeholder="POH"
                        onChange={handleChange}
                        error={errors.poh}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.description}
                        name="description"
                        label="Description"
                        placeholder="Description"
                        onChange={handleChange}
                        error={errors.description}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <Selector
                        // disableClearable
                        label="Function Selector"
                        options={functionSelector.map((item) => item.name)}
                        onChange={(value) => setFieldValue("functionSelector", value)}
                        value={values.functionSelector}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <Selector
                        defaultValue={values.departmentName}
                        key={values.departmentName}
                        label="Department Name"
                        options={departments.items.map((item) => item.name)}
                        onChange={(value) => setFieldValue("departmentName", value)}
                        value={values.departmentName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormControlLabel
                        label="Show In Chart"
                        control={
                          <Switch
                            checked={values.showInChart}
                            onChange={(e) => setFieldValue("showInChart", e.target.checked)}
                            color="primary"
                            name="showInChart"
                          />
                        }
                        // labelPlacement="start"
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormControlLabel
                        label="Show In Parameter Table"
                        control={
                          <Switch
                            checked={values.showInParameterTable}
                            onChange={(e) =>
                              setFieldValue("showInParameterTable", e.target.checked)
                            }
                            color="primary"
                            name="showInParameterTable"
                          />
                        }
                        // labelPlacement="start"
                      />
                    </Grid>

                    <Grid item xs={12} sm={6}>
                      <FormControlLabel
                        label="Show In Assessment Text"
                        control={
                          <Switch
                            checked={values.showInAssessmentText}
                            onChange={(e) =>
                              setFieldValue("showInAssessmentText", e.target.checked)
                            }
                            color="primary"
                            name="showInAssessmentText"
                          />
                        }
                        // labelPlacement="start"
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
