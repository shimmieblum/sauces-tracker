import {AppBar, Box, IconButton, Toolbar, Button, Typography, Menu, MenuItem} from "@mui/material";
import MenuIcon from '@mui/icons-material/Menu'
import Image from 'next/image';
import WhiteLogo from './WhiteLogo.png';
import {useState, MouseEvent, MouseEventHandler} from "react";
import {useRouter} from "next/navigation";

export const HeaderComponent = (props: {}) => {
  const [showMenu, setShowMenu] = useState(false);
  const [anchorEl, setAnchorEl] = useState<HTMLElement | null>(null);
  const router = useRouter();
  const handleMenuOpen = (e: MouseEvent<HTMLElement>) => {
    setShowMenu(true);
    setAnchorEl(e.currentTarget);
  }
  
  const handleMenuClose = () => {
    setShowMenu(false);
    setAnchorEl(null);
  }
  
   const NavMenu = () => (
    <Menu
      open={showMenu}
      anchorEl={anchorEl}
      keepMounted
      onClose={handleMenuClose}
    >
      <MenuItem onClick={() => router.push('/sauces')}>Sauces</MenuItem>
      <MenuItem onClick={() => router.push('/create-new-sauce')}>Create New Sauce</MenuItem>
    </Menu>
  );
  
  return <AppBar position={'static'}>
    <Toolbar 
      sx={{
        backgroundColor: 'black',
        borderBottom: 1,
        borderBottomColor: 'white',
      }}
    >
      <IconButton 
        size='large'
        edge='start'
        color='inherit'
        aria-label='menu'
        sx={{ mr: 2 }}
        onClick={handleMenuOpen}
      >
          <MenuIcon />
      </IconButton>
      <NavMenu/>
      <Typography variant='h6' component='div' sx={{flexGrow: 1}}>
        Sauces Database
      </Typography>
      <Box sx={{mr: 2}}>
        <Image width={100} src={WhiteLogo} alt='SauXe ltd.' />
      </Box>
    </Toolbar>
  </AppBar>;
}