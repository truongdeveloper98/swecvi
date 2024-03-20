import React from "react";
import Icon from "@mui/material/Icon";
import MDBox from "components/MDBox";
import MDPagination from "components/MDPagination";
import MDInput from "components/MDInput";

function Pagination({ entity, pageOptions, onChange, previousPage, gotoPage, nextPage }) {
  const renderPagination = pageOptions.map((option) => (
    <MDPagination
      item
      key={option}
      onClick={() => gotoPage(Number(option))}
      active={entity?.page === option}
    >
      {option + 1}
    </MDPagination>
  ));

  const customizedPageOptions = pageOptions.map((option) => option + 1);

  return (
    <MDPagination variant="gradient" color="info">
      {entity.page > 0 && (
        <MDPagination item onClick={previousPage}>
          <Icon sx={{ fontWeight: "bold" }}>chevron_left</Icon>
        </MDPagination>
      )}
      {renderPagination.length > 6 ? (
        <MDBox mx={1}>
          <MDInput
            inputProps={{ type: "number", min: 1, max: customizedPageOptions.length }}
            value={customizedPageOptions[entity?.page]}
            onChange={onChange}
          />
        </MDBox>
      ) : (
        renderPagination
      )}
      {entity.page < entity.totalPages - 1 && (
        <MDPagination item onClick={nextPage}>
          <Icon sx={{ fontWeight: "bold" }}>chevron_right</Icon>
        </MDPagination>
      )}
    </MDPagination>
  );
}

export default Pagination;
