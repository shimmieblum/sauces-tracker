'use client'

import { SauceResponse } from "../../models/SauceResponseSchema";
import React, { useEffect, useState } from "react";
import {useDeleteSauce, useGetSauces} from '../../hooks/useSaucesApi';
import {SauceCardComponent} from "./SauceCardComponent";
import {Grid} from "@mui/material";
import {ResponseSnackBars} from "../sharedComponents/ResponseSnackBars";
import {base} from "next/dist/build/webpack/config/blocks/base";
import {NotFound} from "../sharedComponents/NotFound";

export const SaucesComponent = () => {
  const [sauces, setSauces] = useState<SauceResponse[] | undefined>();
  const [deleteSuccess, setDeleteSuccess] = useState<boolean>();
  const [deleteFailed, setDeleteFailed] = useState<boolean>();
  const [deleteSuccessMessage, setDeleteSuccessMsg] = useState('');
  const [deleteFailedMessage, setDeleteFailedMsg] = useState('');
  const { deleteSauce, isDeleting } = useDeleteSauce();
  const { GetSauces, isLoading } = useGetSauces();
  
  useEffect(() => {
    (async () => {
      const sauces = await GetSauces()
      setSauces(sauces);
    })();}, []);

  if(isLoading){
    return <> </>
  }

  if(!sauces){
    return <NotFound> Something went wrong.... </NotFound>
  }
  
  const handleDeleteSauce = (s:SauceResponse) => {
    const baseMsg = `deletion of ${s.name} sauce`;
    setDeleteFailedMsg(baseMsg + ' failed' );
    setDeleteSuccessMsg(baseMsg + ' succeeded' )
    deleteSauce(s.id).then(r => {
      if(!r) {
        setDeleteFailed(true)
        return;
      }
      setDeleteSuccess(true)
      const updatedSauces = sauces.filter(sauce => sauce.id !== s.id)
      setSauces(updatedSauces);
    })
  };

  return(
    <Grid container style={{padding: '100px 50px'}} spacing={2}>
      {sauces.map(s => 
        <Grid xs={12} sm={6} lg={3} xl={2}>
          <SauceCardComponent sauce={s} handleDelete={handleDeleteSauce}/>
        </Grid>
      )}
      <ResponseSnackBars
        isGood={deleteSuccess}
        setGood={setDeleteSuccess}
        goodMessage={deleteSuccessMessage}
        isBad={deleteFailed}
        setBad={setDeleteFailed}
        badMessage={deleteFailedMessage}
      />
    </Grid>
  );
}


