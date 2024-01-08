import React, {FormEvent, useState} from "react";
import {Box, Button, Card, Paper, TextField, Typography} from "@mui/material";
import {makeStyles} from '@mui/styles';
import {FermentationRequest, SauceRequest} from "../../models/SauceRequestSchema";
import {useCreateSauce, useDeleteSauce} from "../../hooks/useSaucesApi";
import {IngredientsInput} from "./inputs/IngredientsInput";
import {Ingredient} from "../../models/IngredientSchema";
import {TextFieldInput, NumberFieldInput} from "./inputs/FieldInputs";
import {ResponseSnackBars} from "../sharedComponents/ResponseSnackBars";
import {BuildTwoTone} from "@mui/icons-material";
import {useRouter} from "next/navigation";

const useStyles = makeStyles((theme) => ({
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
  form: {
    margin: "auto",
    width: '50%',
    maxWidth: '90%',
    minWidth: 'fit-content',
    display: 'flex', 
    flexDirection: 'column',
    alignItems: 'center', 
    justifyContent: 'center',
    background: 'white',
    marginTop: 10,
    padding: 50,
    borderRadius: 50,
  },
  FermentationBox: {
    marginTop: 5,
    width: "90%",
    margin: 'auto',
    border: '1px solid lightgrey',
    borderRadius: '5px',
    display: 'flex',
    flexDirection: 'column',
  },
  text: {
    padding: '10px',
  },
  button: {
    margin: 5,
  },
}));

export const NewSauceForm = () => {
  const classes = useStyles();
  const [fermentation, setFermentation] = useState<FermentationRequest>();
  const [fermentationPercentage, setFermentationPercentage] = useState(0);
  const [name, setName] = useState('');
  const [notes, setNotes] = useState('');
  const [sauceId, setSauceId] = useState('');
  const [nonFermentedIngredients, setNonFermentedIngredients] = useState<Ingredient[]>([{ingredient: '', percentage: 0}]);
  const [createSuccess, setCreateSuccess] = useState<boolean | undefined>()
  const [createFailed, setCreateFailed] = useState<boolean | undefined>()
  const [isFormSubmitted, setFormSubmitted] = useState(false);
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
        : <Box className={classes.form} component='form' onSubmit={handleSubmit}>
          <Typography margin='20px' variant='h4'>
            CREATE NEW SAUCE
          </Typography>
          <TextFieldInput title='name' setValueFn={setName} autoFocus/>
          <FermentationRecipeInput 
            setFermentationFn={setFermentation}
          />
          <NonFermentedIngredientsInput 
            setIngredients={setNonFermentedIngredients} 
            ingredients={nonFermentedIngredients} 
            setFermentedPercentage={setFermentationPercentage}
          />
          <TextFieldInput multiline required={false} title={'notes'} setValueFn={setNotes}/>
          <Button 
            className={classes.button} 
            variant='contained' 
            color='primary' 
            type='submit'
          >
            Create Sauce
          </Button>
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

const FermentationRecipeInput = (props: {
  setFermentationFn: (f:FermentationRequest) => void,
}) => {
  const classes = useStyles();
  const [ingredients, setIngredients] = useState<Ingredient[]>([{ingredient: '', percentage: 0}]);
  const [lengthInDays, setLengthInDays] = useState(0);
  const handleChange = () => {
    props.setFermentationFn({
      ingredients: ingredients,
      lengthInDays: lengthInDays
    })
  };

  return(
    <Box className={classes.FermentationBox} component='div' onChange={handleChange}>
      <Typography className={classes.text} variant='body1'>
        Fermentation Recipe
      </Typography>
      <IngredientsInput title='Ingredients' entries={ingredients} setEntries={setIngredients}/>
      <NumberFieldInput title='Length (days)' setValueFn={setLengthInDays}/>
  </Box>);
}



const NonFermentedIngredientsInput = ({ingredients, setIngredients, setFermentedPercentage} : {
  ingredients: Ingredient[],
  setIngredients: (f:Ingredient[]) => void,
  setFermentedPercentage: (n:number) => void,
}) => {
  const classes = useStyles();
  
  return(
    <Box className={classes.FermentationBox} component='div'>
      <Typography className={classes.text} variant='body1'>
        Sauce Recipe
      </Typography>
      <NumberFieldInput title='Fermentation Percentage' setValueFn={setFermentedPercentage} />
      <IngredientsInput title='Ingredients' entries={ingredients} setEntries={setIngredients}/>
    </Box>);
}



