import { useState, useEffect } from 'react';

function useAuth() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [userProfile, setUserProfile] = useState(null);

  useEffect(() => {
    // Check if there's a token in localStorage
    const token = localStorage.getItem('authToken');
    
    if (token) {
      setIsAuthenticated(true);
      // You can also fetch the user profile from the backend here
      fetchUserProfile(token);
    } else {
        setIsAuthenticated(false);
    }
  }, []);

  const logout = () => {
    // Clear the token from localStorage
    localStorage.removeItem('authToken');
  
    // Update the authentication state
    setIsAuthenticated(false);
    setUserProfile(null); // Clear the user profile
  
    // Redirect the user to the login page
    window.location.href = '/login'; // Or use navigate('/login') if you're using React Router v6
  };
  

  // Function to fetch user profile based on the token
  const fetchUserProfile = async (token) => {
    try {
      const response = await fetch('http://localhost:5224/api/User/Profile', {
        method: 'GET',
        headers: {
          'Authorization': `Bearer ${token}`, // Sending token for authenticated requests
        }
      });
      
      if (response.ok) {
        const data = await response.json();
        setUserProfile(data);
      }
    } catch (error) {
      console.error('Error fetching user profile:', error);
    }
  };

  return { isAuthenticated, userProfile, logout };
}

export default useAuth;
