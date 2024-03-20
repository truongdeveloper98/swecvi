/* eslint-disable no-console */
import API from "features/patient-finding/api";
import { updateStudyFinding } from "features/patient-finding/services";
import { useState } from "react";

import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";

import { getValueInObject } from "utils/convertData";

// const { useState } = require("react");

const useAllmant = () => {
  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);
  const params = useParams();
  const [openSuccess, setOpenSuccess] = useState(false);
  const [openError, setOpenError] = useState(false);
  // const [valueAllmant, setValueAllmant] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  // const [valueOrsak, setValueOrsak] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  // const [valueRytm, setValueRytm] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  // const [valueBedömningStresstest, setBedömningStresstest] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  const handleBtnSave = async () => {
    // console.log(params);
    await updateStudyFinding(params, getValueInObject(patientFindingValueInput))
      .then(() => {
        setOpenSuccess(true);
        setOpenError(false);
      })
      .catch(() => {
        setOpenError(true);
        setOpenSuccess(false);
      });

    await API.studyFinding({
      HospitalId: params.hospitalId,
      StudyId: params.id,
      FingdingStudyItems: getValueInObject(patientFindingValueInput),
    })
      .then(() => {
        setOpenSuccess(true);
        setOpenError(false);
      })
      .catch(() => {
        setOpenError(true);
        setOpenSuccess(false);
      });
    setOpenError(false);
    setOpenSuccess(false);
  };

  return {
    // valueAllmant,
    // setValueAllmant,

    // valueOrsak,
    // setValueOrsak,

    // valueRytm,
    // setValueRytm,

    // valueBedömningStresstest,
    // setBedömningStresstest,

    handleBtnSave,
    openSuccess,
    openError,
  };
};

export default useAllmant;
