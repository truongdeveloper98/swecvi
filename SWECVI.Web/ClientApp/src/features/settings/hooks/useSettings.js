import { getDepartmentRequest } from "features/department/services";
import PAGES from "navigation/pages";
import { useEffect, useRef } from "react";
import { useNavigate } from "react-router-dom";

const useSettings = () => {
  const navigate = useNavigate();
  const agRef = useRef();

  useEffect(() => {
    getDepartmentRequest();
  }, []);

  const onCreateSettings = () => {
    navigate(PAGES.newSettings);
  };

  const handleEditSettings = async (id) => {
    navigate(`${PAGES.editSettings}/${id}`);
  };

  return { agRef, onCreateSettings, handleEditSettings };
};

export default useSettings;
