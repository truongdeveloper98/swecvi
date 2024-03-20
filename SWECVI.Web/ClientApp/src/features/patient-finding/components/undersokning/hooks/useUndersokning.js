// import API from "features/patient-finding/api";

const useUndersokning = () => {
  // const [valueKontrast, setValueKontrast] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  // const [valueTEE, setValueTEE] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  // const [valueStresstest1, setValueStresstest1] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  // const [valueStresstest2, setValueStresstest2] = useState({
  //   valueOnlyCheckBox: {},
  //   valueOnlyRadioButton: {},
  //   valueMultiSelect: {},
  //   valueOneSelect: {},
  //   valueNumricInput: {},
  //   valueTextInput: {},
  //   valueDateInput: {},
  // });

  const handleBtnSave = async () => {
    // await API.studyFinding({
    //   HospitalId: params.hospitalId,
    //   StudyId: params.id,
    //   FingdingStudyItems: getValueInObject(
    //     objectsWithData(mergeObjects(valueKontrast, valueTEE, valueStresstest1, valueStresstest2))
    //   ),
    // });
  };

  return {
    handleBtnSave,
  };
};

export default useUndersokning;
