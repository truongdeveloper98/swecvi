import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import PAGES from "navigation/pages";
// import { useSelector } from "react-redux";
// import { useJwt } from "react-jwt";
import { useSelector } from "react-redux";
import { createHospitalRequest, getHospitalRequest, updateHospitalRequest } from "../services";

const useHospitalDetail = () => {
  const [hospital, setHospital] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();
  // const token = useSelector((state) => state.auth.token);
  // const { decodedToken } = useJwt(token);
  const [openBackdrop, setOpenBackdrop] = useState(false);
  const regionValue = useSelector((state) => state.hospital.region);

  useEffect(() => {
    if (params.id) {
      getHospitalRequest(params?.id, (data) => {
        setHospital(data);
      });
    }
  }, [params?.id]);

  const handleSubmitForm = async (data) => {
    setOpenBackdrop(true);
    if (params?.id) {
      await updateHospitalRequest(params.id, data, () => {
        navigate(PAGES.hospital);
      });
    } else {
      await createHospitalRequest(data, () => {
        navigate(PAGES.hospital);
      });
    }
    setOpenBackdrop(false);
  };
  const handleCancel = () => {
    navigate(PAGES.hospital);
  };

  return { hospital, handleCancel, handleSubmitForm, openBackdrop, regionValue };
};

export default useHospitalDetail;
