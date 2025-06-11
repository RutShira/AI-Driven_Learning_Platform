import { createAsyncThunk } from '@reduxjs/toolkit';

import { captureOwnerStack } from 'react';
import { getRequest ,postRequest} from '../api';

export const fetchCategories = createAsyncThunk('categories/fetchCategories', async (_, thunkAPI) => {
  try {
    return await getRequest('/Category');
  } catch (err) {
    return thunkAPI.rejectWithValue(err);
  }
});

export const fetchSubCategories = createAsyncThunk('subCategories/fetchSubCategories', async (_, thunkAPI) => {
  'subCategories/fetchByCategory'
  try {
    return await getRequest('/SubCategory');
  } catch (err) {
    return thunkAPI.rejectWithValue(err);
  }
});

export const fetchUsers = createAsyncThunk('user/fetchUsers', async (_, thunkAPI) => {
  try {
    return await getRequest('/users');
  } catch (err) {
    return thunkAPI.rejectWithValue(err);
  }
});

export const fetchPrompts = createAsyncThunk('prompts/fetchPrompts', async (_, thunkAPI) => {
  try {
    return await getRequest('/Prompt');
  } catch (err) {
    return thunkAPI.rejectWithValue(err);
  }
});

// export const loginUser = createAsyncThunk(
//   'user/loginUser',
//   async (userData, thunkAPI) => {
//     try {
//       const { Token } = await postRequest('/Auth/login', userData);
//       localStorage.setItem('token', Token);

//       const user = await getRequest('/users/me', {
//         headers: { Authorization: `Bearer ${Token}` }
//       });

//       return { user, token: Token };
//     } catch (err) {
//       return thunkAPI.rejectWithValue(err?.response?.data?.message || 'Login failed');
//     }
//   }
// );

export const fetchSubCategoriesByCategory = createAsyncThunk(
  'subCategories/fetchByCategory',
  async (categoryId, thunkAPI) => {
try {
  return await getRequest(`/SubCategory/category=${categoryId}`);
} catch (err) {
  return thunkAPI.rejectWithValue(err.message || 'Something went wrong');
}

  }
);
export const fetchUserByLogin = createAsyncThunk(
  'user/fetchUserByLogin',
  async ({ username, password }, { rejectWithValue }) => {
    try {
      const response = await postRequest('/User/login', loginModel);
      return response.data;
    } catch (err) {
      return rejectWithValue(err.response?.data?.message || 'שגיאת התחברות');
    }
  }
);
