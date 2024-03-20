import {
  Box,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableRow,
  Typography,
} from "@mui/material";
import { flexRender, getCoreRowModel, useReactTable } from "@tanstack/react-table";
import React, { forwardRef, useEffect, useImperativeHandle, useState } from "react";
import { useTranslation } from "react-i18next";
import { getStudiesByPatientId } from "./services";
// import PropTypes from "prop-types";

const SubTsGridTable = forwardRef(({ data, columns }, ref) => {
  const [dataPatient, setDataPatient] = useState([]);

  useEffect(() => {
    getStudiesByPatientId(data.hospitalId, data.patientId)
      .then((res) => {
        setDataPatient(res.data);
      })
      .catch((err) => {
        throw err;
      });
  }, [data]);

  const subGridTable = useReactTable({
    data: dataPatient,
    columns,
    getCoreRowModel: getCoreRowModel(),
  });

  useImperativeHandle(ref, () => ({}));

  const { t } = useTranslation();

  return (
    <TableContainer sx={{ border: "1px solid #babfc7" }}>
      <Table stickyHeader aria-label="sticky table">
        <thead>
          {subGridTable.getHeaderGroups().map((headerGroup) => (
            <TableRow key={headerGroup.id}>
              {headerGroup.headers.map((header) => (
                <TableCell key={header.id}>
                  <Typography variant="body2" fontWeight="bold" color="#181d1f">
                    {flexRender(header.column.columnDef.header, header.getContext())}
                  </Typography>
                </TableCell>
              ))}
            </TableRow>
          ))}
        </thead>
        <TableBody>
          {dataPatient.length > 0 ? (
            <>
              {subGridTable.getRowModel().rows.map((row) => (
                <TableRow key={row.id} onClick={row.getToggleExpandedHandler()}>
                  {row.getVisibleCells().map((cell) => (
                    <TableCell key={cell.id}>
                      {flexRender(cell.column.columnDef.cell, cell.getContext())}
                    </TableCell>
                  ))}
                </TableRow>
              ))}
            </>
          ) : (
            <TableRow>
              <TableCell colSpan={subGridTable.getAllColumns().length}>
                <Box sx={{ display: "flex", justifyContent: "center" }}>
                  <Typography fontWeight={200} color="#181d1f" fontSize={14}>
                    {t("NoRowsToShow")}
                  </Typography>
                </Box>
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </TableContainer>
  );
});

// SubTsGridTable.defaultProps = {
//   columns: [],
//   data: null,
// };

// SubTsGridTable.propTypes = {
//   columns: PropTypes.array.isRequired(),
//   data: PropTypes.any.isRequired(),
// };

export default SubTsGridTable;
