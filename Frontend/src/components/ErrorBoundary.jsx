import React from 'react';

import  { useState, useEffect } from 'react';

class ErrorBoundary extends React.Component {
constructor(props) {
super(props);
this.state = { hasError: false, error: null };
}

static getDerivedStateFromError(error) {
return { hasError: true, error };
}

componentDidCatch(error, errorInfo) {
// אפשר לשלוח לשרת לוגים כאן
console.error("Caught by ErrorBoundary:", error, errorInfo);
}

render() {
if (this.state.hasError) {
return (
<div style={{ padding: 32, textAlign: 'center', color: 'red' }}>
<h2>אירעה שגיאה באפליקציה</h2>
<pre>{this.state.error?.message}</pre>
</div>
);
}
return this.props.children;
}
}

export default ErrorBoundary;