import { getDepartmentRequest } from "features/department/services";
import PAGES from "navigation/pages";
import { useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";

const useReferences = () => {
  const navigate = useNavigate();
  const agRef = useRef();

  useEffect(() => {
    getDepartmentRequest();
  }, []);

  const onCreateReferences = () => {
    navigate(PAGES.newReferences);
  };

  const handleEditReferences = async (id) => {
    navigate(`${PAGES.editReferences}/${id}`);
  };

  return { agRef, onCreateReferences, handleEditReferences };
};

export default useReferences;
