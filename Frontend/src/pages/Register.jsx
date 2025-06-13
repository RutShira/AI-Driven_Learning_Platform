import { Container, Typography } from '@mui/material';
import UserProfile from '../components/UserProfile';
import LoginForm from '../components/LoginForm';
import RegisterFrom from '../components/RegisterFrom';

const Register = () => {
  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Register From
      </Typography>
      <RegisterFrom />
    </Container>
  );
};

export default Register;