export const dateTimeFormat = (dateTime) => {
  const year = dateTime.slice(0, 4);
  const month = dateTime.slice(4, 6);
  const day = dateTime.slice(6, 8);
  return `${year}-${month}-${day}`;
};
