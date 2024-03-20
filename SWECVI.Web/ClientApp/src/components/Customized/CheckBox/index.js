/* eslint-disable no-nested-ternary */
import { Checkbox, FormControl, FormControlLabel } from "@mui/material";
import React from "react";
import { useSelector } from "react-redux";

export default function CheckBox({ inputLabel, checkBoxData, onChangeValue, tabName }) {
  const { id, inputOptions } = checkBoxData;
  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);

  // console.log("111", patientFindingValueInput.valueOnlyCheckBox);

  return (
    <FormControl sx={{ display: "flex", flexDirection: "row", flexWrap: "wrap" }}>
      {inputOptions &&
        inputOptions.length > 0 &&
        inputOptions.map((item) => (
          <FormControlLabel
            control={
              <Checkbox
                checked={
                  patientFindingValueInput.valueOnlyCheckBox[tabName]
                    ? patientFindingValueInput.valueOnlyCheckBox[tabName][inputLabel]
                      ? patientFindingValueInput.valueOnlyCheckBox[tabName][inputLabel][item]
                      : false
                    : false || false
                }
                onChange={() => onChangeValue(id, inputLabel, item, tabName)}
              />
            }
            sx={{ display: "flex", justifyContent: "center" }}
            label={item}
            key={item}
          />
        ))}
    </FormControl>
  );
}
