import React from "react";
import ReactLoading from "react-loading";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import { persistor, store } from "stores";
import { combineProviders } from "utils/combineProviders";
import { MaterialUIControllerProvider } from "context";

function ReduxProvider(props) {
  return <Provider store={store} {...props} />;
}

function Persist(props) {
  return <PersistGate persistor={persistor} loading={<ReactLoading type="cylon" />} {...props} />;
}

export const AppProviders = ({ children }) =>
  combineProviders([MaterialUIControllerProvider, ReduxProvider, Persist], children);
