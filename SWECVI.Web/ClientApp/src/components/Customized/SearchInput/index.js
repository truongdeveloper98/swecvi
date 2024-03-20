import MDInput from "components/MDInput";
import React, { useCallback, useEffect, useState } from "react";
import debounce from "lodash/debounce";

function SearchInput({ value, onChange, label, fullWidth = false }) {
  const [text, setText] = useState(value);
  const debounced = useCallback(
    debounce((val) => onChange(val), 500),
    []
  );

  useEffect(() => {
    debounced(text);
  }, [text]);

  return (
    <MDInput
      fullWidth={fullWidth}
      value={text}
      type="search"
      label={label}
      onChange={({ target }) => {
        setText(target.value);
      }}
    />
  );
}

export default SearchInput;
