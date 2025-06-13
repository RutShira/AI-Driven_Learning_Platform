import React, { useState } from 'react';
import { TextField, Button, Card, CardContent, Typography, CircularProgress, Box } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { fetchUserByLogin } from '../store/thunk';
import { useNavigate } from 'react-router-dom';

const LoginForm = () => {
  const [credentials, setCredentials] = useState({ username: '', idNumber: '' });
  const [errors, setErrors] = useState({});
  const dispatch = useDispatch();
  const { status, error } = useSelector((state) => state.user);
  const navigate = useNavigate();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setCredentials((prev) => {
      const updated = { ...prev, [name]: value };
      const newErrors = { ...errors };
      if (name === "username") {
        if (!value || value.length < 2) newErrors.username = "יש להזין שם משתמש (לפחות 2 תווים)";
        else delete newErrors.username;
      }
      if (name === "idNumber") {
        if (!value || !/^\d{9}$/.test(value)) newErrors.idNumber = "יש להזין תעודת זהות תקינה (9 ספרות)";
        else delete newErrors.idNumber;
      }
      setErrors(newErrors);
      return updated;
    });
  };

  const validate = () => {
    const newErrors = {};
    if (!credentials.username || credentials.username.length < 2)
      newErrors.username = "יש להזין שם משתמש (לפחות 2 תווים)";
    if (!credentials.idNumber || !/^\d{9}$/.test(credentials.idNumber))
      newErrors.idNumber = "יש להזין תעודת זהות תקינה (9 ספרות)";
    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!validate()) return;

    try {
      const resultAction = await dispatch(fetchUserByLogin({
        password: Number(credentials.idNumber),
        username: credentials.username
      }));

      if (fetchUserByLogin.fulfilled.match(resultAction)) {
        navigate('/dashboard');
      } else {
        throw new Error(resultAction.payload || 'שגיאת התחברות');
      }
    } catch (err) {
      alert(err.message || 'שגיאה לא צפויה');
    }
  };

  return (
    <Box
      sx={{
        minHeight: '100vh',
        bgcolor: '#f5f7fa',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
      }}
    >
      <Card
        variant="outlined"
        sx={{
          maxWidth: 400,
          width: '100%',
          boxShadow: 3,
          borderRadius: 2,
          p: 2,
          bgcolor: '#fff',
        }}
      >
        <CardContent>
          <Typography variant="h5" gutterBottom align="center">
            Login
          </Typography>
          <form onSubmit={handleSubmit}>
            <TextField
              label="Name"
              name="username"
              value={credentials.username}
              onChange={handleChange}
              fullWidth
              margin="normal"
              required
              error={Boolean(errors.username)}
              helperText={errors.username}
            />
            <TextField
              label="ID"
              name="idNumber"
              value={credentials.idNumber}
              onChange={handleChange}
              fullWidth
              margin="normal"
              required
              error={Boolean(errors.idNumber)}
              helperText={errors.idNumber}
            />
            {error && (
              <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2, mb: 1 }}>
                <Typography color="error" align="center">
                  {error}
                </Typography>
              </Box>
            )}
            <Button
              type="submit"
              variant="contained"
              color="primary"
              fullWidth
              disabled={status === 'loading'}
              sx={{ mt: 2 }}
            >
              {status === 'loading' ? <CircularProgress size={24} /> : 'Login'}
            </Button>
          </form>
        </CardContent>
      </Card>
    </Box>
  );
};

export default LoginForm;