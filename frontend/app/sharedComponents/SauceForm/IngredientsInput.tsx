import React, {createRef, RefObject, useRef} from 'react';
import {
  Grid,
  Box,
  IconButton,
  Typography
} from '@mui/material';
import {makeStyles} from "@mui/styles";
import MinusIcon from '@mui/icons-material/Remove'
import AddIcon from "@mui/icons-material/Add";
import {Ingredient} from "../../../models/IngredientSchema";
import {StyledTextInput} from "./StyledTextField";

const useStyles = makeStyles((theme) => ({
  gridBox: {
    flexGrow: 1,
    width: "100%",
    marginBottom: '10px',
    padding: '10px 0px',
    marginTop: '10px'
  },
  gridContainer: {
    width: '95%',
    margin: 'auto',
  },
  button: {
    margin: 2
  },
}));

export const IngredientsInput = (props: {
  title: string,
  entries: Ingredient[], 
  setEntries: (I:Ingredient[]) => void,
}) => {
  const classes = useStyles();
  const {entries, setEntries, title} = props;
  const entriesRef = useRef<Array<RefObject<HTMLInputElement>>>([]);
  if(entriesRef.current.length !== entries.length){
    entriesRef.current = entries.map(_ => createRef<HTMLInputElement>())
  }
  const addRow = () => {
    const lastEntry = entries[entries.length -1];
    if(!lastEntry || lastEntry.ingredient && lastEntry.percentage) {
      const updatedEntries = [...entries, {ingredient: '', percentage: 0}];
      setEntries(updatedEntries);
    }
  };

  const removeRow = (index: number) => {
    const newEntries = [...entries];
    newEntries.splice(index, 1);
    setEntries(newEntries);
    if(index > 0 && entriesRef.current[index - 1].current){
      entriesRef.current[index - 1].current?.focus();
    }
  };

  const updateEntries = (index: number, ingredient: string, percentage: number) => {
    const newEntries = [...entries];
    newEntries[index] = { ingredient: ingredient, percentage:percentage };
    setEntries(newEntries);
  };
  
  return (
    <Box component={'div'} className={classes.gridBox}>
      <Grid alignItems='center' className={classes.gridContainer} container spacing={1} key='header' marginBottom={2}>
        <Grid item  xs={8}>
          <Typography variant='h6'>Ingredient</Typography>
        </Grid>
        <Grid item xs={3}>
          <Typography variant='h6'>Percentage</Typography>
        </Grid>
        <Grid item xs={1}/>
      </Grid>
      <Box className={classes.gridContainer}>
        {entries.map((entry, index) => (
          <Grid alignItems='center' container spacing={1} key={index} marginBottom={2}>
            <Grid item xs={8}>
              <StyledTextInput
                inputRef={entriesRef.current[index]}
                fullWidth
                value={entry.ingredient}
                onChange={(e) => updateEntries(index, e.target.value, entry.percentage)}
                onKeyDown={k => {
                  if(['Backspace', 'Delete'].includes(k.key) && !entry.ingredient) {
                    removeRow(index);
                  }
                }}
                autoFocus={index == entries.length - 1}
              />
            </Grid>
            <Grid item xs={3}>
              <StyledTextInput
                type="number"
                fullWidth
                value={entry.percentage}
                onChange={(e) => updateEntries(index, entry.ingredient, parseInt(e.target.value))}
                onKeyDown={(k) => {
                  if (index === entries.length - 1 && k.key === 'Tab') {
                    k.preventDefault();
                    addRow();
                  }
                }}
              />
            </Grid>
            {index == entries.length - 1 &&
              <Grid item xs={1} >
                <IconButton
                  size='small'
                  color="primary"
                  className={classes.button}
                  onClick={() => removeRow(index)}
                >
                  <MinusIcon/>
                </IconButton>
              </Grid>}
          </Grid>
        ))}
        <Box component='div'>
          <IconButton
            size='small'
            color="primary"
            className={classes.button}
            onClick={() => {
              addRow()
            }}
          >
            <AddIcon/>
          </IconButton>
        </Box>
      </Box>
    </Box>
  );
};

