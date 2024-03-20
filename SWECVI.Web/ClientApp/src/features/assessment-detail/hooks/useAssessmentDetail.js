import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getAssessmentRequest } from "../services";

const useAssessmentDetail = () => {
  const [assessment, setAssessment] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();

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

  return { handleCancel, assessment };
};

export default useAssessmentDetail;
