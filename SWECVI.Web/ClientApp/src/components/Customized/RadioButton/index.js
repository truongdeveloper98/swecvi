import { FormControlLabel, Radio, RadioGroup } from "@mui/material";
import React from "react";
import { useSelector } from "react-redux";

export default function RadioButton({ label, radioBoxData, onChangeValue, tabName }) {
  const { id, inputOptions } = radioBoxData;
  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);

  return (
    <RadioGroup
      aria-labelledby="demo-radio-buttons-group-label"
      value={
        patientFindingValueInput.valueOnlyRadioButton[tabName]
          ? patientFindingValueInput.valueOnlyRadioButton[tabName][label]?.value
          : null || null
      }
      name="radio-buttons-group"
      row
      onChange={(e) => onChangeValue(id, label, e.target.value, tabName)}
      sx={{ display: "flex", flexDirection: "row", alignItems: "center" }}
    >
      {inputOptions &&
        inputOptions.map((item) => (
          <FormControlLabel key={`${item}`} value={item} control={<Radio />} label={item} />
        ))}
    </RadioGroup>
  );
}
