import { useState, useEffect, useMemo } from "react";
import { Routes, Route, useLocation, useNavigate } from "react-router-dom";
import { isExpired } from "react-jwt";
import { ThemeProvider } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import Sidenav from "examples/Sidenav";
import theme from "assets/theme";
import themeRTL from "assets/theme/theme-rtl";
import themeDark from "assets/theme-dark";
import themeDarkRTL from "assets/theme-dark/theme-rtl";
import rtlPlugin from "stylis-plugin-rtl";
import { CacheProvider } from "@emotion/react";
import createCache from "@emotion/cache";
import privateRoutes from "navigation/privateRoutes";
import { useMaterialUIController } from "context";
import brandWhite from "assets/images/logo-ct.png";
import brandDark from "assets/images/logo-ct-dark.png";
import publicRoutes from "navigation/publicRoutes";
import PAGES from "navigation/pages";
import { useSelector } from "react-redux";
import Snackbar from "components/Customized/Snackbar";
import ReactLoading from "react-loading";
import MDBox from "components/MDBox";

export default function App() {
  const navigate = useNavigate();
  const location = useLocation();
  const [controller] = useMaterialUIController();
  const {
    miniSidenav,
    direction,
    layout,
    sidenavColor,
    transparentSidenav,
    whiteSidenav,
    darkMode,
  } = controller;
  const [onMouseEnter, setOnMouseEnter] = useState(false);
  const [rtlCache, setRtlCache] = useState(null);
  const { pathname } = useLocation();
  const token = useSelector((state) => state.auth.token);
  const isLoading = useSelector((state) => state.common.isLoading);

  // Cache for the rtl
  useMemo(() => {
    const cacheRtl = createCache({
      key: "rtl",
      stylisPlugins: [rtlPlugin],
    });

    setRtlCache(cacheRtl);
  }, []);

  // Open sidenav when mouse enter on mini sidenav
  const handleOnMouseEnter = () => {
    if (miniSidenav && !onMouseEnter) {
      // setMiniSidenav(dispatch, false);
      setOnMouseEnter(true);
    }
  };

  // Close sidenav when mouse leave mini sidenav
  const handleOnMouseLeave = () => {
    if (onMouseEnter) {
      // setMiniSidenav(dispatch, true);
      setOnMouseEnter(false);
    }
  };

  // Setting the dir attribute for the body element
  useEffect(() => {
    document.body.setAttribute("dir", direction);
  }, [direction]);

  // Setting page scroll to 0 when changing the route
  useEffect(() => {
    document.documentElement.scrollTop = 0;
    document.scrollingElement.scrollTop = 0;
  }, [pathname]);

  useEffect(() => {
    if (token && !isExpired(token)) {
      if (
        location.pathname.includes(PAGES.editExam) ||
        location.pathname.includes(PAGES.editUser)
      ) {
        navigate(location.pathname);
        return;
      }
      navigate(PAGES.analytics);
    }
  }, []);

  const getRoutes = (allRouter) =>
    allRouter.map((route) => {
      if (!token && location.pathname !== PAGES.signIn) {
        navigate(PAGES.signIn);
        return null;
      }
      if (route.collapse) {
        return getRoutes(route.collapse);
      }
      if (route.route) {
        return <Route exact path={route.route} element={route.component} key={route.key} />;
      }
      return null;
    });

  const renderContent = (contentTheme) => (
    <ThemeProvider theme={contentTheme}>
      <CssBaseline />
      {layout === "dashboard" && (
        <Sidenav
          color={sidenavColor}
          brand={(transparentSidenav && !darkMode) || whiteSidenav ? brandDark : brandWhite}
          brandName="SWECVI"
          routes={privateRoutes}
          onMouseEnter={handleOnMouseEnter}
          onMouseLeave={handleOnMouseLeave}
          token={token}
        />
      )}
      <Routes>
        {publicRoutes.map((route) => (
          <Route key={route.key} path={route.route} exact={route.exact} element={route.component} />
        ))}
        {getRoutes(privateRoutes)}
      </Routes>

      {/* snacks */}
      <Snackbar />

      {isLoading && (
        <MDBox
          display="flex"
          justifyContent="center"
          alignItems="center"
          position="absolute"
          zIndex="10000"
          width="100%"
          height="100%"
          top="0"
          bgColor="#00000080"
        >
          <ReactLoading color="black" type="spin" width="100px" />
        </MDBox>
      )}
    </ThemeProvider>
  );

  return direction === "rtl" ? (
    <CacheProvider value={rtlCache}>
      {renderContent(darkMode ? themeDarkRTL : themeRTL)}
    </CacheProvider>
  ) : (
    renderContent(darkMode ? themeDark : theme)
  );
}
