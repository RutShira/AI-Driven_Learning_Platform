import React, { useState, useEffect } from 'react';
import { TextField, Button, Typography, Paper } from '@mui/material';
import { useDispatch } from 'react-redux';
import { createPrompt } from '../store/thunk';


const ChatComponent = ({ user, course, subCategory }) => {
  const [message, setMessage] = useState('');
  const [messages, setMessages] = useState([]);
  const dispatch = useDispatch();




const breakTextIntoLines = (text, wordsPerLine) => {
  const words = text.split(' ');
  const lines = [];
  
  for (let i = 0; i < words.length; i += wordsPerLine) {
    lines.push(words.slice(i, i + wordsPerLine).join(' '));
  }

  return lines.join('\n'); // מחזיר את הטקסט עם שורות חדשות
};

const handleSendMessage = async () => {
  if (message.trim()) {
    setMessages([...messages, { sender: 'user', text: message }]);
    setMessage('');

    try {
      const response = await dispatch(createPrompt({
        UserId: 23849987,
        CategoryId: course.categoryId,
        SubCategoryId: subCategory.subCategoryId,
        Prompt1: message
      }));

      console.log('creating prompt:', response);

      if (createPrompt.fulfilled.match(response)) {
        const formattedResponse = breakTextIntoLines(response.payload, 5); // 5 מילים בכל שורה
        setMessages(prevMessages => [...prevMessages, { sender: 'bot', text: formattedResponse }]);
      } else {
        const errorMessage = response.payload?.details;
        console.error('Error creating prompt:', errorMessage);
        
        setMessages(prevMessages => [...prevMessages, { sender: 'bot', text: `שגיאה: ${errorMessage}` }]);
      }
    } catch (error) {
      console.error('Unexpected error creating prompt:', error);
      
      setMessages(prevMessages => [...prevMessages, { sender: 'bot', text: 'שגיאה בלתי צפויה קרתה. אנא נסה שוב מאוחר יותר.' }]);
    }
  }
};

  useEffect(() => {
    // כאן תוכל להוסיף לוגיקה שתרצה להריץ כאשר messages משתנה
    console.log('Messages have changed:', messages);
  }, [messages]); // התלות היא messages

  return (
    <Paper style={{ padding: '16px', marginTop: '16px' }}>
      <Typography variant="h6">Chat about {subCategory.name}</Typography>
      <div style={{ maxHeight: '300px', overflowY: 'auto', marginBottom: '16px' }}>
        {messages.map((msg, index) => (
          <div key={index} style={{ textAlign: msg.sender === 'user' ? 'right' : 'left' }}>
  <Typography 
  variant="body1" 
  style={{ 
    background: msg.sender === 'user' ? '#d1e7dd' : '#f8d7da', 
    padding: '8px', 
    borderRadius: '4px', 
    display: 'inline-block' 
  }} 
  whiteSpace="pre-line" // חשוב
>
  {msg.text}
</Typography>
          </div>
        ))}
      </div>
      <TextField
        variant="outlined"
        fullWidth
        value={message}
        onChange={(e) => setMessage(e.target.value)}
        placeholder="כתוב הודעה..."
      />
      <Button variant="contained" color="primary" onClick={handleSendMessage} style={{ marginTop: '8px' }}>
        שלח
      </Button>
    </Paper>
  );

}
export default ChatComponent;

