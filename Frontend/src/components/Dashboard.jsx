import React, { useEffect } from 'react';
import { Grid, Typography, CircularProgress } from '@mui/material';
import CourseList from './CourseList';
import Sidebar from './Sidebar';
import { useDispatch, useSelector } from 'react-redux';
import { fetchCategories } from '../store/thunk';

const Dashboard = () => {
  const dispatch = useDispatch();
  const { status, error } = useSelector((state) => state.categories);
 

  useEffect(() => {
    dispatch(fetchCategories());
  }, [dispatch]);

  return (
    <Grid container spacing={2}>
      <Grid item xs={3}>
        <Sidebar />
      </Grid>
      <Grid item xs={9}>
        <Typography variant="h4" gutterBottom>
          Dashboard
        </Typography>

        {status === 'loading' && <CircularProgress />}
        {status === 'failed' && <Typography color="error">{error}</Typography>}

        {/* השינוי הקריטי כאן */}
        {status === 'succeeded' && <CourseList />}
      </Grid>
    </Grid>
  );
};

export default Dashboard;