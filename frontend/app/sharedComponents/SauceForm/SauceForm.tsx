import {Box, Typography} from "@mui/material";
import React, {ReactNode, useState} from "react";
import {makeStyles} from "@mui/styles";
import {FermentationRequest} from "../../../models/SauceRequestSchema";
import {Ingredient} from "../../../models/IngredientSchema";
import {IngredientsInput} from "./IngredientsInput";
import {StyledTextInput} from "./StyledTextField";


const useStyles = makeStyles((theme) => ({
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
    color: 'black',
    borderRadius: 50,
  },
  FermentationBox: {
    marginBottom: '10px',
    width: '90%',
    margin: 'auto',
    borderRadius: '5px',
    display: 'flex',
    flexDirection: 'column',
  },
  FormSection:{
    marginBottom: '10px',
    margin: 'auto',
    display: 'flex',
    flexDirection: 'column',
    width: '100%'
  }
}));

type state<T> = {
  get: T;
  set: (t:T) => void;
}


type SauceFormProps = {
  title:string;
  name: state<string>;
  notes: state<string>;
  fermentationPercentage: state<number>;
  fermentation: state<FermentationRequest | undefined>;
  nonFermentedIngredients: state<Ingredient[]>;
  buttons?: React.ReactNode;
}


export const SauceForm = (props: SauceFormProps) => {
  
  const {name, notes, fermentationPercentage, fermentation, nonFermentedIngredients, buttons, title} = props;
  const classes = useStyles();
  return (
  <Box className={classes.form} component='div'>
    <Typography margin='20px' variant='h4'>
      {title}
    </Typography>
    <StyledTextInput
      label='name' 
      fullWidth
      value={name.get}
      onChange={(e)=> name.set(e.target.value)}
      autoFocus
    />
    
    <FermentationRecipeInput
      setFermentationFn={fermentation.set}
    />
    <NonFermentedIngredientsInput
      setIngredients={nonFermentedIngredients.set}
      ingredients={nonFermentedIngredients.get}
      fermentedPercentage={fermentationPercentage.get}
      setFermentedPercentage={fermentationPercentage.set}
    />
    <StyledTextInput
      fullWidth
      multiline 
      value={notes.set}
      label='notes'
      onChange={(e) => notes.set(e.target.value)}
    />
    {buttons}
  </Box>);
}


const FermentationRecipeInput = ({setFermentationFn}: {
  setFermentationFn: (f:FermentationRequest) => void,
}) => {
  const classes = useStyles();
  const [ingredients, setIngredients] = useState<Ingredient[]>([{ingredient: '', percentage: 0}]);
  const [lengthInDays, setLengthInDays] = useState(0);
  const handleChange = () => {
    setFermentationFn({
      ingredients: ingredients,
      lengthInDays: lengthInDays
    })
  };

  return(
    <FormSection title='Fermentation Recipe'>
      <IngredientsInput
        title='Ingredients'
        entries={ingredients} 
        setEntries={setIngredients}
      />
      <StyledTextInput
        fullWidth
        type='number'
        onChange={(e) => setLengthInDays(parseInt(e.target.value))}  
        value={lengthInDays}
        label='Length (days)'
      />
    </FormSection>
  );
}


const NonFermentedIngredientsInput = ({ingredients, setIngredients, setFermentedPercentage, fermentedPercentage} : {
  fermentedPercentage:number,
  ingredients: Ingredient[],
  setIngredients: (f:Ingredient[]) => void,
  setFermentedPercentage: (n:number) => void,
}) => {
  const classes = useStyles();
  return(
    <FormSection title='Sauce Recipe'>
      <StyledTextInput
        fullWidth
        type='number'
        value={fermentedPercentage}
        label='Fermentation Percentage'
        onChange={(e) => setFermentedPercentage(parseInt(e.target.value))}
      />
      <IngredientsInput
        title='Ingredients'
        entries={ingredients}
        setEntries={setIngredients}
      />
    </FormSection>);
}

const FormSection = ({title, children}:{title:string, children:ReactNode}) => {
  const classes = useStyles();
  return (
    <Box component='div' className={classes.FormSection}>
      <Typography  variant='h6'>{title}</Typography>
      {children}
    </Box>
  );
}



