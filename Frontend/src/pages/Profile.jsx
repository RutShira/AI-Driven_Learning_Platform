import React from 'react';
import { Container, Typography } from '@mui/material';
import UserProfile from '../components/UserProfile';
import LoginForm from '../components/LoginForm';

const Profile = () => {
  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        User Profile
      </Typography>
      <LoginForm />
    </Container>
  );
};

export default Profile;