import { createContext, useContext, useState, useEffect } from 'react';

const AuthContext = createContext();

export function AuthProvider({ children }) {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [userProfile, setUserProfile] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem('authToken');
        console.log("tried to find token");
        if (token) {
            console.log("Token has been found");
            setIsAuthenticated(true);
            fetchUserProfile(token);
        }
    }, []);

    const fetchUserProfile = async (token) => {
        try {
            const response = await fetch('http://localhost:5224/api/User/Profile', {
                headers: { Authorization: `Bearer ${token}` },
            });
            if (response.ok) {
                const data = await response.json();
                setUserProfile(data);
                console.log("User profile data has been saved: ", data);
            }
        } catch (error) {
            console.error('Error fetching profile:', error);
        }
    };

    const logout = () => {
        localStorage.removeItem('authToken');
        setIsAuthenticated(false);
        setUserProfile(null);
        window.location.href = '/login';
    };

    const login = (token) => {
        localStorage.setItem('authToken', token);
        setIsAuthenticated(true);
        fetchUserProfile(token);
      };

    return (
        <AuthContext.Provider value={{ isAuthenticated, userProfile, logout , login}}>
            {children}
        </AuthContext.Provider>
    );
}

export function useAuth() {
    return useContext(AuthContext);
}
