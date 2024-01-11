import {Box, TextField, TextFieldProps} from "@mui/material";
import React from "react";

export const StyledTextInput = (props: TextFieldProps&{align?:'right'|'centre'|'left'}) => {
  const marginTop = props.label ? '10px' : '5px';
  return (
    <TextField
      sx={{
        minWidth: 'fit-content',
        margin: "auto",
        marginTop: marginTop
      }}
      {...props}
    />);
}