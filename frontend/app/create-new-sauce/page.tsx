'use client'

import React from "react";
import {Box} from "@mui/material";
import {HeaderComponent} from "../sharedComponents/header/HeaderComponent";
import {NewSauceForm} from "./newSauceForm";

export default function Home() {
  return (
    <Box color={'black'} className=" min-h-screen items-center justify-between p-24">
      <HeaderComponent/>
      <NewSauceForm/>
    </Box>
  );
}

