import { createSlice } from '@reduxjs/toolkit';
import { fetchSubCategories } from './thunk';


const subCategoriesSlice = createSlice({
  name: 'subCategories',
  initialState: {
    items: [],
    loading: false,
    error: null,
  },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchSubCategories.pending, (state) => {
        
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSubCategories.fulfilled, (state, action) => {
        state.loading = false;
        state.items = action.payload; // Assuming payload contains the subcategories
      })
      .addCase(fetchSubCategories.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload; // Handle the error appropriately
      });
  },
});


export default subCategoriesSlice.reducer;
