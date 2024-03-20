import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  createReferencesRequest,
  getReferencesRequest,
  updateReferencesRequest,
} from "../services";

const useReferencesDetail = () => {
  const [references, setReferences] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();
  const [openBackdrop, setOpenBackdrop] = useState(false);

  useEffect(() => {
    if (params.id) {
      getReferencesRequest(params?.id, (data) => {
        setReferences(data);
      });
    }
  }, [params?.id]);

  const handleCancel = () => {
    navigate(PAGES.references);
  };

  const handleSubmitForm = async (data) => {
    setOpenBackdrop(true);
    if (params?.id) {
      await updateReferencesRequest(params.id, data, () => {
        navigate(PAGES.references);
      });
    } else {
      await createReferencesRequest(data, () => {
        navigate(PAGES.references);
      });
    }
    setOpenBackdrop(false);
  };

  return { handleCancel, references, handleSubmitForm, openBackdrop };
};

export default useReferencesDetail;
