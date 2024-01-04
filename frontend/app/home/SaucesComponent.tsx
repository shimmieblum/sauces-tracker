'use client'

import { SauceResponse } from "../../models/SauceResponseSchema";
import { useEffect, useState } from "react";
import { useGetSauces } from '../../hooks/useSaucesApi';
import {SauceCardComponent} from "./SauceCardComponent";
import {Grid} from "@mui/material";

export const SaucesComponent = () => {
  const [sauces, setSauces] = useState<SauceResponse[] | undefined>();
  const { GetSauces, isLoading } = useGetSauces();
  useEffect(() => {
    (async () => {
      const sauces = await GetSauces()
      setSauces(sauces);
    })();}, []);

  if(isLoading){
    return <> Loading... </>
  }

  if(!sauces){
    return <> Something went wrong.... </>
  }

  return(
    <Grid container style={{padding: '100px 50px'}} spacing={2}>
      {sauces.map(s => 
        <Grid xs={3} >
          <SauceCardComponent sauce={s}/>
        </Grid>
      )}
    </Grid>
  );
}

