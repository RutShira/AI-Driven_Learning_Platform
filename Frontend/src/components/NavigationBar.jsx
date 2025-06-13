import React, { useEffect, useState } from 'react';
import { AppBar as MuiAppBar, Toolbar, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';


const NavigationBar = () => {


  return (
    <MuiAppBar position="static">
      <Toolbar>
        <Typography variant="h6" sx={{ flexGrow: 1 }}>
          Learning Platform
        </Typography>
        <Button color="inherit" component={Link} to="/">
          Home
        </Button>
        <Button color="inherit" component={Link} to="/courses">
          Courses
        </Button>
        <Button color="inherit" component={Link} to="/login">
          Login
        </Button>
        <Button color="inherit" component={Link} to="/signup">
          Sign Up
        </Button>
      </Toolbar>
    </MuiAppBar>
  );
};

export default NavigationBar;