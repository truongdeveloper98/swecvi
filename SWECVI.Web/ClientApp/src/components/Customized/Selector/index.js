/* eslint-disable no-unused-expressions */
/* eslint-disable no-nested-ternary */
/* eslint-disable react/jsx-no-useless-fragment */
import React from "react";
import { Autocomplete, TextField } from "@mui/material";
import { useSelector } from "react-redux";

export default function Selector({
  multiple = false,
  hideLabel = false,
  disabled = false,

  options,
  optionsBoxFinding,
  tabName,
  onChange,
  property,
  label,
  ...rest
}) {
  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);

  return (
    <>
      {optionsBoxFinding ? (
        <Autocomplete
          disablePortal
          multiple={multiple}
          id="tags-standard"
          options={optionsBoxFinding && optionsBoxFinding.inputOptions}
          getOptionLabel={(option) => String(property ? option[property] ?? option : option)}
          onChange={(e, value) => onChange(optionsBoxFinding.id, label, value, tabName)}
          defaultValue={
            multiple
              ? patientFindingValueInput.valueMultiSelect[tabName]
                ? patientFindingValueInput.valueMultiSelect[tabName][label]?.value
                : []
              : patientFindingValueInput.valueOneSelect[tabName]
              ? patientFindingValueInput.valueOneSelect[tabName][label]?.value
              : ""
          }
          renderInput={(params) =>
            hideLabel ? (
              <TextField {...params} variant="standard" placeholder={label} />
            ) : (
              <TextField {...params} variant="standard" label={label} placeholder={label} />
            )
          }
          disabled={disabled}
          {...rest}
        />
      ) : (
        <Autocomplete
          disablePortal
          multiple={multiple}
          id="tags-standard"
          options={options}
          getOptionLabel={(option) => String(property ? option[property] ?? option : option)}
          onChange={(e, value) => onChange(value)}
          renderInput={(params) =>
            hideLabel ? (
              <TextField {...params} variant="standard" placeholder={label} />
            ) : (
              <TextField {...params} variant="standard" label={label} placeholder={label} />
            )
          }
          disabled={disabled}
          {...rest}
        />
      )}
    </>
  );
}
