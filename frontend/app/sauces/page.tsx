'use client'

import React from "react";
import {SaucesComponent} from "./SaucesComponent";
import {Box, Chip, Typography} from "@mui/material";
import {createTheme} from "@mui/material/styles";
import {HeaderComponent} from "../sharedComponents/header/HeaderComponent";
import AddIcon from '@mui/icons-material/Add'
import {redirect, RedirectType} from "next/navigation";



const AddNewSauceButton = () =>  {
  return <Chip
    clickable
    component='a'
    href='/create-new-sauce'
    sx={{ml: 2, mt: 2}} 
    label={<Typography color='white'>New Sauce</Typography> } 
    icon={<AddIcon />}
    color='primary'
  />;
}

export default function Home() {
  return (
    <Box className="flex min-h-screen flex-col items-center justify-between p-24">
      <HeaderComponent/>
      <AddNewSauceButton/>
      <SaucesComponent/>
    </Box>
  );
}
