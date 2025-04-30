import React, { useState } from 'react';
import './Login.css'; // Make sure the path is correct

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  
  const handleSubmit = async (event) => {
    event.preventDefault();

    const loginDto = { email, password };

    try {
      const response = await fetch('http://localhost:5224/api/User/Login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginDto),
      });

      if (response.ok) {
        const data = await response.json();
        // Store the token in localStorage or sessionStorage
        localStorage.setItem('authToken', data.Token);
        window.location.href = '/'; // Redirect to another page
      } else {
        const errorText = await response.text();
        setError(errorText);
      }
    } catch (error) {
      setError('Error: ' + error.message);
    }
  };

  return (
    <div className="login-container">
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label htmlFor="email">Email:</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>

        <div>
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>

        <button type="submit">Login</button>
      </form>

      {error && <div className="error-message" style={{ color: 'red' }}>{error}</div>}
    </div>
  );
}

export default Login;
