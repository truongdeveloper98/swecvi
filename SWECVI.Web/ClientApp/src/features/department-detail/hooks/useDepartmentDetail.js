import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import PAGES from "navigation/pages";
import {
  createDepartmentRequest,
  getDepartmentRequest,
  getHospitalsRequest,
  updateDepartmentRequest,
} from "../service";

const useDepartmentDetail = () => {
  const [department, setDepartment] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();
  const [hospitalSuperAdmin, setHospitalSuperAdmin] = useState([]);
  const [openBackdrop, setOpenBackdrop] = useState(false);

  useEffect(() => {
    if (params.id) {
      getDepartmentRequest(params?.id, (data) => {
        setDepartment(data);
      });
    }
  }, [params?.id]);

  useEffect(() => {
    getHospitalsRequest()
      .then((res) => {
        setHospitalSuperAdmin(res);
      })
      .catch((err) => {
        throw err;
      });
  }, []);

  const handleSubmitForm = async (data) => {
    setOpenBackdrop(true);
    if (params?.id) {
      await updateDepartmentRequest(params.id, data, () => {
        navigate(PAGES.department);
      });
    } else {
      await createDepartmentRequest(data, () => {
        navigate(PAGES.department);
      });
    }
    setOpenBackdrop(false);
  };
  const handleCancel = () => {
    navigate(PAGES.department);
  };

  return { department, handleCancel, handleSubmitForm, hospitalSuperAdmin, openBackdrop };
};

export default useDepartmentDetail;
