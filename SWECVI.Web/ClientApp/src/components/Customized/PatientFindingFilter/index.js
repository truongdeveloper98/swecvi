import { Card } from "@mui/material";
import MDBox from "components/MDBox";
import React from "react";
import MDButton from "components/MDButton";
import usePatientFindingFilters from "./hooks/usePatientFindingFilter";
import { patientFinding } from "./constants";

export default function PatientFindingFiltersCard() {
  const { filter, handleFilter } = usePatientFindingFilters();
  return (
    <MDBox>
      <Card>
        <MDBox overflow="auto" flexDirection="row" p={2}>
          {patientFinding.map(({ title, value }) => (
            <MDBox display="inline-block" m={0.5} key={`${title}-${value}`}>
              <MDButton
                onClick={() => handleFilter(title, value)}
                size="small"
                variant={filter === title ? "contained" : "outlined"}
                color="info"
              >
                {title}
              </MDButton>
            </MDBox>
          ))}
        </MDBox>
      </Card>
    </MDBox>
  );
}
