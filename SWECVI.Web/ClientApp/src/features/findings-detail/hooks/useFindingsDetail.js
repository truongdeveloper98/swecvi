import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { createFindingsRequest, getFindingsRequest, updateFindingsRequest } from "../services";

const useFindingsDetail = () => {
  const [findingStructure, setFindings] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();
  const [openBackdrop, setOpenBackdrop] = useState(false);

  useEffect(() => {
    if (params.id) {
      getFindingsRequest(params?.id, (data) => {
        setFindings(data);
      });
    }
  }, [params?.id]);

  const handleCancel = () => {
    navigate(PAGES.findings);
  };

  const handleSubmitForm = async (data) => {
    setOpenBackdrop(true);
    if (params?.id) {
      await updateFindingsRequest(params.id, data, () => {
        navigate(PAGES.findingStructure);
      });
    } else {
      await createFindingsRequest(data, () => {
        navigate(PAGES.findingStructure);
      });
    }
    setOpenBackdrop(false);
  };

  return { handleCancel, findingStructure, handleSubmitForm, openBackdrop };
};

export default useFindingsDetail;
