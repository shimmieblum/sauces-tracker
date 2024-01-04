import React, {useEffect, useState} from "react";
import {Box, Button, TextField} from "@mui/material";
import {makeStyles} from '@mui/styles';
import {SauceRequest} from "../../models/SauceRequestSchema";
import {useCreateSauce, useDeleteSauce} from "../../hooks/useSaucesApi";
import {useInput} from "./UseInput";

const useStyles = makeStyles((theme) => ({
  form: {
    margin: "auto",
    minWidth: '50%',
    maxWidth: 'fit-content',
    display: 'flex', 
    flexDirection: 'column',
    alignItems: 'center', 
    justifyContent: 'center',
    background: 'white',
    marginTop: 10,
    padding: 50,
    borderRadius: 50,
  },
  button: {
    margin: 5,
  },
  textField: {
    margin: 5
  }
}));

export const NewSauceForm = () => {
  const classes = useStyles();
  
  const [fermentationRecipe, setFermentationRecipe] = useState(new Map<string, number>())
  const {value:name, handleChange:handleNameChange} = useInput('');
  const {value:notes, handleChange:handleNotesChange} = useInput('');

  const handleSubmit = () => {
    debugger;
    const {createSauce} = useCreateSauce();
    const sauce:SauceRequest = {
      name: name,
      notes: notes,
      nonFermentedIngredients: [{
        ingredient:'red pepper',
        percentage: 100
      }],
      fermentation: {
        ingredients: [{
          ingredient: 'red pepper', percentage: 100
        }],
        lengthInDays: 30
      },
      fermentationPercentage: 30
    }
    console.log(sauce);
    let success = true;    
    createSauce(sauce)
      .then(success => {
        if(!success) {
          console.log('failed to create new sauce')
          success = false;
        }
      })
      .catch(err => console.error(err, {message: `error occurred creating sauce`}));
    console.log(success);
  }
  
  return (
    <Box className={classes.form} component='form' onSubmit={handleSubmit}> 
      <TextField 
        label='name'
        fullWidth
        required
        onChange={handleNameChange}
        className={classes.textField}
      />
      <TextField
        label='notes'
        fullWidth
        required
        onChange={handleNotesChange}
        className={classes.textField}
      />
      <Button 
        className={classes.button}
        variant='contained'
        color='primary' 
        type='submit'
      >
        Create Sauce
      </Button>
    </Box>
  );
}




