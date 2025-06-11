import React from 'react'
import { List, ListItemIcon, ListItemText, ListItemButton } from '@mui/material'
import HomeIcon from '@mui/icons-material/Home'
import CourseIcon from '@mui/icons-material/School'
import ProfileIcon from '@mui/icons-material/Person'
import { Link } from 'react-router-dom'

const Sidebar = () => {
  return (
    <div style={{ width: '250px', backgroundColor: '#f4f4f4', padding: '20px' }}>
      <List>
        <ListItemButton component={Link} to="/">
          <ListItemIcon>
            <HomeIcon />
          </ListItemIcon>
          <ListItemText primary="Home" />
        </ListItemButton>
        <ListItemButton component={Link} to="/courses">
          <ListItemIcon>
            <CourseIcon />
          </ListItemIcon>
          <ListItemText primary="Courses" />
        </ListItemButton>
        <ListItemButton component={Link} to="/profile">
          <ListItemIcon>
            <ProfileIcon />
          </ListItemIcon>
          <ListItemText primary="Profile" />
        </ListItemButton>
      </List>
    </div>
  )
}

export default Sidebar