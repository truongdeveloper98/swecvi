import PAGES from "navigation/pages";
import { useRef } from "react";
import { useNavigate } from "react-router-dom";

const useFindingStructures = () => {
  const navigate = useNavigate();
  const agRef = useRef();

  const onCreateFindingStructures = () => {
    navigate(PAGES.newFindingStructures);
  };

  const handleEditFindingStructures = (id) => {
    navigate(`${PAGES.editFindingStructures}/${id}`);
  };

  return { agRef, onCreateFindingStructures, handleEditFindingStructures };
};

export default useFindingStructures;
