import React from "react";

export const combineProviders = (list, children) =>
  list.reduceRight((acc, Provider) => <Provider>{acc}</Provider>, children);
