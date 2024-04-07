/* eslint-disable react/no-unstable-nested-components */
import React from "react";
import { useSelector } from "react-redux";
import BaseLayout from "components/Customized/BaseLayout";
// import CellCheckbox from "components/Customized/CellCheckbox";
// import AgGridTable from "components/Customized/AgGridTable";
import { createColumnHelper } from "@tanstack/react-table";
import TsGridTable from "components/Customized/TsGridTable";
import { t } from "i18next";
import { Button, Checkbox } from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import useUsers from "./hooks/useUsers";
import { getUsersRequest } from "./services";

const columnHelper = createColumnHelper();

function Users() {
  const { agRef, handleEditUser, handleChangeActiveStatus, handleCreateUser } = useUsers();
  // const { agRef, handleEditUser } = useUsers();

  // const columnDefs = [
  //   {
  //     field: "id",
  //     headerName: "User ID",
  //   },
  //   {
  //     field: "firstName",
  //     headerName: "First Name",
  //   },
  //   {
  //     field: "lastName",
  //     headerName: "Last Name",
  //   },
  //   {
  //     field: "phoneNumber",
  //     headerName: "Phone Number",
  //     sortable: false,
  //   },
  //   { field: "email", headerName: "Email", sortable: false },
  //   {
  //     field: "isActive",
  //     headerName: "Active",
  //     cellRenderer: CellCheckbox,
  //     cellRendererParams: {
  //       onChange: (id, data) => {
  //         handleChangeActiveStatue(id, data.isActive);
  //       },
  //     },
  //     maxWidth: 100,
  //   },
  // ];

  const users = useSelector((state) => state.user.users);

  const columns = [
    columnHelper.accessor("id", {
      id: "id",
      header: t("UserID"),
    }),
    columnHelper.accessor("firstName", {
      id: "firstName",
      header: t("FirstName"),
    }),
    columnHelper.accessor("lastName", {
      id: "lastName",
      header: t("LastName"),
    }),
    columnHelper.accessor("phoneNumber", {
      id: "phoneNumber",
      header: t("PhoneNumber"),
      enableSorting: false,
    }),
    columnHelper.accessor("email", {
      id: "email",
      header: "Email",
      enableSorting: false,
    }),
    columnHelper.accessor("role", {
      id: "role",
      header: "Role",
      enableSorting: false,
    }),
    columnHelper.accessor("department", {
      id: "department",
      header: "Department",
      enableSorting: false,
    }),
    columnHelper.accessor("isActive", {
      id: "isActive",
      header: t("Active"),
      cell: ({ row }) => (
        <Checkbox
          checked={row.original.isActive}
          onChange={() => handleChangeActiveStatus(row.original.id, row.original.isActive)}
        />
      ),
    }),
    columnHelper.accessor("", {
      id: "action",
      header: () => null,
      cell: ({ row }) => (
        <Button onClick={() => handleEditUser(row.original.id)}>
          <EditIcon />
        </Button>
      ),
    }),
  ];

  return (
    <BaseLayout>
      {/* <AgGridTable
        ref={agRef}
        columnDefs={columnDefs}
        entity={users}
        onFetching={getUsersRequest}
        onEdit={({ id }) => handleEditUser(id)}
        onCreate={handleCreateUser}
        entityName="User"
      /> */}
      <TsGridTable
        ref={agRef}
        columns={columns}
        entity={users}
        onFetching={getUsersRequest}
        onCreate={handleCreateUser}
        entityName={t("User")}
        isExpand={false}
      />
    </BaseLayout>
  );
}

export default Users;
