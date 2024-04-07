import React from "react";
import EditIcon from "@mui/icons-material/Edit";
import CellButton from "components/Customized/CellButton";

function EditButton({ data, onClick }) {
  return (
    <CellButton
      title={<EditIcon />}
      color="info"
      onClick={() => {
        onClick(data);
      }}
    />
  );
}

export default EditButton;
