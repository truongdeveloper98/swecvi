import { combineReducers } from "redux";
import { LOGOUT } from "constants/actionTypes";

import storage from "redux-persist/lib/storage";
import auth from "./auth.reducer";
import patient from "./patient.reducer";
import common from "./common.reducer";
import analytic from "./analytic.reducer";
import user from "./user.reducer";
import statistic from "./statistics.reducer";
import session from "./session.reducer";
import parameter from "./parameter.reducer";
import python from "./python.reducer";
import exam from "./exam.reducer";
import hospital from "./hospital.reducer";
import department from "./department.reducer";
import references from "./references.reducer";
import assessment from "./assessment.reducer";
import settings from "./settings.reducer";

const appReducer = combineReducers({
  auth,
  patient,
  common,
  analytic,
  user,
  statistic,
  session,
  parameter,
  python,
  exam,
  hospital,
  department,
  references,
  assessment,
  settings,
});

const rootReducer = (state, action) => {
  if (action.type === LOGOUT) {
    storage.removeItem("persist:root");
    return appReducer(undefined, action);
  }
  return appReducer(state, action);
};

export default rootReducer;
