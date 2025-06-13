import React, { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Button, Typography, CircularProgress } from '@mui/material';
import ChatComponent from './ChatComponent'; // קומפוננטת הצ'אט שלך
import { fetchCategoryByID, fetchSubCategories } from '../store/thunk';
import { useDispatch, useSelector } from 'react-redux';

const CoursePage = () => {
    const { courseId } = useParams(); // קבלת ה-ID של הקורס מה-URL
    const [selectedSubCategory, setSelectedSubCategory] = React.useState(null);
    const user = useSelector((state) => state.user.user);
    const corse = useSelector((state) => state.category.category);
    const dispatch = useDispatch();
   
    const { items: subCategories, loading, error } = useSelector((state) => state.subCategories);

    useEffect(() => {  
        dispatch(fetchCategoryByID(courseId));  
        dispatch(fetchSubCategories());
    }, [dispatch, courseId]);

    const filteredSubCategories = subCategories
        ? subCategories.filter((sub) => sub.categoryId === corse.categoryId)
        : [];

    return (
        <div>
            <Typography variant="h4">Course: {corse.name}</Typography>
            <div>
                {loading === 'loading' && <CircularProgress size={20} />}
                {error && <Typography color="error">{error}</Typography>} 
                <Typography variant="h6">What do you want to learn about?</Typography>
                {filteredSubCategories.map((sub) => (
                    <Button
                        key={sub.subCategoryId}
                        variant="outlined"
                        onClick={() => setSelectedSubCategory(sub)} // עדכון מצב של תת קטגוריה
                        style={{ margin: '8px' }}
                    >
                        {sub.name}
                    </Button>
                ))}
            </div>

            {selectedSubCategory && (
                <ChatComponent user={user} course={corse} subCategory={selectedSubCategory} />
            )}
        </div>
    );
};

export default CoursePage;
