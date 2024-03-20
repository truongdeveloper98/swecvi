import { store } from "stores";
import { failed, requested, settingsSuccess } from "stores/reducers/settings.reducer";
import API from "./api";

export const getSettingsRequest = async (params) => {
  const { dispatch } = store;
  try {
    dispatch(requested());
    const response = await API.settingss(params);
    if (response.data) {
      dispatch(settingsSuccess(response.data));
    }
  } catch (error) {
    dispatch(failed(error));
  }
};
