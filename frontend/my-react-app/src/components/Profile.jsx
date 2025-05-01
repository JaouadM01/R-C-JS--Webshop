import React from 'react';
import useAuth from './hooks/useAuth'; // Adjust the path if necessary

function Profile() {
    const { isAuthenticated, userProfile, loading, error, logout } = useAuth();

    if (loading) {
        return <div>Loading...</div>;
    }

    if (!isAuthenticated) {
        return <div>Please log in to view your profile. </div>;
    }

    if (error) {
        return <div style={{ color: 'red' }}>{error}</div>;
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
