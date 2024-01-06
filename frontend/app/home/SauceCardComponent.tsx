
import {SauceResponse} from "../../models/SauceResponseSchema";
import {
  Alert, Button,
  Card,
  CardActions,
  CardContent,
  Dialog, DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  Typography
} from "@mui/material";
import React, {useState} from "react";
import DeleteIcon from "@mui/icons-material/Delete"
import {useDeleteSauce} from "../../hooks/useSaucesApi";
import {Snackbar} from "@mui/base";
import {ResponseSnackBars} from "../sharedComponents/ResponseSnackBars";

export const SauceCardComponent  = ({sauce, handleDelete}: {sauce:SauceResponse, handleDelete: (sauce:SauceResponse) => void}) => {
  
  const [ showDeleteConfirmation, setShowDeleteConfirmation] = useState(false);
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
            // disabled={true} // disabled for now until the endpoint is fixed
            onClick={() => setShowDeleteConfirmation(true)}
          >
            <DeleteIcon/>
          </IconButton>
        </CardActions>
      </Card>
      <ConfirmationDialog
        title='Caution'
        content='Deleting the sauce is irreversible. Are you sure you want to continue?'
        open={showDeleteConfirmation}
        handleCancel={() => {
          setShowDeleteConfirmation(false);
        }}
        handleOk={() => {
          handleDelete(sauce);
          setShowDeleteConfirmation(false);
        }}
      />
    </>
  );
}

type ConfirmationDialogProps = {
  open: boolean;
  title: string;
  content: string;
  handleCancel: () => void;
  handleOk: () => void;
}

const ConfirmationDialog = (props: ConfirmationDialogProps) => {
  const {open, handleOk, handleCancel , title, content} = props;
  return (
    <Dialog 
      open={open}
    >
      <DialogTitle>
        {title}
      </DialogTitle>
      <DialogContent>
        {content}
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel}>Cancel</Button>
        <Button onClick={handleOk} autoFocus>Ok</Button>
      </DialogActions>
    </Dialog>)
} 
