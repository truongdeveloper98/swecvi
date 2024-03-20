import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import {
  createPythonRequest,
  deletePythonRequest,
  getPythonsRequest,
  resetDefaultPythonCodeRequest,
  setAsDefaultPythonCodeRequest,
} from "../service";

const usePython = () => {
  const pythons = useSelector((state) => state.python.pythons);
  const [version, setVersion] = useState(0);
  const [editorValue, setEditorValue] = useState("");
  const [selectedFile, setSelectedFile] = useState(0);

  useEffect(() => {
    getPythonsRequest();
  }, []);

  useEffect(() => {
    const currentPython = pythons[selectedFile]?.versions?.find((python, i) => {
      if (python.isCurrentVersion) {
        setVersion(i);
        return true;
      }
      return false;
    });
    setEditorValue(currentPython?.script);
  }, [pythons, selectedFile]);

  useEffect(() => {
    const currentPython = pythons[selectedFile]?.versions?.find(
      (python, index) => version === index
    );
    setEditorValue(currentPython?.script);
  }, [version]);

  const handleDelete = () => {
    const { id } = pythons[selectedFile].versions[version];
    deletePythonRequest(id, getPythonsRequest);
  };

  const handleReset = () => {
    const { id } = pythons[selectedFile].versions[version];
    resetDefaultPythonCodeRequest(id, getPythonsRequest);
  };

  const handleSetAsDefault = () => {
    const { id } = pythons[selectedFile].versions[version];
    setAsDefaultPythonCodeRequest(id, getPythonsRequest);
  };

  const handleCreate = () => {
    const { id } = pythons[selectedFile].versions[version];
    createPythonRequest(id, { script: editorValue }, getPythonsRequest);
  };

  const handleChange = (e) => {
    const { value } = e.target;
    setEditorValue(value);
  };

  return {
    editorValue,
    pythons,
    version,
    selectedFile,

    handleDelete,

    handleReset,
    handleSetAsDefault,
    handleChange,
    handleCreate,

    setSelectedFile,
    setVersion,
  };
};
export default usePython;
