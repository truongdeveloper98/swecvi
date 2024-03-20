import React from "react";
import CellButton from "components/Customized/CellButton";

function EditButton({ icon, data, onClick }) {
  return (
    <CellButton
      icon={icon}
      color="info"
      title="Edit"
      onClick={() => {
        onClick(data);
      }}
    />
  );
}

export default EditButton;
