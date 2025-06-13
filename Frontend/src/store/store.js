import { configureStore } from '@reduxjs/toolkit';
import { combineReducers } from 'redux';
import usersReducer from './usersSlice';
import categoriesReducer from './categoriesSlice';
import subCategoriesReducer from './subCategoriesSlice';
import userReducer from './userSlice';
import promptsReducer from './promptsSlice';
import categoryReducer from './categorySlice';
import { createStoreHook } from 'react-redux';




export const store = configureStore({
  reducer: {
    users: usersReducer,
    user: userReducer,
    categories: categoriesReducer,
    subCategories:subCategoriesReducer,
    prompts: promptsReducer,
    category:categoryReducer
  },
});
