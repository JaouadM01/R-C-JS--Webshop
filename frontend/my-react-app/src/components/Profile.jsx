import React, { useState, useEffect } from 'react';
import { useAuth } from './context/AuthProvider';
import { useNavigate } from 'react-router-dom';
import './Profile.css';

function Profile() {
    const { isAuthenticated, userProfile, logout } = useAuth();
    const navigate = useNavigate();

    const [favourites, setFavourites] = useState([]);
    const [products, setProducts] = useState([]);

    const [ownedProducts, setOwnedProducts] = useState([]);
    const [history, setHistory] = useState([]);

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
            } catch (error) {
                console.error('Error fetching products: ', error);
            }
        };

        if (favourites.length > 0) {
            getProducts();
        }
    }, [favourites]);

    const toggleFavourite = async (productId, userId) => {
        try {
            // Send request to toggle the favourite status (Add or Remove)
            const response = await fetch(`http://localhost:5224/api/User/Favourite?productId=${productId}&userId=${userId}`, {
                method: 'PUT', // Assuming PUT is used for toggling
            });

            if (response.ok) {
                // If toggle is successful, fetch updated favourites list
                const updatedFavouritesResponse = await fetch(`http://localhost:5224/api/User/getfavourites?id=${userId}`);
                if (updatedFavouritesResponse.ok) {
                    const updatedData = await updatedFavouritesResponse.json();
                    setFavourites(updatedData); // Update favourites state with the new list
                }
            } else {
                console.error('Failed to toggle favourite');
            }
        } catch (error) {
            console.error('Error with toggling favourites:', error);
        }
    };

    useEffect(() => {
        const getMyProducts = async () => {
            try {
                const response = await fetch(`http://localhost:5224/api/Products/GetProductList?id=${userProfile?.id}`);
                if (response.ok) {
                    const data = await response.json();
                    setOwnedProducts(data);
                }
            }
            catch (error) {
                console.error('Error with retrieving my products:', error);
            }
        }

        if (isAuthenticated && userProfile?.id) {
            getMyProducts();
        }
    }, [isAuthenticated, userProfile?.id]);

    useEffect(() => {
        const getMyHistory = async () => {
            try {
                const response = await fetch(`http://localhost:5224/api/PurchaseHistory/getbasedonuserid?id=${userProfile?.id}`);
                if (response.ok) {
                    const data = await response.json();
                    setHistory(data);
                }
            }
            catch (error) {
                console.error('Error with retrieving my products:', error);
            }
        }

        if (isAuthenticated && userProfile?.id) {
            getMyHistory();
        }
    }, [isAuthenticated, userProfile?.id]);


    return (
        <div className="profile-container">
            <section className="profile-header">
                <h2 className="profile-title">My Profile</h2>
                <p className="profile-info"><strong>Name:</strong> {userProfile?.name || 'N/A'}</p>
                <p className="profile-info"><strong>Email:</strong> {userProfile?.email || 'N/A'}</p>
                <p className="profile-info"><strong>Role:</strong> {userProfile?.role || 'N/A'}</p>
                <p className="profile-info"><strong>ID:</strong> {userProfile?.id || 'N/A'}</p>

                {isAuthenticated && (
                    <button className="logout-button" onClick={logout}>Logout</button>
                )}
            </section>

            <section className="profile-favourites">
                <h3 className="favourites-title">My Favourite Products</h3>
                {products && products.length > 0 ? (
                    <div className="products-list">
                        {products.map((product, index) => (
                            <div className="product-card" key={index}>
                                <h4>{product.name || 'Unnamed Product'}</h4>
                                <p><strong>Type:</strong> {product.type || 'Unknown'}</p>
                                <p><strong>Description:</strong> {product.description || 'No description available'}</p>
                                <p><strong>Price:</strong> ${product.price || 'N/A'}</p>
                                <button
                                    onClick={() => toggleFavourite(product.id, userProfile?.id)}
                                    className="favourite-button"
                                >
                                    {favourites.includes(product.id) ? 'Remove from Favourites' : 'Add to Favourites'}
                                </button>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p>No favourite products found.</p>
                )}
            </section>

            <section className="my-products-section">
                <h3 className="my-products-title">My Products</h3>
                {ownedProducts && ownedProducts.length > 0 ? (
                    <div className="product-list-container">
                        {ownedProducts.map((prd, index) => (
                            <div key={index} className="product-item">
                                {/* Display product name and other relevant details */}
                                <h4><span className="label">Name:</span> {prd.name}</h4>
                                <p><span className="label">Description:</span></p>
                                {prd.description.split('\n').map((line, index) => (
                                    <p key={index}>{line}</p>
                                ))}
                                <p><span className="label">Type:</span> {prd.type}</p>
                                <p><span className="history-item-price">Price:</span> {prd.price ? `${prd.price.toFixed(2)} Satoshi` : "Price not available"}</p>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p>No products purchased</p>
                )}
            </section>

            <section className="purchase-history-section">
                <h3 className="purchase-history-title">My Purchase History</h3>
                {history && history.length > 0 ? (
                    <div className="purchase-history-list">
                        {history.map((hsry, index) => (
                            <div key={index} className="history-item">
                                <h4 className="history-item-title">Product: {hsry.productId}</h4>
                                <p className="history-item-details">Purchased on: {new Date(hsry.changedDate).toLocaleDateString()}</p>
                                <p className="history-item-details">Previous Owner: {hsry.previousOwner || 'Unknown'}</p>
                                <p className="history-item-details">New Owner: {hsry.newOwner || 'Unknown'}</p>
                            </div>
                        ))}
                    </div>
                ) : (
                    <p className="no-history-message">No Purchase History found</p>
                )}
            </section>


        </div>
    );
}

export default Profile;
