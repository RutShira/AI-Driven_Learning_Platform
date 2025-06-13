import { createAsyncThunk } from '@reduxjs/toolkit';
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
    var res=await getRequest('/SubCategory');
    console.log('res', res);
    return res;

  } catch (err) {
    return thunkAPI.rejectWithValue(err);
  }
});

// export const fetchUsers = createAsyncThunk('user/fetchUsers', async (_, thunkAPI) => {
//   try {
//     return await getRequest('/users');
//   } catch (err) {
//     return thunkAPI.rejectWithValue(err);
//   }
// });



export const fetchUserByLogin = createAsyncThunk(
  'user/loginUser',
  async (userData, thunkAPI) => {
    try {
      const { Token } = await postRequest('/Auth/login', userData);
      localStorage.setItem('token', Token);

      const user = await getRequest('/Auth/me', {
        headers: { Authorization: `Bearer ${Token}` }
      });

      return { user, token: Token };
    } catch (err) {
      return thunkAPI.rejectWithValue(err?.response?.data?.message || 'Login failed');
    }
  }
);


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
// export const fetchUserByLogin = createAsyncThunk(
//   'user/fetchUserByLogin',
//   async (loginModel, { rejectWithValue }) => {
//     try {
//       const response = await postRequest('/User/login', loginModel);
//       return response.data;
//     } catch (err) {
//       return rejectWithValue(err.response?.data?.message || 'שגיאת התחברות');
//     }
//   }
// );
export const fetchCategoryByID = createAsyncThunk(
  'Categories/fetchByCategory',
  async (categoryId, thunkAPI) => {
try {
  return await getRequest(`/Category/${categoryId}`);
} catch (err) {
  return rejectWithValue(err.message || 'Something went wrong');
}

  }
);


export const createUser = (userData) => {
    return async (dispatch) => {
        try {
            const response = await postRequest('/User', userData);
            return response.data; // Assuming the API returns the created user data
            dispatch({ type: 'USER_CREATE_SUCCESS', payload: response.data });

            // Handle successful registration (e.g., redirect, show message)
        } catch (error) {
            dispatch({ type: 'USER_CREATE_FAIL', payload: error.response ? error.response.data.message : error.message });
        }
    };
};


// פונקציית THUNK לקבלת פרופס
// export const fetchPrompts = createAsyncThunk(
//   'prompts/fetchPrompts',
//   async (_, thunkAPI) => {
//     try {
//       const response = await getRequest('/Prompt');
//       return response; // אם getRequest כבר מחזיר את response.data
//     } catch (error) {
//       return thunkAPI.rejectWithValue(error.response?.data || 'Failed to fetch prompts');
//     }
//   }
// );


// פונקציית THUNK ליצירת פרומפס
export const createPrompt = createAsyncThunk(
  'prompts/createPrompt',
  async (newPrompt, thunkAPI) => {
   try{
      const response = await postRequest('/Prompt', newPrompt); // שנה ל-URL המתאים שלך 
      console.log('response', response);
      return response; // החזר את הפרומפט שנוצר
    } catch (error) {
      return thunkAPI.rejectWithValue(error.response.data || 'Failed to create prompt');
    }
  }
);


// helper to get token
const authHeader = () => {
  const token = localStorage.getItem('token');
  return { headers: { Authorization: `Bearer ${token}` } };
};

// ✅ מביא את כל המשתמשים עם token
export const fetchUsers = createAsyncThunk('user/fetchUsers', async (_, thunkAPI) => {
  try {
    return await getRequest('/User/admin/all', authHeader());
  } catch (err) {
    return thunkAPI.rejectWithValue(err?.message || 'Failed to fetch users');
  }
});

// ✅ מביא את כל הפרומפטים עם token
export const fetchPrompts = createAsyncThunk('prompts/fetchPrompts', async (_, thunkAPI) => {
  try {
    console.log('Auth header:', authHeader()); // ✅ בדיקה
    const response = await getRequest('/Prompt/admin/all', authHeader());
    return response;
  } catch (error) {
    return thunkAPI.rejectWithValue(error?.message || 'Failed to fetch prompts');
  }
});
// ✅ מביא פרומפט לפי ID עם token



