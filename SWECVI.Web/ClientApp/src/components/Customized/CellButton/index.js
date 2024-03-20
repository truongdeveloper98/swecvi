import React, { useState } from "react";
import { Dialog, DialogActions, DialogTitle, Icon } from "@mui/material";
import { useMaterialUIController } from "context";
import MDButton from "components/MDButton";

function CellButton({ icon, title, onClick, confirmTitle, color = "secondary" }) {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;
  const [isOpen, setOpen] = useState(false);
  const handleClose = () => {
    setOpen(false);
  };

  const handleOpen = () => {
    setOpen(true);
  };

  return (
    <>
      <MDButton
        variant="text"
        onClick={confirmTitle ? handleOpen : onClick}
        size="small"
        color={color}
      >
        {icon && (<Icon>add</Icon>)`&nbsp;`}
        {title}
      </MDButton>

      {confirmTitle && (
        <Dialog
          open={isOpen}
          onClose={handleClose}
          aria-labelledby="alert-dialog-title"
          aria-describedby="alert-dialog-description"
          PaperProps={{
            style: {
              background: darkMode ? "#121212" : "#fff",
            },
          }}
        >
          <DialogTitle id="alert-dialog-title">{confirmTitle}</DialogTitle>
          <DialogActions>
            <MDButton
              color="error"
              onClick={() => {
                onClick();
                handleClose();
              }}
            >
              Yes
            </MDButton>
            <MDButton onClick={handleClose}>No</MDButton>
          </DialogActions>
        </Dialog>
      )}
    </>
  );
}

export default CellButton;
