import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { fetchCategories,  } from './thunk';

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
        state.data = action.payload
      })
      .addCase(fetchCategories.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message;
      })
     
     
  },
});

export default categoriesSlice.reducer;
