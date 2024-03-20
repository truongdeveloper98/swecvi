import { useState, forwardRef, useImperativeHandle } from "react";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import FormField from "components/Customized/FormFiled";
import { Formik } from "formik";
import * as Yup from "yup";
import { Grid } from "@mui/material";
import { useMaterialUIController } from "context";
import MDButton from "components/MDButton";

const SessionSchema = Yup.object().shape({
  name: Yup.string().required("Required"),
});

const FormDialog = forwardRef(({ onCreate, onUpdate }, ref) => {
  const [controller] = useMaterialUIController();
  const { darkMode } = controller;

  const [open, setOpen] = useState(false);
  const [init, setInit] = useState(undefined);
  const [entity, setEntity] = useState(undefined);

  useImperativeHandle(ref, () => ({
    setEntity: (field) => {
      setEntity(field);
    },
    entity: () => entity,
    setInit: (data) => {
      setInit(data);
    },
    handleOpen: () => setOpen(true),
  }));

  const handleClose = () => {
    setOpen(false);
    setInit(undefined);
    setEntity(undefined);
  };

  return (
    <Dialog
      open={open}
      onClose={handleClose}
      PaperProps={{
        style: {
          background: darkMode ? "#121212" : "#fff",
        },
      }}
    >
      <DialogTitle>
        {init ? "Edit" : "Create"} {entity}
      </DialogTitle>

      <Formik
        enableReinitialize
        initialValues={{
          name: init?.name ?? "",
          english: init?.english ?? "",
          swedish: init?.swedish ?? "",
        }}
        validationSchema={SessionSchema}
        onSubmit={(values) => {
          if (init?.id) {
            onUpdate(init.id, values);
          } else {
            onCreate(values);
          }
          handleClose();
        }}
      >
        {({ values, errors, handleChange, handleSubmit, dirty, isValid }) => (
          <>
            <DialogContent>
              <Grid container spacing={1}>
                <Grid item xs={12}>
                  <FormField
                    value={values.name}
                    name="name"
                    label={`${entity} Name`.trim()}
                    placeholder={`${entity} Name`.trim()}
                    onChange={handleChange}
                    error={errors.name}
                  />
                </Grid>
                <Grid item xs={12}>
                  <FormField
                    value={values.english}
                    name="english"
                    label="English Label"
                    placeholder="English Label"
                    onChange={handleChange}
                  />
                </Grid>
                <Grid item xs={12}>
                  <FormField
                    value={values.swedish}
                    name="swedish"
                    label="Swedish Label"
                    placeholder="Swedish Label"
                    onChange={handleChange}
                  />
                </Grid>
              </Grid>
            </DialogContent>
            <DialogActions>
              <MDButton onClick={handleClose}>Cancel</MDButton>
              <MDButton color="info" disabled={!dirty || !isValid} onClick={handleSubmit}>
                {init ? "Update" : "Create"}
              </MDButton>
            </DialogActions>
          </>
        )}
      </Formik>
    </Dialog>
  );
});

export default FormDialog;
