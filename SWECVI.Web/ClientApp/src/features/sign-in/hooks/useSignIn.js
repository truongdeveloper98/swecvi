import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import PAGES from "navigation/pages";
import { useSelector } from "react-redux";
import { hospitalData, loginRequest } from "../services";

const useSignIn = () => {
  const navigate = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [hospitalId, setHospitalId] = useState("");
  const token = useSelector((state) => state.auth.token);

  const hospitals = useSelector((state) => state.auth.hospitals);

  const handleChangeHospital = (value) => {
    const resp = hospitals.find((item) => item.name === value);
    localStorage.setItem("hospitalId", resp.id);
    setHospitalId(resp.id);
  };

  const handleLogin = () => {
    loginRequest({ email, password, hospitalId });
  };

  useEffect(() => {
    hospitalData();
  }, []);

  useEffect(() => {
    if (token) {
      navigate(PAGES.analytics);
    }
  }, [token]);

  return {
    state: {
      email,
      setEmail,
      password,
      setPassword,
      hospitals,
    },

    function: {
      handleLogin,
      handleChangeHospital,
    },
  };
};
export default useSignIn;
