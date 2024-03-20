import PAGES from "navigation/pages";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";

const useDepartment = () => {
  const navigate = useNavigate();
  const agRef = useRef(null);

  const onCreateDepartment = () => {
    navigate(PAGES.newDepartment);
  };

  const handleEditDepartment = (id) => {
    navigate(`${PAGES.editDepartment}/${id}`);
  };

  return { agRef, onCreateDepartment, handleEditDepartment };
};

export default useDepartment;
