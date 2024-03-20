import React from "react";
import Checkbox from "@mui/material/Checkbox";

export default function CellCheckbox({ value, data, onChange, colDef }) {
  const handleChangeActiveStatue = (e) => {
    const nextValue = e.target.checked;
    onChange(data.id, { ...data, [colDef.field]: nextValue });
  };

  return <Checkbox checked={value} onChange={handleChangeActiveStatue} />;
}
