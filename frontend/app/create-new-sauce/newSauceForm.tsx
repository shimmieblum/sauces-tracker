import React, {FormEvent, useState} from "react";
import {Box, Button, Typography} from "@mui/material";
import {FermentationRequest, SauceRequest} from "../../models/SauceRequestSchema";
import {useCreateSauce} from "../../hooks/useSaucesApi";
import {Ingredient} from "../../models/IngredientSchema";
import {ResponseSnackBars} from "../sharedComponents/ResponseSnackBars";
import {useRouter} from "next/navigation";
import {makeStyles} from "@mui/styles";
import {SauceForm} from "../sharedComponents/SauceForm/SauceForm";

const useStyles = makeStyles(theme => ({
  submitSuccessBox: {
    margin: "auto",
    width: '50%',
    maxWidth: '90%',
    minWidth: 'fit-content',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
  },
  button: {
    margin: 5,
  },
}))

export const NewSauceForm = () => {
  const classes = useStyles();
  const [createSuccess, setCreateSuccess] = useState<boolean | undefined>()
  const [createFailed, setCreateFailed] = useState<boolean | undefined>()
  const [isFormSubmitted, setFormSubmitted] = useState(false);

  const [fermentation, setFermentation] = useState<FermentationRequest>();
  const [fermentationPercentage, setFermentationPercentage] = useState(0);
  const [name, setName] = useState('');
  const [notes, setNotes] = useState('');
  const [sauceId, setSauceId] = useState('');
  const [nonFermentedIngredients, setNonFermentedIngredients] = useState<Ingredient[]>([{ingredient: '', percentage: 0}]);

  
  const router = useRouter();
  const handleSubmit = (event:FormEvent<HTMLFormElement>) => {
    if(!fermentation){
      console.error(`no fermentation defined`);
      return;
    }
    const {createSauce} = useCreateSauce();
    const sauce:SauceRequest = {
      name: name,
      notes: notes,
      nonFermentedIngredients: nonFermentedIngredients,
      fermentation: fermentation,
      fermentationPercentage: fermentationPercentage
    }
    createSauce(sauce)
      .then(id => {
        setCreateSuccess(Boolean(id))
        setCreateFailed(!id)
        if(!id) {
          console.log('failed to create new sauce')
          return;
        }
        setSauceId(id)
        setFormSubmitted(true);
      })
      .catch(err => console.error(err, {message: `error occurred creating sauce`}));
    event.preventDefault();
  }
  
  const navigateToSauce = () => {
    router.push(`/sauces/${sauceId}`)
  }
  
  const SubmitButton = (
    <Button
    className={classes.button}
    variant='contained'
    color='primary'
    type='submit'
  >
    Create Sauce
  </Button>);
  
  const FormSubmitted = () => {
    return (
    <Box className={classes.submitSuccessBox} component='div'>
      <Typography variant='h4' color={'white'}>
        Sauce submitted successfully
      </Typography>
      <Button
        className={classes.button}
        variant='contained'
        color='primary' 
        onClick={() => setFormSubmitted(false)}>
        Create another? 
      </Button>
      <Button
        className={classes.button}
        variant='contained'
        color='primary'
        onClick={navigateToSauce}
       >
        Go to Sauce
      </Button>
    </Box>);
  }
  return (
    <>
      {isFormSubmitted 
        ? <FormSubmitted /> 
        : <Box component='form' onSubmit={handleSubmit}>
            <SauceForm 
              title='CREATE NEW SAUCE'
              name={{get: name, set: setName}} 
              notes={{get: notes, set: setNotes}} 
              fermentationPercentage={{get:fermentationPercentage, set:setFermentationPercentage}}
              fermentation={{get:fermentation, set:setFermentation}} 
              nonFermentedIngredients={{get:nonFermentedIngredients, set:setNonFermentedIngredients}}
              buttons={SubmitButton}
            />
          </Box>
      }
      <ResponseSnackBars
        isGood={createSuccess}
        setGood={setCreateSuccess}
        goodMessage={'successfully created sauce'}
        isBad={createFailed} setBad={setCreateFailed}
        badMessage={'failed to create sauce'}
      />
    </>
  );
}
