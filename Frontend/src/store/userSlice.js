import { createSlice } from '@reduxjs/toolkit';
import { fetchUserByLogin } from './thunk';

const userSlice = createSlice({
  name: 'user',
  initialState: {
    user: null,
    // token: null,
    status: 'idle',
    error: null,
  },
  reducers: {
    logout: (state) => {
      state.user = null;
    //   state.token = null;
      state.status = 'idle';
      state.error = null;
    //   localStorage.removeItem('token');
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchUserByLogin.pending, (state) => {
        state.status = 'loading';
        state.error = null;
      })
      .addCase(fetchUserByLogin.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.user = action.payload; // אם ה-thunk מחזיר רק את המשתמש
        // אם ה-thunk מחזיר {user, token} אז:
        // state.user = action.payload.user;
        // state.token = action.payload.token;
      })
      .addCase(fetchUserByLogin.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.payload;
      });
  },
});

export const { logout } = userSlice.actions;
export default userSlice.reducer;