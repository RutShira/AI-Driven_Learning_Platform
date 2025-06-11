import React, { useEffect } from 'react';
import CourseCard from './CourseCard';
import { Grid, CircularProgress, Typography } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCategories, fetchSubCategoriesByCategory } from '../store/thunk';
import { useNavigate } from 'react-router-dom';

const CourseList = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const user = useSelector((state) => state.user.user);
  const { data: courses, status, error } = useSelector((state) => state.categories);

  const categories = useSelector((state) => state.categories.data);
    useEffect(() => {
   
     dispatch(fetchCategories());
      if (categories && categories.length > 0) {
        for (const category of categories) {
           dispatch(fetchSubCategoriesByCategory(category.categoryId));
        }
      }
      
    }, [dispatch]);

    


  if (status === 'loading') return <CircularProgress />;
  if (status === 'failed') return <Typography color="error">{error}</Typography>;

  return (
    <Grid container spacing={2}>
      {courses && courses.length > 0 ? (
        courses.map((course) => (
          <Grid item xs={12} sm={6} md={4} key={course.categoryId}>
            <CourseCard
              course={course}
              user={user}
              onEnroll={() => {
                if (!user) {
                  // Navigate to the course details or enrollment page
                  
                } else {
                  // Redirect to login if user is not logged in
                  navigate('/login');
                }
              }}
            />
          </Grid>
        ))
      ) : (
        <Typography sx={{ m: 2 }}>No courses found.</Typography>
      )}
    </Grid>
  );
};

export default CourseList;