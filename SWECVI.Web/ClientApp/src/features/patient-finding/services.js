/* eslint-disable no-shadow */
/* eslint-disable no-console */
import { store } from "stores";
import {
  failed,
  patientFindingBoxRequest,
  patientFindingBoxSuccess,
  setPatientCreateOrUpdate,
  setPatientFindingValueInput,
  succeed,
} from "stores/reducers/patient.reducer";
// import { convertDataStudyFinding } from "utils/convertData";
import { convertDataToInputV2 } from "utils/convertData";
import API from "./api";

export const getPatientFindingBox = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(patientFindingBoxRequest());
    const response = await API.findingStructure();
    const getStudyFindingResp = await API.getStudyFinding(params.id);

    if (response.data) {
      dispatch(patientFindingBoxSuccess(response.data));
    }
    if (getStudyFindingResp.data) {
      const tmp = convertDataToInputV2(getStudyFindingResp.data, {
        valueOnlyCheckBox: {},
        valueOnlyRadioButton: {},
        valueMultiSelect: {},
        valueOneSelect: {},
        valueNumricInput: {},
        valueTextInput: {},
        valueDateInput: {},
      });
      dispatch(setPatientCreateOrUpdate(getStudyFindingResp.data));
      dispatch(setPatientFindingValueInput(tmp));
    }
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const createStudyFinding = async (model) => {
  const { dispatch } = store;
  try {
    await API.studyFinding(model);
    dispatch(succeed("Study Finding created successfully"));
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

export const updateStudyFinding = async (params, FingdingStudyItems) => {
  const { dispatch } = store;
  try {
    await API.updateStudyFinding(params.hospitalId, {
      HospitalId: params.hospitalId,
      StudyId: params.id,
      FingdingStudyItems,
    });
    dispatch(succeed("Study Finding updated successfully"));
  } catch (error) {
    dispatch(failed(error?.response?.data));
  }
};

// export const updateDataValueInput = async (
//   params,
//   patientFindingValueInput,
//   patientFindingData
// ) => {
//   const { dispatch } = store;
//   try {
//     dispatch(setPatientFindingValueRequest());
//     if (patientFindingData.length > 0) {
//       await API.updateStudyFinding(params.hospitalId, {
//         HospitalId: params.hospitalId,
//         StudyId: params.id,
//         FingdingStudyItems: getValueInObject(patientFindingValueInput),
//       });
//     }
//     await API.studyFinding({
//       HospitalId: params.hospitalId,
//       StudyId: params.id,
//       FingdingStudyItems: getValueInObject(patientFindingValueInput),
//     });
//     dispatch(setPatientFindingValueSuccess());
//   } catch (error) {
//     dispatch(failed(error?.response?.data));
//   }
// };
