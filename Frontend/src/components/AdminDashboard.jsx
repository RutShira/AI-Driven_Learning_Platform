import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
 // ודא שהנתיב נכון
import {
  Box,
  Typography,
  Card,
  CardContent,
  List,
  ListItem,
  CircularProgress,
  Alert,
  Divider,
} from '@mui/material';
import { fetchPrompts, fetchUsers } from '../store/thunk';

const AdminDashboard = () => {
  const dispatch = useDispatch();

  const { users, status: usersLoading, error: usersError } = useSelector((state) => state.users);
  const { prompts, status: promptsLoading, error: promptsError } = useSelector((state) => state.prompts);

  useEffect(() => {
    dispatch(fetchUsers());
    dispatch(fetchPrompts());
  }, [dispatch]);

  const getUserPrompts = (userId) => {
    return prompts?.filter((p) => p.userId === userId) || [];
  };

  return (
    <Box sx={{ padding: 4 }}>
      <Typography variant="h4" gutterBottom>לוח בקרה למנהל</Typography>

      {(usersLoading || promptsLoading) && (
        <Box sx={{ display: 'flex', justifyContent: 'center', mt: 2 }}>
          <CircularProgress />
        </Box>
      )}

      {(usersError || promptsError) && (
        <Alert severity="error" sx={{ my: 2 }}>
          {usersError || promptsError}
        </Alert>
      )}

      {users && users.length > 0 ? (
        users.map((user) => (
          <Card key={user.id} sx={{ mb: 3 }}>
            <CardContent>
              <Typography variant="h6">
                {user.name} ({user.phone})
              </Typography>
              <Divider sx={{ my: 1 }} />
              <Typography variant="subtitle1" gutterBottom>
                פניות:
              </Typography>
              <List dense>
                {getUserPrompts(user.id).map((prompt, idx) => (
                  <ListItem key={idx}>
                    <Typography variant="body2">
                      {prompt.prompt1 || prompt.content}
                    </Typography>
                  </ListItem>
                ))}
              </List>
            </CardContent>
          </Card>
        ))
      ) : (
        !usersLoading && <Typography>לא נמצאו משתמשים</Typography>
      )}
    </Box>
  );
};

export default AdminDashboard;
