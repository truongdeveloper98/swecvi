import React from "react";
import CellButton from "components/Customized/CellButton";

function DeleteButton({ data, confirmTitle, onClick }) {
  return (
    <CellButton
      color="error"
      title="Delete"
      confirmTitle={confirmTitle}
      onClick={() => {
        onClick(data);
      }}
    />
  );
}

export default DeleteButton;
