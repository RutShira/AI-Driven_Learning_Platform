import React from 'react'
import { Typography, Paper, Box } from '@mui/material'

const LessonContent = ({ lesson }) => {
  return (
    <Paper elevation={3} style={{ padding: '16px', margin: '16px 0' }}>
      <Typography variant="h4" gutterBottom>
        {lesson.name}
      </Typography>
      <Box>
        <Typography variant="body1" paragraph>
          {lesson.content}
        </Typography>
        {lesson.video && (
          <video controls style={{ width: '100%', marginTop: '16px' }}>
            <source src={lesson.video} type="video/mp4" />
            Your browser does not support the video tag.
          </video>
        )}
      </Box>
    </Paper>
  )
}

export default LessonContent