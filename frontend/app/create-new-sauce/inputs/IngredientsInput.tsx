import React, {createRef, RefObject, useRef} from 'react';
import {TextField, Grid, Box, IconButton, Typography} from '@mui/material';
import {makeStyles} from "@mui/styles";
import MinusIcon from '@mui/icons-material/Remove'
import AddIcon from "@mui/icons-material/Add";
import {Ingredient} from "../../../models/IngredientSchema";

const useStyles = makeStyles((theme) => ({
  gridBox: {
    border: '1px solid lightgrey',
    flexGrow: 1,
    margin: "auto",
    width: "90%",
    borderRadius: '5px',
    marginBottom: '10px',
    marginTop: '10px'
  },
  gridContainer: {
    width: '100%',
    margin: 'auto',
  },
  text: {
    padding: '5px',
  },
  button: {
    margin: 2
  },
}));

export const IngredientsInput = (props: {
  title: string,
  entries: Ingredient[], 
  setEntries: (I:Ingredient[]) => void
}) => {
  const classes = useStyles();
  const {entries, setEntries, title} = props;
  const entriesRef = useRef<Array<RefObject<HTMLInputElement>>>([]);
  if(entriesRef.current.length !== entries.length){
    entriesRef.current = entries.map(_ => createRef<HTMLInputElement>())
  }
  const addRow = () => {
    const updatedEntries = [...entries, { ingredient: '', percentage: 0 }]; 
    setEntries(updatedEntries);
  };

  const removeRow = (index: number) => {
    const newEntries = [...entries];
    newEntries.splice(index, 1);
    setEntries(newEntries);
    if(index > 0 && entriesRef.current[index - 1].current){
      entriesRef.current[index - 1].current?.focus();
    }
  };

  const handleInputChange = (index: number, ingredient: string, percentage: number) => {
    const newEntries = [...entries];
    newEntries[index] = { ingredient: ingredient, percentage:percentage };
    setEntries(newEntries);
  };
  
  
  return (
    <Box component={'div'} className={classes.gridBox}>
      <Typography variant='body2' className={classes.text}>
        Ingredients
      </Typography>
      {entries.map((entry, index) => (
        <Grid alignItems='center' className={classes.gridContainer} container spacing={1} key={index} marginBottom={2}>
          <Grid item xs={8}>
            <TextField
              label="Ingredient"
              inputRef={entriesRef.current[index]}
              value={entry.ingredient}
              onChange={(e) => handleInputChange(index, e.target.value, entry.percentage)}
              onKeyDown={k => {
                if( ['Backspace', 'Delete'].includes(k.key) && !entry.ingredient) {
                  removeRow(index);
                }
              }}
              autoFocus={index == entries.length - 1}
              fullWidth
            />
          </Grid>
          <Grid item xs={3}>
            <TextField
              label="Percentage"
              type="number"
              value={entry.percentage}
              onChange={(e) => handleInputChange(index, entry.ingredient, parseInt(e.target.value))}
              fullWidth
              onKeyDown={(k) => {
                if (index === entries.length - 1 && k.key === 'Tab') {
                  k.preventDefault();
                  addRow();
                }
              }}
            />
          </Grid>
          {index == entries.length - 1 ?
            <Grid item xs={1} >
              <IconButton
                size='small'
                color="primary"
                className={classes.button}
                onClick={() => removeRow(index)}
              >
                <MinusIcon/>
              </IconButton>
            </Grid> : <></> 
          }
        </Grid>
      ))}
      <Box component='div'>
        <IconButton
          size='small'
          color="primary"
          className={classes.button}
          onClick={() => {
            addRow()
          }
        }
        >
          <AddIcon/>
        </IconButton>  
      </Box>
    </Box>
  );
};

