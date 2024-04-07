import PAGES from "navigation/pages";
import { useNavigate } from "react-router-dom";
import { useRef } from "react";
import { deleteDepartmentRequest } from "../services";

const useDepartment = () => {
  const navigate = useNavigate();
  const agRef = useRef(null);

  const onCreateDepartment = () => {
    navigate(PAGES.newDepartment);
  };

  const handleEditDepartment = (id) => {
    navigate(`${PAGES.editDepartment}/${id}`);
  };

  const handleDeleteDepartment = (id, cb) => {
    if (!id) return;
    deleteDepartmentRequest(id, cb);
  };

  return { agRef, onCreateDepartment, handleEditDepartment, handleDeleteDepartment };
};

export default useDepartment;
