import {TextField} from "@mui/material";
import {makeStyles} from "@mui/styles";

const useStyles = makeStyles(theme => ({
  textField: {
    width: '90%',
    minWidth: 'fit-content',
    margin: "auto",
    marginTop: 10,
    marginBottom: 10
  }
}));

export const TextFieldInput = ({title, setValueFn, required=true, multiline=false}: {
  title:string,
  setValueFn: (val:string) => void,
  required?:boolean,
  multiline?:boolean
}) => {
  const classes = useStyles();
  return <TextField
    label={title}
    fullWidth
    required={required}
    onChange={(e) => setValueFn(e.target.value)}
    className={classes.textField}
    multiline={multiline}
  />
}

export const NumberFieldInput = ({title, setValueFn, required=true}: {
  title:string, 
  setValueFn: (val:number) => void, 
  required?:boolean
}) => {
  const classes = useStyles();
  return <TextField
    label={title}
    type='number'
    fullWidth
    required={required}
    onChange={(e) => setValueFn(parseInt(e.target.value))}
    className={classes.textField}
  />
}