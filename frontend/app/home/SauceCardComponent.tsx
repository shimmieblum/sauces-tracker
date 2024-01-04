'use client';

import {SauceResponse} from "../../models/SauceResponseSchema";
import {Alert, Card, CardActions, CardContent, IconButton, Typography} from "@mui/material";
import React, {useState} from "react";
import DeleteIcon from "@mui/icons-material/Delete"
import {useDeleteSauce} from "../../hooks/useSaucesApi";
import {Snackbar} from "@mui/base";

export const SauceCardComponent  = ({sauce}: {sauce:SauceResponse}) => {
  const { deleteSauce, isDeleting } = useDeleteSauce(sauce.id);
  const [deleteSuccess, setDeleteSuccess]  = useState<boolean | undefined>();
  const [deleteFailed, setDeleteFailed] = useState<boolean | undefined>();
  const confirmDelete = () => {
    return true;
  }
  
  const fermentationString  = sauce.fermentation.ingredients.map(i => i.ingredient).join(', ');
  const otherIngredientsString = sauce.nonFermentedIngredients.map(i => i.ingredient).join(', ');
  const ingredientString = `fermentation (${fermentationString}), ${otherIngredientsString})`;
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
          <Typography variant='body2' color='text.secondary'>{ingredientString}</Typography>
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
      <Snackbar
        open={deleteSuccess ?? false}
        style={{
          minWidth: 'fit-content',
          padding: '10px 0px'
        }}
        autoHideDuration={3000}
        onClose={() => setDeleteSuccess(undefined)}
      >
        <Alert severity={'success'}>
          {`deleted ${sauce.name} sauce`}
        </Alert>
      </Snackbar>
      <Snackbar
        open={deleteFailed ?? false}
        autoHideDuration={6000}
        onClose={() => setDeleteFailed(undefined)}
      >
        <Alert severity='error'>
          {`deletion of ${sauce.name} sauce failed`}
        </Alert>
      </Snackbar>
    </>
  );
}
