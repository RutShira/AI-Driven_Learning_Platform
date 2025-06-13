

import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';

import NavigationBar from './components/NavigationBar'; // עדכן את הייבוא
import Sidebar from './components/Sidebar';
import Home from './pages/Home';
import Courses from './pages/Courses';
import Lesson from './pages/Lesson';

import Dashboard from './components/Dashboard';

import LoginForm from './components/LoginForm';
import { ThemeProvider } from '@mui/material/styles';
import muiTheme from './theme/muiTheme';
import CoursePage from './components/CoursePage';
import Register from './pages/Register';
import Admin from './pages/Admin';



function App() {

  return (

      <ThemeProvider theme={muiTheme}>
        <Router>
          <NavigationBar /> {/* השתמש בשם החדש */}
          <Sidebar />
          <Routes>
            
            <Route path="/" element={<Home />} />
            <Route path="/login" element={<LoginForm />} />
            <Route path="/courses" element={<Courses />} />
          <Route path="/course/:courseId" element={<Lesson />} />
            <Route path="/dashboard" element={<Dashboard />} />
             <Route path="/signup" element={<Register />} />
             <Route path="/admin" element={<Admin />} />
          </Routes>
        </Router>
      </ThemeProvider>

    

  );
}




export default App;


