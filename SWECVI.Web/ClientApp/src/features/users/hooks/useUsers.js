import PAGES from "navigation/pages";
import { useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { getDepartmentRequest } from "features/department/services";
import { activeUserRequest, inactiveUserRequest } from "../services";

const useUsers = () => {
  const navigate = useNavigate();
  const agRef = useRef(null);

  useEffect(() => {
    getDepartmentRequest();
  }, []);

  const handleCreateUser = () => {
    navigate(PAGES.newUser);
  };

  const handleEditUser = (id) => {
    navigate(`${PAGES.editUser}/${id}`);
  };

  const handleChangeActiveStatus = async (id, status) => {
    if (status) {
      await activeUserRequest(id, () => agRef.current?.fetchData());
    } else {
      await inactiveUserRequest(id, () => agRef.current?.fetchData());
    }
  };

  return {
    // state
    agRef,

    // funcs
    handleEditUser,
    handleChangeActiveStatus,
    handleCreateUser,
  };
};
export default useUsers;
