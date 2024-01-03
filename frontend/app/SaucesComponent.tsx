'use client'

import { Sauce } from "../models/SauceSchema";
import { useEffect, useState } from "react";
import { useGetSauces } from '../hooks/useSaucesApi';
import {SauceCardComponent} from "./SauceCardComponent";
  
export const SaucesComponent = () => {
    const [sauces, setSauces] = useState<Sauce[] | undefined>();
    const { GetSauces, isLoading } = useGetSauces(); 
    useEffect(() => {
        (async () => {
            const sauces = await GetSauces()
            setSauces(sauces);
        })();
    }, []);

    if(isLoading){
        return <> Loading... </>
    }

    if(!sauces){
        return <> Something went wrong.... </>
    }

    return(
      <div style={{ padding: 16 }}>
        {sauces.map(s => <SauceCardComponent sauce={s}/>)}
    </div>);
}

