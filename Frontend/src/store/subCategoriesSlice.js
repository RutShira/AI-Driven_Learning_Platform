import { createSlice } from '@reduxjs/toolkit';
import { fetchSubCategoriesByCategory } from './thunk';


const subCategoriesSlice = createSlice({
  name: 'subCategories',
  initialState: {
    items: [],
    loading: false,
    error: null,
  },
  reducers: {
    clearSubCategories: (state) => {
      state.items = [];
      state.error = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchSubCategoriesByCategory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSubCategoriesByCategory.fulfilled, (state, action) => {
        state.loading = false;
        state.items = action.payload; // Assuming payload contains the subcategories
      })
      .addCase(fetchSubCategoriesByCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload; // Handle the error appropriately
      });
  },
});

export const { clearSubCategories } = subCategoriesSlice.actions;

export default subCategoriesSlice.reducer;
