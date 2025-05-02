import React, { useState, useEffect } from 'react';
import { useAuth } from './context/AuthProvider';
import { useNavigate } from 'react-router-dom';

function Profile() {
    const { isAuthenticated, userProfile, logout } = useAuth();
    const navigate = useNavigate();

    const [favourites, setFavourites] = useState([]);
    const [products, setProducts] = useState([]);

    // Redirect to login if not authenticated
    useEffect(() => {
        if (!isAuthenticated) {
            navigate('/login');
        }
    }, [isAuthenticated, navigate]);

    // Fetch favourites (only once when userProfile changes)
    useEffect(() => {
        const getFavourites = async () => {
            try {
                const response = await fetch(`http://localhost:5224/api/User/getfavourites?id=${userProfile?.id}`);
                if (response.ok) {
                    const data = await response.json();
                    setFavourites(data);
                    console.log('Favourite list has been saved: ', data);
                }
            } catch (error) {
                console.error('Error fetching favourites:', error);
            }
        };

        if (isAuthenticated && userProfile?.id) {
            getFavourites();
        }
    }, [isAuthenticated, userProfile?.id]);

    // Fetch products for favourites (only once when favourites change)
    useEffect(() => {
        const getProducts = async () => {
            try {
                const productData = await Promise.all(
                    favourites.map(async (productId) => {
                        const response = await fetch(`http://localhost:5224/api/Products/${productId}`);
                        if (response.ok) {
                            return await response.json();
                        }
                        return null;
                    })
                );
                const uniqueProducts = productData.filter((product, index, self) =>
                    product && self.findIndex((p) => p?.id === product?.id) === index
                );
                setProducts(uniqueProducts);
                console.log('Products have been added to the list: ', uniqueProducts);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };

        if (favourites.length > 0) {
            getProducts();
        }
    }, [favourites]);

    // Render the profile
    return (
        <div>
            <h2>Profile</h2>
            <p>Name: {userProfile?.name || 'N/A'}</p>
            <p>Email: {userProfile?.email || 'N/A'}</p>
            <p>Role: {userProfile?.role || 'N/A'}</p>
            <p>Id: {userProfile?.id || 'N/A'}</p>
            {isAuthenticated && (
                <li>
                    <button onClick={logout}>Logout</button>
                </li>
            )}
            {products && products.length > 0 ? (
                products.map((product, index) => (
                    <ul key={index}>
                        <li>{product.name || 'Unnamed Product'}</li>
                        <li>{product.type || 'Unnamed Product'}</li>
                        <li>{product.description || 'Unnamed Product'}</li>
                        <li>{product.price || 'Unnamed Product'}</li>
                    </ul>
                ))
            ) : (
                <p>No products found.</p>
            )}
        </div>
    );
}

export default Profile;