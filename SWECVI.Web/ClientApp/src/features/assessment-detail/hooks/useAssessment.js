import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  createAssessmentRequest,
  getAssessmentRequest,
  updateAssessmentRequest,
} from "../services";

const useAssessmentDetail = () => {
  const [assessment, setAssessment] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();
  const [openBackdrop, setOpenBackdrop] = useState(false);

  useEffect(() => {
    if (params.id) {
      getAssessmentRequest(params?.id, (data) => {
        setAssessment(data);
      });
    }
  }, [params?.id]);

  const handleCancel = () => {
    navigate(PAGES.assessment);
  };

  const handleSubmitForm = async (data) => {
    setOpenBackdrop(true);
    if (params?.id) {
      await updateAssessmentRequest(params.id, data, () => {
        navigate(PAGES.assessment);
      });
    } else {
      await createAssessmentRequest(data, () => {
        navigate(PAGES.assessment);
      });
    }
    setOpenBackdrop(false);
  };

  return { handleCancel, assessment, handleSubmitForm, openBackdrop };
};

export default useAssessmentDetail;
