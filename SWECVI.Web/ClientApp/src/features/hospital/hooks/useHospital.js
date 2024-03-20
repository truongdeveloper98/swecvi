import PAGES from "navigation/pages";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";
import { getHospitalRequestById } from "../services";

const useHospital = () => {
  const navigate = useNavigate();

  const agRef = useRef(null);

  const onCreateHospital = () => {
    navigate(PAGES.newHospital);
  };

  const handleEditHospital = async (id) => {
    await getHospitalRequestById(id);
    navigate(`${PAGES.editHospital}/${id}`);
  };

  return { agRef, onCreateHospital, handleEditHospital };
};

export default useHospital;
