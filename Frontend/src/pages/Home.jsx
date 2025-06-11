import React from 'react';
import { Container, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <Container>
      <Typography variant="h2" gutterBottom>
        Welcome to the Learning Platform
      </Typography>
      <Typography variant="h5" gutterBottom>
        Explore a variety of courses and enhance your skills. Join us today and start your learning journey!
      </Typography>
      <Button variant="contained" color="primary" component={Link} to="/courses">
        Browse Courses
      </Button>
    </Container>
  );
};

export default Home;