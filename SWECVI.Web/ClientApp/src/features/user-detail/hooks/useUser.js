import PAGES from "navigation/pages";
import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useSelector } from "react-redux";
import { useJwt } from "react-jwt";

import { createUserRequest, getUserRequest, updateUserRequest } from "../services";

const useUser = () => {
  const [user, setUser] = useState(undefined);
  const navigate = useNavigate();
  const params = useParams();
  const token = useSelector((state) => state.auth.token);
  const { decodedToken } = useJwt(token);
  const [openBackdrop, setOpenBackdrop] = useState(false);

  useEffect(() => {
    if (params?.id) {
      getUserRequest(params?.id, (data) => {
        setUser(data);
      });
    }
  }, [params?.id]);

  const handleCancel = () => {
    navigate(PAGES.users);
  };

  const handleSubmitForm = async (data) => {
    setOpenBackdrop(true);
    if (params?.id) {
      await updateUserRequest(params.id, data, () => {
        navigate(PAGES.users);
      });
    } else {
      await createUserRequest(data, () => {
        navigate(PAGES.users);
      });
    }
    setOpenBackdrop(false);
  };

  return {
    user,
    handleSubmitForm,
    handleCancel,
    decodedToken,
    openBackdrop,
  };
};
export default useUser;
