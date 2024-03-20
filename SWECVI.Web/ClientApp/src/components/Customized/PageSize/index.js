import React from "react";
import { Autocomplete } from "@mui/material";
import MDInput from "components/MDInput";
import MDTypography from "components/MDTypography";
import { useTranslation } from "react-i18next";

function PageSize({ pageSize, onChange }) {
  const { t } = useTranslation();

  return (
    <>
      <Autocomplete
        disableClearable
        value={pageSize}
        options={[5, 10, 15, 20, 25]}
        getOptionLabel={(option) => String(option)}
        onChange={onChange}
        size="small"
        sx={{ width: "5rem" }}
        renderInput={(params) => <MDInput {...params} />}
      />
      <MDTypography pr={1} variant="caption" color="secondary">
        &nbsp;&nbsp;{t("EntriesPerPage")}
      </MDTypography>
    </>
  );
}

export default PageSize;
