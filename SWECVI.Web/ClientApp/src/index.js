import React from "react";
import App from "App";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";

import "ag-grid-enterprise";
import "ag-grid-community/dist/styles/ag-grid.css";
import "ag-grid-community/dist/styles/ag-theme-alpine.css";
import "ag-grid-community/dist/styles/ag-theme-alpine-dark.css";
import "custom-styles.css";

// Soft UI Context Provider
import { StyledEngineProvider } from "@mui/material/styles";
import { AppProviders } from "./AppProviders";

import "./i18n";

const root = ReactDOM.createRoot(document.getElementById("root"));

root.render(
  <BrowserRouter>
    <AppProviders>
      <StyledEngineProvider injectFirst>
        <App />
      </StyledEngineProvider>
    </AppProviders>
  </BrowserRouter>
);
