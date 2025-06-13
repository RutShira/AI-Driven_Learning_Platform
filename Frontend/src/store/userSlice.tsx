// import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
// import { fetchUserByLogin } from './thunk';

// const userSlice = createSlice({
//   name: 'user',
//   initialState: {
//     user: null,
//     token: null,
//     status: 'idle',
//     error: null,
//   },
//   reducers: {
//     logout: (state) => {
//       state.user = null;
//       state.token = null;
//       state.status = 'idle';
//       state.error = null;
//       localStorage.removeItem('token'); // אם אתה שומר טוקן
//     },
//   },
//   extraReducers: (builder) => {
//     builder
//       .addCase(fetchUserByLogin.pending, (state) => {
//         state.status = 'loading';
//         state.error = null;
//       })
//       .addCase(fetchUserByLogin.fulfilled, (state, action) => {
//         state.status = 'succeeded';
//         state.user = action.payload.user; // הנחתי שה-thunk מחזיר אובייקט עם משתמש
//         state.token = action.payload.token; // הנחתי שה-thunk מחזיר גם טוקן
//         localStorage.setItem('token', action.payload.token); // שמירת הטוקן
//       })
//       .addCase(fetchUserByLogin.rejected, (state, action) => {
//         state.status = 'failed';
//         state.error = action.payload;
//       });
//   },
// });

// export const { logout } = userSlice.actions;
// export default userSlice.reducer;
// src/redux/slices/userSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface UserState {
  id: string | null;
  name: string | null;
  phone: string | null;
  isAdmin: boolean;
}

const initialState: UserState = {
  id: null,
  name: null,
  phone: null,
  isAdmin: false,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser(state, action: PayloadAction<UserState>) {
      state.id = action.payload.id;
      state.name = action.payload.name;
      state.phone = action.payload.phone;
      state.isAdmin = action.payload.isAdmin;
    },
    clearUser(state) {
      state.id = null;
      state.name = null;
      state.phone = null;
      state.isAdmin = false;
    },
  },
});

export const { setUser, clearUser } = userSlice.actions;

export default userSlice.reducer;
