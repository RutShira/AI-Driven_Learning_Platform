import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { fetchCategories, fetchSubCategoriesByCategory } from './thunk';

const categoriesSlice = createSlice({
  name: 'categories',
  initialState: { 
    data: [], // מערך ריק של קטגוריות
    status: 'idle', 
    error: null 
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchCategories.pending, (state) => { 
        state.status = 'loading'; 
      })
      .addCase(fetchCategories.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.data = action.payload.map(category => ({
          ...category,
          subCategories: [] // הוסף מערך ריק של תתי קטגוריות לכל קטגוריה
        }));
      })
      .addCase(fetchCategories.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message;
      })
      .addCase(fetchSubCategoriesByCategory.pending, (state) => {
        state.status = 'loading'; // אם תרצה לעדכן את הסטטוס גם כאן
      })
      .addCase(fetchSubCategoriesByCategory.fulfilled, (state, action) => {
        state.status = 'succeeded';
        const categoryIndex = state.data.findIndex(category => category.id === action.meta.arg);
        if (categoryIndex !== -1) {
          state.data[categoryIndex].subCategories = action.payload; // עדכון תתי הקטגוריות
        }
      })
      .addCase(fetchSubCategoriesByCategory.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message;
      });
  },
});

// פונקציה נפרדת כדי לטעון את תתי הקטגוריות
export const loadSubCategories = (categoryId) => async (dispatch) => {
  await dispatch(fetchSubCategoriesByCategory(categoryId));
};

export default categoriesSlice.reducer;
