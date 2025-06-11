import React, { useEffect } from 'react';
import { Card, CardContent, Typography, Button, List, ListItem, ListItemText, CircularProgress } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { fetchSubCategories, fetchSubCategoriesByCategory } from '../store/thunk';
import { clearSubCategories } from '../store/subCategoriesSlice';
import { useNavigate } from 'react-router-dom';

const CourseCard = ({ course, user, onEnroll }) => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
 const subCategories = course.subCategories || [];  
 



  return (
    <Card variant="outlined" style={{ margin: '16px' }}>
      <CardContent>
        <Typography variant="h5" component="div">
          {course.name}
          
        </Typography>
         <Typography variant="h5" component="div">
          {Number(course.categoryId)}
           
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {course.description}
        </Typography>
        {course.status === 'loading' && <CircularProgress size={20} />}
        {subCategories&& subCategories.length > 0 && (
          <>
            <Typography variant="subtitle1" sx={{ mt: 2 }}>Sub Categories:</Typography>
            <List dense>
              {subCategories.map((sub) => (
                <ListItem key={sub.subCategoryId}>
                  <ListItemText primary={sub.name} />
                </ListItem>
              ))}
            </List>
          </>
        )}
        <Button
          variant="contained"
          color="primary"
          onClick={() => {
            if (!user) {
              navigate(`/lesson/${course.categoryId}`);
              onEnroll();
            } else {
              // Redirect to login if user is not logged in
              window.location.href = '/login';
            }
           
          }}
          style={{ marginTop: '16px' }}
        >
          Enroll
        </Button>
      </CardContent>
    </Card>
  );
};

export default CourseCard;