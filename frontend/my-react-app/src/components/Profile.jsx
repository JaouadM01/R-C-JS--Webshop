import React from 'react';
import { useAuth } from './context/AuthProvider';
import { useNavigate } from 'react-router-dom';

function Profile() {
    const { isAuthenticated, userProfile, logout } = useAuth();
    const navigate = useNavigate();

    if (!isAuthenticated) {
        return <div>
            <div>
            Please log in to view your profile.
            </div> 
                <button onClick={() => navigate("/login")}>Click here to go to login page</button>
            </div>;
    }

    return (
        <div>
            <h2>Profile</h2>
            <p>Name: {userProfile?.name || 'N/A'}</p>
            <p>Email: {userProfile?.email || 'N/A'}</p>
            <p>Role: {userProfile?.role || 'N/A'}</p>
            {isAuthenticated && (
                <li><button onClick={logout}>Logout</button></li>
            )}
        </div>
    );
}

export default Profile;
