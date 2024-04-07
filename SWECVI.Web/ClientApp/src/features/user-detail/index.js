/* eslint-disable no-console */
/* eslint-disable no-unused-vars */
import React, { memo, useEffect, useState } from "react";
import MDBox from "components/MDBox";
import Card from "@mui/material/Card";
import Grid from "@mui/material/Grid";
import MDTypography from "components/MDTypography";
import { Formik } from "formik";
import { useMaterialUIController } from "context";
import MDButton from "components/MDButton";
import { useLocation, useParams } from "react-router-dom";
import * as Yup from "yup";
import REG_EXP from "constants/regExp";
import FormField from "components/Customized/FormFiled";
import BaseLayout from "components/Customized/BaseLayout";
import Checkbox from "@mui/material/Checkbox";
import {
  Autocomplete,
  Backdrop,
  CircularProgress,
  FormControlLabel,
  FormGroup,
  FormHelperText,
  TextField,
  Typography,
} from "@mui/material";
import { useTranslation } from "react-i18next";
import Selector from "components/Customized/Selector";
import { useSelector } from "react-redux";
import useUser from "./hooks/useUser";
import "./style.css";

const UserSchema = Yup.object().shape({
  firstName: Yup.string().min(2, "Too Short!").max(50, "Too Long!").required("Required"),
  lastName: Yup.string().min(2, "Too Short!").max(50, "Too Long!").required("Required"),
  passWord: Yup.string().min(2, "Too Short!").max(50, "Too Long!").required("Required"),
  email: Yup.string().email("Invalid email").required("Required"),
  phoneNumber: Yup.string()
    .matches(REG_EXP.phone, "Phone number is not valid")
    .required("Required"),
  roles: Yup.array().of(Yup.string()).min(1),
  department: Yup.mixed().required("Required"),
});

function UserDetail() {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const params = useParams();
  const { user, handleCancel, handleSubmitForm, decodedToken, openBackdrop } = useUser();
  const departments = useSelector((state) => state.department.departments);

  const { t } = useTranslation();

  return (
    <BaseLayout>
      <Formik
        enableReinitialize
        initialValues={{
          firstName: user?.firstName,
          lastName: user?.lastName,
          email: user?.email,
          phoneNumber: user?.phoneNumber,
          roles: user?.roles,
          passWord: user?.passWord,
          department: decodedToken?.[
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
          ].includes("HospitalAdmin")
            ? departments.items.map((item) => item.name)
            : [],
        }}
        validationSchema={UserSchema}
        onSubmit={(values) => {
          const data = {
            ...values,
          };
          let resp;
          if (
            decodedToken?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].includes(
              "HospitalAdmin"
            )
          ) {
            resp = {
              ...data,
              hospitalId: parseInt(localStorage.getItem("hospitalId").toString(), 10),
              indexDepartment: departments.items.find((item) => item.name === data.department)?.id,
            };
          } else if (
            decodedToken?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"].includes(
              "SuperAdmin"
            )
          ) {
            resp = {
              ...data,
              hospitalId: parseInt(localStorage.getItem("hospitalId").toString(), 10),
              roles: ["SuperAdmin"],
            };
          } else {
            resp = {
              ...data,
              hospitalId: parseInt(localStorage.getItem("hospitalId").toString(), 10),
            };
          }
          handleSubmitForm(resp);
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
              <MDButton onClick={handleSubmit} variant="gradient" color="info">
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
                    {params?.id ? t("EditUser") : t("CreateUser")}
                  </MDTypography>
                </MDBox>

                <MDBox component="form" pb={3} px={3}>
                  <Grid container spacing={1}>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.firstName}
                        name="firstName"
                        label={t("FirstName")}
                        placeholder="Alec"
                        onChange={handleChange}
                        error={errors.firstName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.lastName}
                        name="lastName"
                        label={t("LastName")}
                        placeholder="Thompson"
                        onChange={handleChange}
                        error={errors.lastName}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.passWord}
                        name="passWord"
                        label="Password"
                        type="password"
                        placeholder="enter password"
                        onChange={handleChange}
                        error={errors.passWord}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.email}
                        name="email"
                        label="Email"
                        placeholder="example@email.com"
                        inputProps={{ type: "email" }}
                        onChange={handleChange}
                        error={errors.email}
                      />
                    </Grid>
                    <Grid item xs={12} sm={6}>
                      <FormField
                        value={values.phoneNumber}
                        name="phoneNumber"
                        label={t("PhoneNumber")}
                        placeholder="+40 735 631 620"
                        onChange={handleChange}
                        error={errors.phoneNumber}
                      />
                    </Grid>
                    {(values.roles || !params?.id) &&
                      !decodedToken?.[
                        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                      ].includes("SuperAdmin") && (
                        <Grid
                          item
                          xs={12}
                          sm={6}
                          style={{
                            display: "flex",
                            flexDirection: "row",
                            alignItems: "center",
                            flexWrap: "wrap",
                          }}
                        >
                          <Typography fontSize={16} flexBasis="25%">
                            Roles
                          </Typography>
                          <FormGroup
                            style={{ display: "flex", flexDirection: "row", flexBasis: "75%" }}
                          >
                            {decodedToken?.[
                              "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                            ].includes("SuperAdmin") && (
                              <FormControlLabel
                                checked={values.roles?.includes("SuperAdmin")}
                                control={<Checkbox size="small" />}
                                label="Super Admin"
                                onChange={() => {
                                  let roles = values.roles ? [...values.roles] : [];
                                  if (roles.includes("SuperAdmin")) {
                                    roles = roles.filter((r) => r !== "SuperAdmin");
                                  } else {
                                    roles.push("SuperAdmin");
                                  }
                                  setFieldValue("roles", roles);
                                }}
                              />
                            )}
                            <FormControlLabel
                              checked={values.roles?.includes("HospitalAdmin")}
                              control={<Checkbox size="small" />}
                              label="Hospital Admin"
                              onChange={() => {
                                let roles = values.roles ? [...values.roles] : [];
                                if (roles.includes("HospitalAdmin")) {
                                  roles = roles.filter((r) => r !== "HospitalAdmin");
                                } else {
                                  roles.push("HospitalAdmin");
                                }
                                setFieldValue("roles", roles);
                              }}
                            />
                            <FormControlLabel
                              checked={values.roles?.includes("User")}
                              control={<Checkbox size="small" />}
                              label={t("User")}
                              onChange={() => {
                                let roles = values.roles ? [...values.roles] : [];
                                if (roles.includes("User")) {
                                  roles = roles.filter((r) => r !== "User");
                                } else {
                                  roles.push("User");
                                }
                                setFieldValue("roles", roles);
                              }}
                            />
                          </FormGroup>
                          {errors?.roles && <FormHelperText error>{errors?.roles}</FormHelperText>}
                        </Grid>
                      )}
                    {decodedToken?.[
                      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
                    ].includes("HospitalAdmin") && (
                      <Grid item xs={12} sm={6}>
                        <p>{typeof user?.indexDepartment === "number"}</p>
                        <Selector
                          disableClearable
                          key={user?.indexDepartment}
                          defaultValue={user?.indexDepartment}
                          label="Department"
                          options={departments.items.map((item) => item.name)}
                          value={
                            departments.items.find((item) => item.id === user?.indexDepartment)
                              ?.name
                          }
                          onChange={(value) => setFieldValue("department", value)}
                        />
                      </Grid>
                    )}
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

export default memo(UserDetail);
