/* eslint-disable no-unused-vars */

// merge Object
export const mergeObjects = (...objects) => {
  const filteredObjects = objects.filter((obj) => Object.keys(obj).length > 0);
  return filteredObjects.reduce((merged, obj) => {
    // eslint-disable-next-line no-restricted-syntax
    for (const key in obj) {
      if (Object.hasOwnProperty.call(obj, key)) {
        merged[key] = { ...merged[key], ...obj[key] };
      }
    }
    return merged;
  }, {});
};

/*
  If an object has many objects, some of which have no value, they should be removed
  data: {
    valueOnlyCheckBox: {},
    valueOnlyRadioButton: {
      key: value
    },
    valueMultiSelect: {},
    valueOneSelect: {},
    valueNumricInput: {},
    valueTextInput: {},
    valueDateInput: {},
  }
  --> data : {
    valueOnlyRadioButton: {
      key: value
    },
  }
*/
export const objectsWithData = (data) =>
  Object.keys(data)
    .filter((key) => Object.keys(data[key]).length > 0)
    .reduce((acc, key) => {
      acc[key] = data[key];
      return acc;
    }, {});

/*
convert data checkbox {
  "STE": true,
  "id": 3,
  "Rondförberedelser": true
} -> {id: id, value: "STE;Rondförberedelser"}
*/
const convertKeyBoolean = (data) => {
  const result = Object.entries(data)
    .filter(([key, value]) => key !== "id" && value === true)
    .reduce(
      (acc, [key]) => {
        acc.value += `${key};`;
        return acc;
      },
      { id: data.id, value: "" }
    );
  result.value = result.value.slice(0, -1);

  return result;
};

// convert từ input value sang model lưu vào db
export const getValueInObject = (data) =>
  Object.values(data)
    .flatMap((subObject) =>
      Object.values(subObject).map((subChildObject) =>
        Object.values(subChildObject).map((item) =>
          item.value !== undefined
            ? { value: Array.isArray(item.value) ? item.value.join(";") : item.value, id: item.id }
            : convertKeyBoolean(item)
        )
      )
    )
    .flatMap((subArray) => subArray)
    .filter((item) => item.value !== null && item.value !== undefined && item.value !== "");

// convert data từ DB sang dạng input
export const swapStringToArray = (data) => (data.includes(";") ? data.split(";") : data);
export const swapStringToMultiArray = (data) => (data.includes(";") ? data.split(";") : [data]);

export const convertDataToInputV2 = (dataResp, dataObject) => {
  const result = dataResp.map((data) => {
    if (data.inputType === 0) {
      const tmp = {
        ...data,
        value: swapStringToMultiArray(data.value).reduce((resultTmp, item) => {
          resultTmp[item] = true;
          return resultTmp;
        }, {}),
      };
      const { value, inputLabel, id, inputType, tabName } = tmp;
      return { value: { [tabName]: { [inputLabel]: { ...value, id } } }, inputType };
    }
    if (data.inputType === 2) {
      const tmp = {
        ...data,
        value: swapStringToMultiArray(data.value),
      };
      return {
        value: { [tmp.tabName]: { [tmp.inputLabel]: { id: tmp.id, value: tmp.value } } },
        inputType: tmp.inputType,
      };
    }
    const tmp = {
      ...data,
      value: swapStringToArray(data.value),
    };
    return {
      value: { [tmp.tabName]: { [tmp.inputLabel]: { id: tmp.id, value: tmp.value } } },
      inputType: tmp.inputType,
    };
  });
  result.forEach((item) => {
    if (item.inputType === 0) {
      if ("Allmänt" in item.value) {
        dataObject.valueOnlyCheckBox = {
          ...dataObject.valueOnlyCheckBox,
          Allmänt: { ...dataObject.valueOnlyCheckBox.Allmänt, ...item.value.Allmänt },
        };
      }
      if ("Undersökning" in item.value) {
        dataObject.valueOnlyCheckBox = {
          ...dataObject.valueOnlyCheckBox,
          Undersökning: {
            ...dataObject.valueOnlyCheckBox.Undersökning,
            ...item.value.Undersökning,
          },
        };
      }
      if ("Aorta" in item.value) {
        dataObject.valueOnlyCheckBox = {
          ...dataObject.valueOnlyCheckBox,
          Aorta: {
            ...dataObject.valueOnlyCheckBox.Aorta,
            ...item.value.Aorta,
          },
        };
      }
      if ("Mitralis" in item.value) {
        dataObject.valueOnlyCheckBox = {
          ...dataObject.valueOnlyCheckBox,
          Mitralis: {
            ...dataObject.valueOnlyCheckBox.Mitralis,
            ...item.value.Mitralis,
          },
        };
      }
      if ("Pulmonalis" in item.value) {
        dataObject.valueOnlyCheckBox = {
          ...dataObject.valueOnlyCheckBox,
          Pulmonalis: {
            ...dataObject.valueOnlyCheckBox.Pulmonalis,
            ...item.value.Pulmonalis,
          },
        };
      }
      if ("Tricuspidalis" in item.value) {
        dataObject.valueOnlyCheckBox = {
          ...dataObject.valueOnlyCheckBox,
          Tricuspidalis: {
            ...dataObject.valueOnlyCheckBox.Tricuspidalis,
            ...item.value.Tricuspidalis,
          },
        };
      }
      if ("Övrigt" in item.value) {
        dataObject.valueOnlyCheckBox = {
          ...dataObject.valueOnlyCheckBox,
          Övrigt: {
            ...dataObject.valueOnlyCheckBox.Övrigt,
            ...item.value.Övrigt,
          },
        };
      }
    }
    if (item.inputType === 1) {
      if ("Allmänt" in item.value) {
        dataObject.valueOnlyRadioButton = {
          ...dataObject.valueOnlyRadioButton,
          Allmänt: { ...dataObject.valueOnlyRadioButton.Allmänt, ...item.value.Allmänt },
        };
      }
      if ("Undersökning" in item.value) {
        dataObject.valueOnlyRadioButton = {
          ...dataObject.valueOnlyRadioButton,
          Undersökning: {
            ...dataObject.valueOnlyRadioButton.Undersökning,
            ...item.value.Undersökning,
          },
        };
      }
      if ("Aorta" in item.value) {
        dataObject.valueOnlyRadioButton = {
          ...dataObject.valueOnlyRadioButton,
          Aorta: {
            ...dataObject.valueOnlyRadioButton.Aorta,
            ...item.value.Aorta,
          },
        };
      }
      if ("Mitralis" in item.value) {
        dataObject.valueOnlyRadioButton = {
          ...dataObject.valueOnlyRadioButton,
          Mitralis: {
            ...dataObject.valueOnlyRadioButton.Mitralis,
            ...item.value.Mitralis,
          },
        };
      }
      if ("Pulmonalis" in item.value) {
        dataObject.valueOnlyRadioButton = {
          ...dataObject.valueOnlyRadioButton,
          Pulmonalis: {
            ...dataObject.valueOnlyRadioButton.Pulmonalis,
            ...item.value.Pulmonalis,
          },
        };
      }
      if ("Tricuspidalis" in item.value) {
        dataObject.valueOnlyRadioButton = {
          ...dataObject.valueOnlyRadioButton,
          Tricuspidalis: {
            ...dataObject.valueOnlyRadioButton.Tricuspidalis,
            ...item.value.Tricuspidalis,
          },
        };
      }
      if ("Övrigt" in item.value) {
        dataObject.valueOnlyRadioButton = {
          ...dataObject.valueOnlyRadioButton,
          Övrigt: {
            ...dataObject.valueOnlyRadioButton.Övrigt,
            ...item.value.Övrigt,
          },
        };
      }
    }
    if (item.inputType === 2) {
      if ("Allmänt" in item.value) {
        dataObject.valueMultiSelect = {
          ...dataObject.valueMultiSelect,
          Allmänt: { ...dataObject.valueMultiSelect.Allmänt, ...item.value.Allmänt },
        };
      }
      if ("Undersökning" in item.value) {
        dataObject.valueMultiSelect = {
          ...dataObject.valueMultiSelect,
          Undersökning: {
            ...dataObject.valueMultiSelect.Undersökning,
            ...item.value.Undersökning,
          },
        };
      }
      if ("Aorta" in item.value) {
        dataObject.valueMultiSelect = {
          ...dataObject.valueMultiSelect,
          Aorta: { ...dataObject.valueMultiSelect.Aorta, ...item.value.Aorta },
        };
      }
      if ("Mitralis" in item.value) {
        dataObject.valueMultiSelect = {
          ...dataObject.valueMultiSelect,
          Mitralis: { ...dataObject.valueMultiSelect.Mitralis, ...item.value.Mitralis },
        };
      }
      if ("Pulmonalis" in item.value) {
        dataObject.valueMultiSelect = {
          ...dataObject.valueMultiSelect,
          Pulmonalis: {
            ...dataObject.valueMultiSelect.Pulmonalis,
            ...item.value.Pulmonalis,
          },
        };
      }
      if ("Tricuspidalis" in item.value) {
        dataObject.valueMultiSelect = {
          ...dataObject.valueMultiSelect,
          Tricuspidalis: {
            ...dataObject.valueMultiSelect.Tricuspidalis,
            ...item.value.Tricuspidalis,
          },
        };
      }
      if ("Övrigt" in item.value) {
        dataObject.valueMultiSelect = {
          ...dataObject.valueMultiSelect,
          Övrigt: {
            ...dataObject.valueMultiSelect.Övrigt,
            ...item.value.Övrigt,
          },
        };
      }
    }
    if (item.inputType === 3) {
      if ("Allmänt" in item.value) {
        dataObject.valueOneSelect = {
          ...dataObject.valueOneSelect,
          Allmänt: { ...dataObject.valueOneSelect.Allmänt, ...item.value.Allmänt },
        };
      }
      if ("Undersökning" in item.value) {
        dataObject.valueOneSelect = {
          ...dataObject.valueOneSelect,
          Undersökning: {
            ...dataObject.valueOneSelect.Undersökning,
            ...item.value.Undersökning,
          },
        };
      }
      if ("Aorta" in item.value) {
        dataObject.valueOneSelect = {
          ...dataObject.valueOneSelect,
          Aorta: { ...dataObject.valueOneSelect.Aorta, ...item.value.Aorta },
        };
      }
      if ("Mitralis" in item.value) {
        dataObject.valueOneSelect = {
          ...dataObject.valueOneSelect,
          Mitralis: { ...dataObject.valueOneSelect.Mitralis, ...item.value.Mitralis },
        };
      }
      if ("Pulmonalis" in item.value) {
        dataObject.valueOneSelect = {
          ...dataObject.valueOneSelect,
          Pulmonalis: {
            ...dataObject.valueOneSelect.Pulmonalis,
            ...item.value.Pulmonalis,
          },
        };
      }
      if ("Tricuspidalis" in item.value) {
        dataObject.valueOneSelect = {
          ...dataObject.valueOneSelect,
          Tricuspidalis: {
            ...dataObject.valueOneSelect.Tricuspidalis,
            ...item.value.Tricuspidalis,
          },
        };
      }
      if ("Övrigt" in item.value) {
        dataObject.valueOneSelect = {
          ...dataObject.valueOneSelect,
          Övrigt: {
            ...dataObject.valueOneSelect.Övrigt,
            ...item.value.Övrigt,
          },
        };
      }
    }
    if (item.inputType === 4) {
      if ("Allmänt" in item.value) {
        dataObject.valueNumricInput = {
          ...dataObject.valueNumricInput,
          Allmänt: { ...dataObject.valueNumricInput.Allmänt, ...item.value.Allmänt },
        };
      }
      if ("Undersökning" in item.value) {
        dataObject.valueNumricInput = {
          ...dataObject.valueNumricInput,
          Undersökning: {
            ...dataObject.valueNumricInput.Undersökning,
            ...item.value.Undersökning,
          },
        };
      }
      if ("Aorta" in item.value) {
        dataObject.valueNumricInput = {
          ...dataObject.valueNumricInput,
          Aorta: { ...dataObject.valueNumricInput.Aorta, ...item.value.Aorta },
        };
      }
      if ("Mitralis" in item.value) {
        dataObject.valueNumricInput = {
          ...dataObject.valueNumricInput,
          Mitralis: { ...dataObject.valueNumricInput.Mitralis, ...item.value.Mitralis },
        };
      }
      if ("Pulmonalis" in item.value) {
        dataObject.valueNumricInput = {
          ...dataObject.valueNumricInput,
          Pulmonalis: {
            ...dataObject.valueNumricInput.Pulmonalis,
            ...item.value.Pulmonalis,
          },
        };
      }
      if ("Tricuspidalis" in item.value) {
        dataObject.valueNumricInput = {
          ...dataObject.valueNumricInput,
          Tricuspidalis: {
            ...dataObject.valueNumricInput.Tricuspidalis,
            ...item.value.Tricuspidalis,
          },
        };
      }
      if ("Övrigt" in item.value) {
        dataObject.valueNumricInput = {
          ...dataObject.valueNumricInput,
          Övrigt: {
            ...dataObject.valueNumricInput.Övrigt,
            ...item.value.Övrigt,
          },
        };
      }
    }
    if (item.inputType === 5) {
      if ("Allmänt" in item.value) {
        dataObject.valueTextInput = {
          ...dataObject.valueTextInput,
          Allmänt: { ...dataObject.valueTextInput.Allmänt, ...item.value.Allmänt },
        };
      }
      if ("Undersökning" in item.value) {
        dataObject.valueTextInput = {
          ...dataObject.valueTextInput,
          Undersökning: {
            ...dataObject.valueTextInput.Undersökning,
            ...item.value.Undersökning,
          },
        };
      }
      if ("Aorta" in item.value) {
        dataObject.valueTextInput = {
          ...dataObject.valueTextInput,
          Aorta: { ...dataObject.valueTextInput.Aorta, ...item.value.Aorta },
        };
      }
      if ("Mitralis" in item.value) {
        dataObject.valueTextInput = {
          ...dataObject.valueTextInput,
          Mitralis: { ...dataObject.valueTextInput.Mitralis, ...item.value.Mitralis },
        };
      }
      if ("Pulmonalis" in item.value) {
        dataObject.valueTextInput = {
          ...dataObject.valueTextInput,
          Pulmonalis: {
            ...dataObject.valueTextInput.Pulmonalis,
            ...item.value.Pulmonalis,
          },
        };
      }
      if ("Tricuspidalis" in item.value) {
        dataObject.valueTextInput = {
          ...dataObject.valueTextInput,
          Tricuspidalis: {
            ...dataObject.valueTextInput.Tricuspidalis,
            ...item.value.Tricuspidalis,
          },
        };
      }
      if ("Övrigt" in item.value) {
        dataObject.valueTextInput = {
          ...dataObject.valueTextInput,
          Övrigt: {
            ...dataObject.valueTextInput.Övrigt,
            ...item.value.Övrigt,
          },
        };
      }
    }
    if (item.inputType === 6) {
      if ("Allmänt" in item.value) {
        dataObject.valueDateInput = {
          ...dataObject.valueDateInput,
          Allmänt: { ...dataObject.valueDateInput.Allmänt, ...item.value.Allmänt },
        };
      }
      if ("Undersökning" in item.value) {
        dataObject.valueDateInput = {
          ...dataObject.valueDateInput,
          Undersökning: {
            ...dataObject.valueDateInput.Undersökning,
            ...item.value.Undersökning,
          },
        };
      }
      if ("Aorta" in item.value) {
        dataObject.valueDateInput = {
          ...dataObject.valueDateInput,
          Aorta: { ...dataObject.valueDateInput.Aorta, ...item.value.Aorta },
        };
      }
      if ("Mitralis" in item.value) {
        dataObject.valueDateInput = {
          ...dataObject.valueDateInput,
          Mitralis: { ...dataObject.valueDateInput.Mitralis, ...item.value.Mitralis },
        };
      }
      if ("Pulmonalis" in item.value) {
        dataObject.valueDateInput = {
          ...dataObject.valueDateInput,
          Pulmonalis: {
            ...dataObject.valueDateInput.Pulmonalis,
            ...item.value.Pulmonalis,
          },
        };
      }
      if ("Tricuspidalis" in item.value) {
        dataObject.valueDateInput = {
          ...dataObject.valueDateInput,
          Tricuspidalis: {
            ...dataObject.valueDateInput.Tricuspidalis,
            ...item.value.Tricuspidalis,
          },
        };
      }
      if ("Övrigt" in item.value) {
        dataObject.valueDateInput = {
          ...dataObject.valueDateInput,
          Övrigt: {
            ...dataObject.valueDateInput.Övrigt,
            ...item.value.Övrigt,
          },
        };
      }
    }
  });
  return dataObject;
};
