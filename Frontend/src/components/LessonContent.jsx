import React, { useEffect } from 'react'
import { Typography, Paper, Box, ListItem, ListItemText, List } from '@mui/material'
import { fetchSubCategories } from '../store/thunk';
import { useDispatch, useSelector } from 'react-redux';

const LessonContent = ({ lesson }) => {



  //סינון תתי־קטגוריות שרלוונטיים לקטגוריה הנוכחית (אם צריך)
  


  return (
    <Paper elevation={3} style={{ padding: '16px', margin: '16px 0' }}>
      <Typography variant="h4" gutterBottom>
        {/* {lesson.id} */}
      </Typography>
           {loading === 'loading' && <CircularProgress size={20} />}
           {error && <Typography color="error">{error}</Typography>} 
       { filteredSubCategories && filteredSubCategories.length > 0 && (
          <>
            <Typography variant="subtitle1" sx={{ mt: 2 }}>Sub Categories:</Typography>
            <List dense>
              {filteredSubCategories.map((sub) => (
                <ListItem key={sub.subCategoryId}>
                  <ListItemText primary={sub.name} />
                </ListItem>
              ))}
            </List>
          </>
        )}
   
    </Paper>
  )
}

export default LessonContent