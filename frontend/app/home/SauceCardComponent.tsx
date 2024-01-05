'use client';

import {SauceResponse} from "../../models/SauceResponseSchema";
import {Alert, Card, CardActions, CardContent, IconButton, Typography} from "@mui/material";
import React, {useState} from "react";
import DeleteIcon from "@mui/icons-material/Delete"
import {useDeleteSauce} from "../../hooks/useSaucesApi";
import {Snackbar} from "@mui/base";
import {ResponseSnackBars} from "../sharedComponents/ResponseSnackBars";

export const SauceCardComponent  = ({sauce}: {sauce:SauceResponse}) => {
  const { deleteSauce, isDeleting } = useDeleteSauce(sauce.id);
  const [deleteSuccess, setDeleteSuccess]  = useState<boolean | undefined>();
  const [deleteFailed, setDeleteFailed] = useState<boolean | undefined>();
  const confirmDelete = () => {
    return true;
  }
  
  const fermentationString  = `fermentation (${sauce.fermentationPercentage}%): ${sauce.fermentation.ingredients.map(i => i.ingredient).join(', ')}`;
  const otherIngredientsString = `other ingredients: (${100 - sauce.fermentationPercentage}%): ${sauce.nonFermentedIngredients.map(i => i.ingredient).join(', ')}`;
  return (
    <>
      <Card sx={{
        maxHeight: 200, 
        minHeight: 100,
        height: 200,
        margin: 2
      }}>
        <CardContent>
          <Typography gutterBottom variant='h5' component='div'>{sauce.name}</Typography>
          <Typography variant='body2' color='text.secondary'>{fermentationString}</Typography>
          <Typography variant='body2' color='text.secondary'>{otherIngredientsString}</Typography>
        </CardContent>  
        <CardActions>
          <IconButton 
            aria-label='delete'
            disabled={true} // disabled for now until the endpoint is fixed
            onClick={() => {
              if(confirmDelete()) 
                deleteSauce()
                  .then(r => r ? setDeleteSuccess(true) : setDeleteFailed(true))
              }}>
            <DeleteIcon/>
          </IconButton>
        </CardActions>
      </Card>
      <ResponseSnackBars
        isGood={deleteSuccess} 
        setGood={setDeleteSuccess}
        goodMessage={`successfully deleted ${sauce.name}`}
        isBad={deleteFailed}
        setBad={setDeleteFailed}
        badMessage={`failed to delete ${sauce.name}`}
      />
    </>
  );
}
