import { createSlice } from "@reduxjs/toolkit";
import { fetchPrompts } from "./thunk";

const promptsSlice = createSlice({
  name: 'prompts',
  initialState: { data: [], status: 'idle', error: null },
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchPrompts.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchPrompts.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.data = action.payload;
      })
      .addCase(fetchPrompts.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message; // שגיאה מהשרת
      });
  },
});

export default promptsSlice.reducer;