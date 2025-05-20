import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../context/AuthProvider';
import './Register.css';

function Register() {
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    name: '',
    email: '',
    password: '',
    role: 'Customer'
  });

  const [error, setError] = useState('');

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    try {
      const response = await fetch('http://localhost:5224/api/User', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(form)
      });

      if (response.ok) {
        alert("Account created successfully!");
        navigate('/login');
      } else {
        const msg = await response.text();
        setError(`Failed to register: ${msg}`);
      }
    } catch (err) {
      setError(`An error occurred: ${err.message}`);
    }
  };

  if (isAuthenticated) {
    return <div><h2>You are already logged in</h2></div>;
  }

  return (
    <div className="register-container">
      <h2>Register</h2>
      {error && <p className="error-msg">{error}</p>}
      <form onSubmit={handleSubmit} className="register-form">
        <input type="text" name="name" placeholder="Name" value={form.name} onChange={handleChange} required />
        <input type="email" name="email" placeholder="Email" value={form.email} onChange={handleChange} required />
        <input type="password" name="password" placeholder="Password" value={form.password} onChange={handleChange} required />
        
        <select name="role" value={form.role} onChange={handleChange}>
          <option value="Customer">Customer</option>
          <option value="ProductSeller">ProductSeller</option>
        </select>
        
        <button type="submit">Create Account</button>
      </form>
    </div>
  );
}

export default Register;
