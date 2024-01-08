import {BoxProps} from "@mui/system";
import {Box} from "@mui/material";

export const NotFound = ({...props}: BoxProps) => (
  <Box component='div'
       bgcolor='red'
       color='white'
       width='100%'
       textAlign='center'
       justifyContent='center'
    {...props}/>);