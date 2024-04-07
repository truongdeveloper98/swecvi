import React from "react";
import DeleteIcon from "@mui/icons-material/Delete";
import CellButton from "components/Customized/CellButton";

function DeleteButton({ data, confirmTitle, onClick }) {
  return (
    <CellButton
      color="error"
      title={<DeleteIcon />}
      confirmTitle={confirmTitle}
      onClick={() => {
        onClick(data);
      }}
    />
  );
}

export default DeleteButton;
