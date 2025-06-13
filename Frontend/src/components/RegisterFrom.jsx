import React, { useState } from 'react';
import { TextField, Button, Typography } from '@mui/material';
import { useDispatch, useSelector } from 'react-redux';
import { createUser } from '../store/thunk';
import { setUser } from '../store/userSlice';

const RegisterForm = () => {
    const [formData, setFormData] = useState({
        Id: '',
        Name: '',
        Phone: '',
        Email: '',
        Role: ''
    });
    const [errors, setErrors] = useState({});
    const dispatch = useDispatch();
    const userCreate = useSelector((state) => state.user);
    const { error: submissionError } = userCreate;

    const validate = () => {
        const newErrors = {};
        if (!formData.Id) newErrors.id = 'ID is required';
        if (!formData.Name) newErrors.Name = 'Name is required';
        if (!formData.Phone.match(/^[0-9\b]+$/)) newErrors.Phone = 'Invalid phone number format';
        if (!formData.Email.match(/^\S+@\S+$/)) newErrors.Email = 'Invalid email address format';
        return newErrors;
    };

    const onSubmit = (e) => {
        e.preventDefault();
        const validationErrors = validate();
        if (Object.keys(validationErrors).length === 0) {
            dispatch(createUser(formData));
            dispatch(setUser(formData)); // Assuming setUser is an action to set the user in the store
            setFormData({
                Id: '',
                Name: '',
                Phone: '',
                Email: '',
                Role: ''
            });
        } else {
            setErrors(validationErrors);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
        setErrors({ ...errors, [name]: '' }); // Clear the error for the field being changed
    };

    return (
        <form onSubmit={onSubmit}>
            <Typography variant="h5">Register</Typography>
            {submissionError && <Typography color="error">{submissionError}</Typography>}
            <TextField
                label="Id"
                name="Id"
                value={formData.Id}
                onChange={handleChange}
                error={!!errors.Id}
                helperText={errors.Id}
                fullWidth
                margin="normal"
            />

            <TextField
                label="Name"
                name="Name"
                value={formData.Name}
                onChange={handleChange}
                error={!!errors.Name}
                helperText={errors.Name}
                fullWidth
                margin="normal"
            />

            <TextField
                label="Phone"
                name="Phone"
                value={formData.Phone}
                onChange={handleChange}
                error={!!errors.Phone}
                helperText={errors.Phone}
                fullWidth
                margin="normal"
            />

            <TextField
                label="Email"
                type="email"
                name="Email"
                value={formData.Email}
                onChange={handleChange}
                error={!!errors.Email}
                helperText={errors.Email}
                fullWidth
                margin="normal"
            />

        

            <Button type="submit" variant="contained" color="primary">
                Register
            </Button>
        </form>
    );
};

export default RegisterForm;
