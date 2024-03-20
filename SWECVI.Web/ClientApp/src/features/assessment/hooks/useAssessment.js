import PAGES from "navigation/pages";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";

const useAssessment = () => {
  const navigate = useNavigate();
  const agRef = useRef();

  const onCreateAssessment = () => {
    navigate(PAGES.newAssessment);
  };

  const handleAssessment = async (id) => {
    navigate(`${PAGES.editAssessment}/${id}`);
  };

  return { agRef, onCreateAssessment, handleAssessment };
};

export default useAssessment;
