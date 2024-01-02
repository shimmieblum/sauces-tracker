'use client'

import { Sauce } from "../models/SauceSchema";
import { useEffect, useState } from "react";
import {
    Card,
    CardContent,
    Typography,
    List,
    ListItem,
    ListItemText,
  } from '@mui/material';
import { useGetSauces } from '../hooks/useGetSauces';
  
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
        {sauces.map(s => <SauceCard sauce={s}/>)}
    </div>);
}

const SauceCard = ({sauce}: {sauce:Sauce}) => {

    return <Card key={sauce.id}>
        <CardContent>
            <Typography variant="h5" component="div">
                {sauce.name}
            </Typography>
            <Typography variant="body1" style={{ marginTop: 8 }}>
                Notes: {sauce.notes}
            </Typography>
            <Typography variant="body1" style={{ marginTop: 8 }}>
                Fermentation Percentage: {sauce.fermentationPercentage}%
            </Typography>

            <Typography variant="body1" style={{ marginTop: 8 }}>
                Fermented Ingredients:
            </Typography>
            <List>
                {sauce.fermentation.ingredients.map((ingredient) => (
                  <ListItem key={ingredient.ingredient}>
                      <ListItemText>
                          {ingredient.ingredient}: {ingredient.percentage}%
                      </ListItemText>
                  </ListItem>
                ))}
            </List>

            <Typography variant="body1" style={{ marginTop: 8 }}>
                Non-Fermented Ingredients:
            </Typography>
            <List>
                {sauce.nonFermentedIngredients.map((ingredient) => (
                  <ListItem key={ingredient.ingredient}>
                      <ListItemText>
                          {ingredient.ingredient}: {ingredient.percentage}%
                      </ListItemText>
                  </ListItem>
                ))}
            </List>
        </CardContent>
        </Card>;
}
