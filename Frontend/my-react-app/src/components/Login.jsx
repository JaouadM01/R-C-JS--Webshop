import React, { useState } from 'react';
import './Login.css'; // Make sure the path is correct

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();

    // For now, checking if credentials are correct
    if (email === 'test@example.com' && password === 'password123') {
      localStorage.setItem('token', 'fake-jwt-token'); // Simulating successful login
      window.location.href = '/dashboard'; // Redirect after successful login
    } else {
      setErrorMessage('Invalid credentials. Please try again.');
    }
  };

  return (
    <div className="login-container">
      <div className="login-box">
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
          <div className="input-container">
            <label htmlFor="email">Email</label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              placeholder="Enter your email"
            />
          </div>

          <div className="input-container">
            <label htmlFor="password">Password</label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              placeholder="Enter your password"
            />
          </div>

          {errorMessage && <p className="error-message">{errorMessage}</p>}

          <button type="submit" className="btn-login">Login</button>
        </form>

        <div className="footer-links">
          <a href="#" className="footer-nav-link">Forgot Password?</a>
        </div>
      </div>
    </div>
  );
};

export default Login;
