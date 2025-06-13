import React, { useEffect, useState } from 'react';
import { Card, CardContent, Typography, Button } from '@mui/material';
import ChatComponent from './ChatComponent'; // קומפוננטת הצ'אט שלך
import { useDispatch, useSelector } from 'react-redux';
import { fetchSubCategories } from '../store/thunk';
import { useNavigate } from 'react-router-dom';

const CourseCard = ({ course ,user}) => {
  const [showSubCategories, setShowSubCategories] = useState(false);
  const [selectedSubCategory, setSelectedSubCategory] = useState(null);
  const navigate = useNavigate();


  const handleEnroll = () => {
    if (!user) {
      navigate(`/course/${course.categoryId}`); 
      setShowSubCategories(true);
    } else {
     navigate("/login")// או טיפול אחר במצב שבו אין יוזר
    }
  };

  const handleSubCategoryClick = (subCategory) => {
    setSelectedSubCategory(subCategory);
  };


  return (
    <Card variant="outlined" style={{ margin: '16px' }}>
      <CardContent>
        <Typography variant="h5" component="div">
          {course.name}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {course.description}
        </Typography>

        <Button
          variant="contained"
          color="primary"
          onClick={handleEnroll}
          style={{ marginTop: '16px' }}
        >
          Enroll
       
        </Button>

    
      </CardContent>
    </Card>
  );
};

export default CourseCard;
