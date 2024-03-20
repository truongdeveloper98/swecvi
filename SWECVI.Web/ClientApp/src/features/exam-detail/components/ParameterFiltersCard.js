import React, { useState } from "react";
import MDBox from "components/MDBox";
import { Card } from "@mui/material";
import MDButton from "components/MDButton";
import MDTypography from "components/MDTypography";
import { filters } from "../constants";

function ParameterFiltersCard({ onChange }) {
  const [filter, setFilter] = useState(filters[0].title);

  const handleFilter = (title, poh) => {
    setFilter(title);
    onChange(poh);
  };

  return (
    <MDBox>
      <Card>
        <MDBox p={2} pb={0}>
          <MDTypography variant="h6" fontWeight="medium">
            Parameter Filter
          </MDTypography>
        </MDBox>
        <MDBox overflow="auto" flexDirection="row" p={2}>
          {filters.map(({ title, poh }) => (
            <MDBox display="inline-block" m={0.5} key={title}>
              <MDButton
                onClick={() => handleFilter(title, poh)}
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

export default ParameterFiltersCard;
