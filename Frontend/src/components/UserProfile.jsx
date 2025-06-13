import React from 'react';
import { Card, CardContent, Typography, Button } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { clearUser } from '../store/userSlice';

const UserProfile = () => {
  const dispatch = useDispatch();
  const user = useSelector((state) => state.user.user);

  if (!user) return null;

  return (
    <Card variant="outlined">
      <CardContent>
        <Typography variant="h5" component="div">
          User Profile
        </Typography>
        <Typography sx={{ mb: 1.5 }} color="text.secondary">
          Name: {user.name}
        </Typography>
        <Typography sx={{ mb: 1.5 }} color="text.secondary">
          Email: {user.email}
        </Typography>
        <Typography sx={{ mb: 1.5 }} color="text.secondary">
          Courses Enrolled: {user.coursesEnrolled ?? '-'}
        </Typography>
        <Button variant="contained" color="primary" onClick={() => dispatch(clearUser())}>
          Logout
        </Button>
      </CardContent>
    </Card>
  );
};

export default UserProfile;