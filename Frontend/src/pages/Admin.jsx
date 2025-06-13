import { Container, Typography } from "@mui/material";
import AdminDashboard from "../components/AdminDashboard";

const Admin = () => {
  return (
    <Container>
      <Typography variant="h4" gutterBottom>
        Register From
      </Typography>
      <AdminDashboard />
    </Container>
  );
};

export default Admin;