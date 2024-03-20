import { Card, Grid, Skeleton, TextField, Typography } from "@mui/material";
import React from "react";
import { useSelector } from "react-redux";
import RadioButton from "../RadioButton";
import CheckBox from "../CheckBox";
import useBoxFinding from "./hooks/useBoxFinding";
import Selector from "../Selector";

export default function BoxFinding({ boxHeader, inputPatientFinding, tabName }) {
  const {
    handleCheckboxChange,

    handleChangeRadioButton,

    handleChangeMutiSelector,

    handleChangeSelector,

    handleChangeInputNumber,

    handleChangeInputText,

    handleChangeInputDate,
  } = useBoxFinding();

  const patientFindingValueInput = useSelector((state) => state.patient.patientFindingValueInput);

  return (
    <Card sx={{ width: "100%", height: "100%", padding: 1 }}>
      {inputPatientFinding && inputPatientFinding.length > 0 && tabName !== undefined ? (
        <Grid container>
          <Grid item xs={12}>
            <Typography>{boxHeader}</Typography>
          </Grid>
          {inputPatientFinding.map((item) => (
            <Grid
              item
              xs={12}
              display="flex"
              alignItems="center"
              justifyContent="space-between"
              key={item.inputLabel}
              padding={1}
            >
              <Grid item xs={5}>
                <Typography>{item.inputLabel}</Typography>
              </Grid>
              <Grid item xs={7}>
                {item.inputType === 0 && (
                  <CheckBox
                    key={item.id}
                    inputLabel={item.inputLabel}
                    checkBoxData={{ id: item.id, inputOptions: item.inputOptions.split(";") }}
                    onChangeValue={handleCheckboxChange}
                    tabName={tabName}
                  />
                )}
                {item.inputType === 1 && (
                  <RadioButton
                    key={item.id}
                    label={item.inputLabel}
                    radioBoxData={{ id: item.id, inputOptions: item.inputOptions.split(";") }}
                    onChangeValue={handleChangeRadioButton}
                    tabName={tabName}
                  />
                )}
                {item.inputType === 2 && (
                  <Selector
                    key={item.id}
                    multiple
                    onChange={handleChangeMutiSelector}
                    optionsBoxFinding={{
                      id: item.id,
                      inputOptions: item.inputOptions
                        .split(";")
                        .filter(
                          (itemFilter, indexFilter, self) =>
                            self.indexOf(itemFilter) === indexFilter
                        ),
                    }}
                    label={item.inputLabel}
                    property={item.inputLabel}
                    tabName={tabName}
                  />
                )}
                {item.inputType === 3 && (
                  <Selector
                    key={item.id}
                    onChange={handleChangeSelector}
                    optionsBoxFinding={{
                      id: item.id,
                      inputOptions: item.inputOptions
                        .split(";")
                        .filter(
                          (itemFilter, indexFilter, self) =>
                            self.indexOf(itemFilter) === indexFilter
                        ),
                    }}
                    label={item.inputLabel}
                    property={item.inputLabel}
                    tabName={tabName}
                  />
                )}
                {item.inputType === 4 && (
                  <TextField
                    key={item.id}
                    id="outlined-basic"
                    type="number"
                    variant="standard"
                    onChange={(e) =>
                      handleChangeInputNumber(item.id, item.inputLabel, e.target.value, tabName)
                    }
                    placeholder="Input Number"
                    defaultValue={
                      patientFindingValueInput.valueNumricInput[tabName]
                        ? patientFindingValueInput.valueNumricInput[tabName][item.inputLabel]?.value
                        : undefined || undefined
                    }
                    fullWidth
                    tabName={tabName}
                  />
                )}
                {item.inputType === 5 && (
                  <TextField
                    key={item.id}
                    id="outlined-basic"
                    variant="standard"
                    onChange={(e) =>
                      handleChangeInputText(item.id, item.inputLabel, e.target.value, tabName)
                    }
                    defaultValue={
                      patientFindingValueInput.valueTextInput[tabName]
                        ? patientFindingValueInput.valueTextInput[tabName][item.inputLabel]?.value
                        : undefined || undefined
                    }
                    fullWidth
                    tabName={tabName}
                  />
                )}
                {item.inputType === 6 && (
                  <TextField
                    key={item.id}
                    type="date"
                    onChange={(e) =>
                      handleChangeInputDate(item.id, item.inputLabel, e.target.value, tabName)
                    }
                    defaultValue={
                      patientFindingValueInput.valueDateInput[tabName]
                        ? patientFindingValueInput.valueDateInput[tabName][item.inputLabel]?.value
                        : undefined || undefined
                    }
                    fullWidth
                    tabName={tabName}
                  />
                )}
              </Grid>
            </Grid>
          ))}
        </Grid>
      ) : (
        <Grid container>
          <Grid item xs={12}>
            <Skeleton width="25%" />
          </Grid>
          <Skeleton width="100%" />
          <Skeleton width="100%" />
          <Skeleton width="100%" />
          <Skeleton width="100%" />
        </Grid>
      )}
    </Card>
  );
}
