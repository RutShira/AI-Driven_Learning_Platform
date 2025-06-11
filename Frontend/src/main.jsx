import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App';
import { Provider } from 'react-redux';
import {store} from './store/store';
import { ThemeProvider } from '@mui/material/styles';
import muiTheme from './theme/muiTheme';
import './styles/global.css';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Provider store={store}>
    <ThemeProvider theme={muiTheme}>
      
        <App />
    
    </ThemeProvider>
  </Provider>
);