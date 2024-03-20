import PropTypes from "prop-types";

import ListItem from "@mui/material/ListItem";
import ListItemIcon from "@mui/material/ListItemIcon";
import ListItemText from "@mui/material/ListItemText";
import Icon from "@mui/material/Icon";

// Otis Admin PRO React components
import MDBox from "components/MDBox";

// Custom styles for the SidenavCollapse
import {
  collapseItem,
  collapseIconBox,
  collapseIcon,
  collapseText,
  collapseArrow,
} from "examples/Sidenav/styles/sidenavCollapse";

// Otis Admin PRO React context
import { useMaterialUIController } from "context";

function SidenavTab({ color, icon, name, children, active, noCollapse, open, ...rest }) {
  const [controller] = useMaterialUIController();
  const { miniSidenav, transparentSidenav, whiteSidenav, darkMode } = controller;

  return (
    <ListItem component="li">
      <MDBox
        {...rest}
        sx={(theme) =>
          collapseItem(theme, { active, color, transparentSidenav, whiteSidenav, darkMode })
        }
      >
        <ListItemIcon
          sx={(theme) => collapseIconBox(theme, { transparentSidenav, whiteSidenav, darkMode })}
        >
          {typeof icon === "string" ? (
            <Icon sx={(theme) => collapseIcon(theme, { active })}>{icon}</Icon>
          ) : (
            icon
          )}
        </ListItemIcon>

        <ListItemText
          primary={name}
          sx={(theme) =>
            collapseText(theme, {
              miniSidenav,
              transparentSidenav,
              whiteSidenav,
              active,
            })
          }
        />

        <Icon
          sx={(theme) =>
            collapseArrow(theme, {
              noCollapse,
              transparentSidenav,
              whiteSidenav,
              miniSidenav,
              open,
              active,
              darkMode,
            })
          }
        >
          expand_less
        </Icon>
      </MDBox>
    </ListItem>
  );
}

// Setting default values for the props of SidenavCollapse
SidenavTab.defaultProps = {
  color: "info",
  active: false,
  noCollapse: true,
  children: false,
  open: false,
};

// Typechecking props for the SidenavCollapse
SidenavTab.propTypes = {
  color: PropTypes.oneOf(["primary", "secondary", "info", "success", "warning", "error", "dark"]),
  icon: PropTypes.node.isRequired,
  name: PropTypes.string.isRequired,
  children: PropTypes.node,
  active: PropTypes.bool,
  noCollapse: PropTypes.bool,
  open: PropTypes.bool,
};

export default SidenavTab;
