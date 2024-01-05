import {Snackbar} from "@mui/base";
import {Alert} from "@mui/material";
import React from "react";

export const ResponseSnackBars = (props: {
  isGood: boolean | undefined,
  setGood: (b:boolean | undefined) => void,
  goodMessage: string,
  isBad: boolean | undefined,
  setBad:  (b:boolean | undefined) => void,
  badMessage: string}) => {
  const {isGood,setGood, goodMessage, isBad, setBad, badMessage} = props
  return (
    <>
      <Snackbar
        open={isGood ?? false}
        style={{
          minWidth: 'fit-content',
          padding: '10px 0px'
        }}
        autoHideDuration={3000}
        onClose={() => setGood(undefined)}
      >
        <Alert severity={'success'}>
          {goodMessage}
        </Alert>
      </Snackbar>
      <Snackbar
        open={isBad ?? false}
        autoHideDuration={6000}
        onClose={() => setBad(undefined)}
      >
        <Alert severity='error'>
          {badMessage}
        </Alert>
      </Snackbar>
    </>
  );
}
