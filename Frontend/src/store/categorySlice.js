import { createSlice } from '@reduxjs/toolkit';
import { fetchCategoryByID } from './thunk';


const categorySlice = createSlice({
  name: 'category',
  initialState: {
    category: [],
    loading: false,
    error: null,
  },
  reducers: {
    clearCategory(state) {
      state.category = [];
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCategoryByID.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCategoryByID.fulfilled, (state, action) => {
        state.loading = false;
        state.category = action.payload; // עדכון הקטגוריות עם התגובה
      })
      .addCase(fetchCategoryByID.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload; // עדכון השגיאה
      });
  },
});

export const { clearCategory } = categorySlice.actions;

export default categorySlice.reducer;
