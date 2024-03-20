import React from "react";
import MDBox from "components/MDBox";
import { Card } from "@mui/material";
import MDButton from "components/MDButton";
import MDTypography from "components/MDTypography";
import { useTranslation } from "react-i18next";
import usePeriodFilter from "./hooks/usePeriodFilter";
import { filters } from "./constants";

function PeriodFiltersCard() {
  const { filter, handleFilter } = usePeriodFilter();

  const { t } = useTranslation();

  return (
    <MDBox>
      <Card>
        <MDBox p={2} pb={0}>
          <MDTypography variant="h6" fontWeight="medium">
            {t("PatientsFilter")}
          </MDTypography>
        </MDBox>
        <MDBox overflow="auto" flexDirection="row" p={2}>
          {filters.map(({ title, value }) => (
            <MDBox display="inline-block" m={0.5} key={`${title}-${value}`}>
              <MDButton
                onClick={() => handleFilter(title, value)}
                size="small"
                variant={filter === title ? "contained" : "outlined"}
                color="info"
              >
                {t(`${title}`)}
              </MDButton>
            </MDBox>
          ))}
        </MDBox>
      </Card>
    </MDBox>
  );
}

export default PeriodFiltersCard;
