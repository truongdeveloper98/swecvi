import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { createSettingsRequest, getSettingsRequest, updateSettingsRequest } from "../services";

const useSettingsDetail = () => {
  const [settings, setSettings] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();
  const [openBackdrop, setOpenBackdrop] = useState(false);

  useEffect(() => {
    if (params.id) {
      getSettingsRequest(params?.id, (data) => {
        setSettings(data);
      });
    }
  }, [params?.id]);

  const handleCancel = () => {
    navigate(PAGES.settings);
  };

  const handleSubmitForm = async (data) => {
    setOpenBackdrop(true);
    if (params?.id) {
      await updateSettingsRequest(params.id, data, () => {
        navigate(PAGES.settings);
      });
    } else {
      await createSettingsRequest(data, () => {
        navigate(PAGES.settings);
      });
    }
    setOpenBackdrop(false);
  };

  return { handleCancel, settings, handleSubmitForm, openBackdrop };
};

export default useSettingsDetail;
