import { useEffect } from "react";
import { useParams } from "react-router-dom";
import {
  getExamReportByExamIdRequest,
  // getExamRequest,
  // getHL7MeasurementByExamIdRequest,
} from "../services";

const useExam = () => {
  const params = useParams();

  // useEffect(() => {
  //   if (params?.id) {
  //     getExamRequest(params?.id);
  //   }
  // }, [params?.id]);

  useEffect(() => {
    if (params?.id) {
      getExamReportByExamIdRequest(params?.hospitalId, params?.id);
    }
  }, [params?.id]);

  // useEffect(() => {
  //   if (params?.id) {
  //     getHL7MeasurementByExamIdRequest(params?.id);
  //   }
  // }, [params?.id]);

  return { params };
};
export default useExam;
