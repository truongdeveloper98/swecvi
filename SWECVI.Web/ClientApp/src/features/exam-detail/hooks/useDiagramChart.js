/* eslint-disable no-console */
import { parameterNamesRequest } from "features/statistics/services";
import _ from "lodash";
import moment from "moment";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import useExam from "./useExam";
import { getDiagramRequest } from "../services";

const useDiagramChart = () => {
  const [data, setData] = useState([]);
  const diagram = useSelector((state) => state.patient.diagram);
  const { params } = useExam();

  useEffect(() => {
    parameterNamesRequest();
    getDiagramRequest();
  }, []);

  const getParameterValues = (values) => {
    getDiagramRequest({ ids: values.map((x) => x.id), patientId: params.id });
  };

  const getArrLabels = () => [
    ...new Set(
      diagram
        ?.filter((item) => item?.valueByTimes[0]?.time !== null)
        .map((x) => x?.valueByTimes?.map((y) => moment(y.time).format("YYYY-MM-DD")))
        .flat()
        .sort()
    ),
  ];

  const convertDataToNewDataToBuildChart = async () => {
    const oldLabelsDateTime = getArrLabels();
    const clonedDiagram = _.cloneDeep(diagram);
    clonedDiagram.forEach((element) => {
      if (element?.valueByTimes?.length && element?.valueByTimes[0].time) {
        const times = element?.valueByTimes?.map((y) => moment(y.time).format("YYYY-MM-DD"));
        const otherTimeNeedPush = oldLabelsDateTime?.filter((time) => !times.includes(time));
        if (otherTimeNeedPush.length) {
          otherTimeNeedPush.forEach((time) => {
            element.valueByTimes.push({
              time,
              value: null,
            });
          });
        }
        element.valueByTimes.sort((a, b) => moment(a.time).valueOf() - moment(b.time).valueOf());
      }
    });
    setData(clonedDiagram);
  };

  useEffect(() => {
    convertDataToNewDataToBuildChart();
  }, [diagram]);

  return {
    data,
    getParameterValues,
    getArrLabels,
  };
};

export default useDiagramChart;
