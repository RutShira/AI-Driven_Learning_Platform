import React from 'react';
import CourseList from '../components/CourseList';
import { Container, Typography } from '@mui/material';

function Courses() {
  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Available Courses
      </Typography>
      <CourseList />
    </Container>
  );
}

export default Courses;