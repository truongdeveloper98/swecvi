import { useDispatch, useSelector } from "react-redux";
import { setPatientFindingValueInput } from "stores/reducers/patient.reducer";

const useBoxFinding = () => {
  const dispatch = useDispatch();

  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);

  const handleCheckboxChange = (id, label, value, tabName) => {
    dispatch(
      setPatientFindingValueInput({
        ...patientFindingValueInput,
        valueOnlyCheckBox: {
          ...patientFindingValueInput.valueOnlyCheckBox,
          [tabName]: {
            ...patientFindingValueInput.valueOnlyCheckBox[tabName],
            [label]: {
              ...(patientFindingValueInput.valueOnlyCheckBox[tabName]
                ? patientFindingValueInput.valueOnlyCheckBox[tabName][label]
                : {} || {}),
              [value]: !patientFindingValueInput.valueOnlyCheckBox[tabName]?.[label]?.[value],
              id,
            },
          },
        },
      })
    );
  };

  const handleChangeRadioButton = (id, lable, value, tabName) => {
    // boxChangeValue((prevValues) => ({
    //   ...prevValues,
    //   valueOnlyRadioButton: {
    //     ...patientFindingValueInput.valueOnlyRadioButton,
    //     [lable]: { value, id },
    //   },
    // }));
    dispatch(
      setPatientFindingValueInput({
        ...patientFindingValueInput,
        valueOnlyRadioButton: {
          ...patientFindingValueInput.valueOnlyRadioButton,
          [tabName]: {
            ...patientFindingValueInput.valueOnlyRadioButton[tabName],
            [lable]: { value, id },
          },
        },
      })
    );
  };

  const handleChangeMutiSelector = (id, label, value, tabName) => {
    // boxChangeValue((prevValues) => ({
    //   ...prevValues,
    //   valueMultiSelect: { ...prevValues.valueMultiSelect, [label]: { value, id } },
    // }));
    dispatch(
      setPatientFindingValueInput({
        ...patientFindingValueInput,
        valueMultiSelect: {
          ...patientFindingValueInput.valueMultiSelect,
          [tabName]: {
            ...patientFindingValueInput.valueMultiSelect[tabName],
            [label]: { value: value || "", id },
          },
        },
      })
    );
  };

  const handleChangeSelector = (id, label, value, tabName) => {
    // boxChangeValue((prevValues) => ({
    //   ...prevValues,
    //   valueOneSelect: { ...prevValues.valueOneSelect, [label]: { value, id } },
    // }));
    dispatch(
      setPatientFindingValueInput({
        ...patientFindingValueInput,
        valueOneSelect: {
          ...patientFindingValueInput.valueOneSelect,
          [tabName]: {
            ...patientFindingValueInput.valueOneSelect[tabName],
            [label]: { value: value || "", id },
          },
        },
      })
    );
  };

  const handleChangeInputNumber = (id, label, value, tabName) => {
    // boxChangeValue((prevValues) => ({
    //   ...prevValues,
    //   valueNumricInput: { ...prevValues.valueNumricInput, [label]: { value, id } },
    // }));
    dispatch(
      setPatientFindingValueInput({
        ...patientFindingValueInput,
        valueNumricInput: {
          ...patientFindingValueInput.valueNumricInput,
          [tabName]: {
            ...patientFindingValueInput.valueNumricInput[tabName],
            [label]: { value, id },
          },
        },
      })
    );
  };

  const handleChangeInputText = (id, label, value, tabName) => {
    // boxChangeValue((prevValues) => ({
    //   ...prevValues,
    //   valueTextInput: { ...prevValues.valueTextInput, [label]: { value, id } },
    // }));
    dispatch(
      setPatientFindingValueInput({
        ...patientFindingValueInput,
        valueTextInput: {
          ...patientFindingValueInput.valueTextInput,
          [tabName]: {
            ...patientFindingValueInput.valueTextInput[tabName],
            [label]: { value, id },
          },
        },
      })
    );
  };

  const handleChangeInputDate = (id, label, value, tabName) => {
    // boxChangeValue((prevValues) => ({
    //   ...prevValues,
    //   valueDateInput: { ...prevValues.valueDateInput, [label]: { value, id } },
    // }));
    dispatch(
      setPatientFindingValueInput({
        ...patientFindingValueInput,
        valueDateInput: {
          ...patientFindingValueInput.valueDateInput,
          [tabName]: {
            ...patientFindingValueInput.valueDateInput[tabName],
            [label]: { value, id },
          },
        },
      })
    );
  };
  return {
    handleCheckboxChange,

    handleChangeRadioButton,

    handleChangeMutiSelector,

    handleChangeSelector,

    handleChangeInputNumber,

    handleChangeInputText,

    handleChangeInputDate,
  };
};

export default useBoxFinding;
