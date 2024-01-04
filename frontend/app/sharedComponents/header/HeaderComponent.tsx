import {AppBar, Box, IconButton, Toolbar, Button, Typography} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu'
import Image from 'next/image';
import WhiteLogo from './WhiteLogo.png';

export const HeaderComponent = (props: {}) => {
    
  return <AppBar position={'static'}>
    <Toolbar sx={{
      backgroundColor: 'black',
      borderBottom: 1,
      borderBottomColor: 'white',
    }}
    >
      <IconButton size='large' edge='start' color='inherit' aria-label='menu' sx={{ mr: 2 }}>
          <MenuIcon />
      </IconButton>
      <Typography variant='h6' component='div' sx={{flexGrow: 1}}>
        Sauces Database
      </Typography>
      <Box sx={{mr: 2}}>
        <Image width={100} src={WhiteLogo} alt='SauXe ltd.' />
      </Box>
    </Toolbar>
  </AppBar>;
}