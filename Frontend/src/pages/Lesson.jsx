import React from 'react';
import LessonContent from '../components/LessonContent';
import { Container, Typography } from '@mui/material';

const Lesson = () => {
  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Lesson Title
      </Typography>
      <LessonContent />
    </Container>
  );
};

export default Lesson;