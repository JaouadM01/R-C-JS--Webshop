import React, { useState } from 'react';
import './Login.css'; // Make sure the path is correct
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthProvider';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const {login} = useAuth();
  
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
      
      console.log("LoginDto:", loginDto);
      console.log("Response status:", response.status);

      if (response.ok) {
        const data = await response.json();
        console.log("Response data: ", data);
        login(data.token);
        if (data.token) {
          console.log("User has logged in");
          if (data.token) {
            navigate('/profile'); // Redirect to another page
          } else {
            setError('Login failed: Invalid token');
          }
        } else {
          setError('Login failed: Token not received');
        }
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

      <div>
        <button
          onClick={() => {
            try {
              navigate("/register");
            } catch (error) {
              console.error("Navigation error:", error);
              setError("Failed to navigate to the register page.");
            }
          }}
        >Register</button>
      </div>

      {error && <div className="error-message" style={{ color: 'red' }}>{error}</div>}
    </div>
  );
}

export default Login;
