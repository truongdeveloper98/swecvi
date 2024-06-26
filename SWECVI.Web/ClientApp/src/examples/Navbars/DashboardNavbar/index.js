/* eslint-disable import/no-extraneous-dependencies */
import { useState, useEffect } from "react";
import { NavLink, useLocation } from "react-router-dom";
import PropTypes from "prop-types";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Icon from "@mui/material/Icon";
import MDBox from "components/MDBox";
import Breadcrumbs from "examples/Breadcrumbs";

import {
  navbar,
  navbarContainer,
  navbarRow,
  navbarIconButton,
  navbarDesktopMenu,
  navbarMobileMenu,
} from "examples/Navbars/DashboardNavbar/styles";

import {
  useMaterialUIController,
  setTransparentNavbar,
  setMiniSidenav,
  setDarkMode,
  setLanguage,
} from "context";
import { Menu, MenuItem, Switch } from "@mui/material";
import { useTranslation } from "react-i18next";
import { LANGUAGES } from "constants/language";
import LanguageIcon from "@mui/icons-material/Language";
import routes from "navigation/privateRoutes";

function DashboardNavbar({ absolute, light, isMini }) {
  const [navbarType, setNavbarType] = useState();
  const [controller, dispatch] = useMaterialUIController();
  const { miniSidenav, transparentNavbar, fixedNavbar, darkMode } = controller;
  const route = useLocation().pathname.split("/").slice(1);

  useEffect(() => {
    if (fixedNavbar) {
      setNavbarType("sticky");
    } else {
      setNavbarType("static");
    }
    function handleTransparentNavbar() {
      setTransparentNavbar(dispatch, (fixedNavbar && window.scrollY === 0) || !fixedNavbar);
    }
    window.addEventListener("scroll", handleTransparentNavbar);
    handleTransparentNavbar();
    return () => window.removeEventListener("scroll", handleTransparentNavbar);
  }, [dispatch, fixedNavbar]);

  const handleMiniSidenav = () => {
    setMiniSidenav(dispatch, !miniSidenav);
  };
  const handleDarkMode = () => setDarkMode(dispatch, !darkMode);

  // Styles for the navbar icons
  const iconsStyle = ({ palette: { dark, white, text }, functions: { rgba } }) => ({
    color: () => {
      let colorValue = light || darkMode ? white.main : dark.main;

      if (transparentNavbar && !light) {
        colorValue = darkMode ? rgba(text.main, 0.6) : text.main;
      }

      return colorValue;
    },
  });

  const { i18n } = useTranslation();

  const [anchorEl, setAnchorEl] = useState(null);

  const handleClick = (e) => {
    setAnchorEl(e.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const [anchorElAvatar, setAnchorElAvatar] = useState(null);

  const handleClickAvatar = (event) => {
    setAnchorElAvatar(event.currentTarget);
  };
  const handleCloseAvatar = () => {
    setAnchorElAvatar(null);
  };

  const handleChangeLanguage = (lng) => {
    i18n.changeLanguage(lng);
    setLanguage(dispatch, lng);
    handleClose();
  };
  return (
    <AppBar
      position={absolute ? "absolute" : navbarType}
      color="inherit"
      sx={(theme) => navbar(theme, { transparentNavbar, absolute, light, darkMode })}
    >
      <Toolbar sx={(theme) => navbarContainer(theme)}>
        <MDBox color="inherit" mb={{ xs: 1, md: 0 }} sx={(theme) => navbarRow(theme, { isMini })}>
          <Breadcrumbs icon="home" title={route[route.length - 1]} route={route} light={light} />
          <IconButton sx={navbarDesktopMenu} onClick={handleMiniSidenav} size="small" disableRipple>
            <Icon fontSize="medium" sx={iconsStyle}>
              {miniSidenav ? "menu_open" : "menu"}
            </Icon>
          </IconButton>
        </MDBox>
        {isMini ? null : (
          <MDBox sx={(theme) => navbarRow(theme, { isMini })}>
            <MDBox />
            <MDBox color={light ? "white" : "inherit"}>
              <IconButton
                size="small"
                color="inherit"
                sx={navbarIconButton}
                variant="contained"
                onClick={handleClick}
              >
                <LanguageIcon />
              </IconButton>
              <Menu
                id="simple-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
              >
                {LANGUAGES.map((item) => (
                  <MenuItem
                    onClick={() => handleChangeLanguage(item.code)}
                    key={item.code}
                    value={item.code}
                  >
                    {item.label}
                  </MenuItem>
                ))}
              </Menu>
              <IconButton size="small" color="inherit" sx={navbarIconButton} variant="contained">
                <Icon color="inherit" sx={iconsStyle}>
                  {darkMode ? "dark_mode" : "wb_sunny"}
                </Icon>
                <Switch checked={darkMode} onChange={handleDarkMode} />
              </IconButton>

              {/* <select
                defaultValue={i18n.language}
                onChange={(e) => handleChangeLanguage(e.target.value)}
              >
                {LANGUAGES.map(({ code, label }) => (
                  <option key={code} value={code}>
                    {label}
                  </option>
                ))}
              </select> */}
              <IconButton
                size="small"
                disableRipple
                color="inherit"
                sx={navbarMobileMenu}
                onClick={handleMiniSidenav}
              >
                <Icon sx={iconsStyle} fontSize="medium">
                  {miniSidenav ? "menu_open" : "menu"}
                </Icon>
              </IconButton>
              <IconButton
                size="small"
                color="inherit"
                sx={navbarIconButton}
                variant="contained"
                onClick={handleClickAvatar}
              >
                {routes.find((item) => item.type === "collapse").icon}
              </IconButton>
              <Menu
                id="simple-menu"
                anchorEl={anchorElAvatar}
                keepMounted
                open={Boolean(anchorElAvatar)}
                onClose={handleCloseAvatar}
              >
                {routes
                  .find((item) => item.type === "collapse")
                  .collapse.map((item) => (
                    <NavLink to={item.route} key={item.key}>
                      <MenuItem>{item.name}</MenuItem>
                    </NavLink>
                  ))}
              </Menu>
            </MDBox>
          </MDBox>
        )}
      </Toolbar>
    </AppBar>
  );
}

// Setting default values for the props of DashboardNavbar
DashboardNavbar.defaultProps = {
  absolute: false,
  light: false,
  isMini: false,
};

// Typechecking props for the DashboardNavbar
DashboardNavbar.propTypes = {
  absolute: PropTypes.bool,
  light: PropTypes.bool,
  isMini: PropTypes.bool,
};

export default DashboardNavbar;
