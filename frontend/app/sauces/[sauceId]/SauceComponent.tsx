import {SauceResponse} from "../../../models/SauceResponseSchema";
import {
  Box,
  Button,
  Card,
  CardActions,
  CardContent,
  IconButton,
  Paper, Table, TableBody, TableCell,
  TableContainer, TableHead, TableRow,
  Typography
} from "@mui/material";
import {useState} from "react";
import {makeStyles} from "@mui/styles";
import {FermentationRequest} from "../../../models/SauceRequestSchema";
import {SauceForm} from "../../sharedComponents/SauceForm/SauceForm";
import {Ingredient} from "../../../models/IngredientSchema";


const useStyle = makeStyles(theme => ({
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
    }
  })
)

export const SauceComponent = ({sauce} : {sauce:SauceResponse}) => {
  const [editMode, setEditMode] = useState(false);
  const [name, setName] = useState(sauce.name);
  const [fermentation, setFermentation] = useState<FermentationRequest | undefined>(sauce.fermentation);
  const [sauceIngredients, setSauceIngredients] = useState(sauce.nonFermentedIngredients);
  const [fermentationPercentage, setFermentationPercentage] = useState(sauce.fermentationPercentage);
  const [notes, setNotes] = useState(sauce.notes);
  const classes = useStyle();
  
  const headers = ['Ingredients', '%']
  const IngredientsTable = ({ingredients}:{ingredients: Ingredient[] | undefined}) => (
    <Box margin={4} >
      <TableContainer component={Paper}>
        <Table sx={{minWidth: 'fit-content'}}>
          <TableHead >
            <TableRow >
              <TableCell>
                <Typography variant='h6'>
                  Ingredient
                </Typography>
              </TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {ingredients?.map((i,index) => (
              <TableRow key={index}>
                <TableCell>{i.ingredient}</TableCell>
                <TableCell align={'right'}>{i.percentage}%</TableCell>
              </TableRow>)
            )}
          </TableBody>
        </Table>
      </TableContainer>
    </Box>
  );
  
  const DisplaySauce = () => (
    <Card
      sx={{
        width: '50%',
        minWidth: 'fit-content',
        margin: 'auto',
        marginTop: 10,
        borderRadius: 10,
        padding: 5,
        alignItems: 'center'
      }}
    >
      <CardContent>
        <Typography variant='h4'>{name}</Typography>
        <Typography variant='h6'>Fermentation</Typography>
        <IngredientsTable ingredients={fermentation?.ingredients}/>
        <Typography variant='h6'>Sauce Recipe</Typography>
        <IngredientsTable 
            ingredients={[...sauceIngredients, {ingredient: 'Fermentation', percentage: fermentationPercentage}]} />
        <Typography variant='h6'>NOTES</Typography>
        <Typography variant='body1'>{sauce.notes}</Typography>
      </CardContent>
      <CardActions sx={{justifyContent: 'center'}}>
        <Button
          sx={{
            width: 'fit-content',
            margin: 1}}
          variant='contained'
          color='primary'
          onClick={() => setEditMode(true)}
        >
          Edit
        </Button>
      </CardActions>
    </Card>
    );
  
  
  const handleSubmitUpdate = () => {
    setEditMode(false);
  }
  
  const handleCancelUpdate = () => {
    setName(sauce.name);
    setFermentation(sauce.fermentation);
    setSauceIngredients(sauce.nonFermentedIngredients);
    setFermentationPercentage(sauce.fermentationPercentage);
    setNotes(sauce.notes);
    setEditMode(false);
  }
  
  const UpdateSauce = () => (
    <SauceForm
      title='EDIT SAUCE'
      name={{get: name, set: setName}}
      notes={{get:notes, set:setNotes}}
      fermentationPercentage={{
        get:fermentationPercentage,
        set:setFermentationPercentage
      }}
      fermentation={{
        get:fermentation,
        set:setFermentation
      }}
      nonFermentedIngredients={{
        get:sauceIngredients,
        set:setSauceIngredients,
      }}
      buttons={<>
        <Button sx={{margin: 1}} variant='contained' color='success' onClick={handleSubmitUpdate}>Save</Button>
        <Button sx={{margin: 1}} variant='contained' color='error' onClick={handleCancelUpdate}>Cancel</Button>
      </>}
    />);
  
  return <Box component={'div'}>
    {editMode ? <UpdateSauce/> : <DisplaySauce/>}
  </Box>;
}