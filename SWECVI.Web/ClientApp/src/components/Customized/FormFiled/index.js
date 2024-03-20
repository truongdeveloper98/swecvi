import PropTypes from "prop-types";
import MDInput from "components/MDInput";

function FormField({ label, ...rest }) {
  return (
    <MDInput
      variant="standard"
      label={label}
      fullWidth
      InputLabelProps={{ shrink: true }}
      {...rest}
    />
  );
}

FormField.defaultProps = {
  label: " ",
};

FormField.propTypes = {
  label: PropTypes.string,
};

export default FormField;
