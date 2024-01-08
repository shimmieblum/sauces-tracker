'use client'

import {useEffect, useState} from "react";
import {SauceResponse} from "../../../models/SauceResponseSchema";
import {useGetSauce} from "../../../hooks/useSaucesApi";
import {notFound} from "next/navigation";
import {Box} from "@mui/material";
import {NotFound} from "../../sharedComponents/NotFound";
import {HeaderComponent} from "../../sharedComponents/header/HeaderComponent";

interface SaucePageParams {
  params: {
    sauceId: string;
  }
}


export default function Sauce({ params }: SaucePageParams) {
  return <>
    <HeaderComponent/>
    <SauceComponent sauceId={params.sauceId}/>
  </>
}

const SauceComponent = ({sauceId}: {sauceId:string}) => {
  const [sauce, setSauce] = useState<SauceResponse | undefined>();
  const { getSauce, loading } = useGetSauce();
  useEffect(() => {
    getSauce(sauceId).then(s => {
      if (s) setSauce(s)
    })}, []);

  if(loading){
    return <></>;
  }

  if(!sauce) {
    return <NotFound> Sauce not found </NotFound>
  }

  return <Box>My Sauce: {sauce.name}</Box>;

}