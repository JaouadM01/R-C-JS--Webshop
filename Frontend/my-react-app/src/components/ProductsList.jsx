import React, { useEffect, useState } from "react";
import PropTypes from 'prop-types';
import './ProductsList.css'; // Import the custom CSS
import { useNavigate } from "react-router-dom";
import { MdFavorite, MdFavoriteBorder } from "react-icons/md";
import { useAuth } from "./context/AuthProvider";


const ProductsList = () => {
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  const { isAuthenticated, userProfile } = useAuth();

  useEffect(() => {
    const getProducts = async () => {
      try {
        const response = await fetch(`http://localhost:5224/api/Products`);
        if (response.ok) {
          const data = await response.json();
          setProducts(data);
        } else {
          console.error("Failed to fetch products");
        }
      }
      catch (error) {
        console.error('Error fetching products: ', error);
      }
    };

    getProducts();
  }, []);

  const toggleFavourite = async (productId, userId) => {
    try {
      // Send request to toggle the favourite status (Add or Remove)
      await fetch(`http://localhost:5224/api/User/Favourite?productId=${productId}&userId=${userId}`, {
        method: 'PUT', // Assuming PUT is used for toggling
      });
      alert("Added/Removed from favourites");
    } catch (error) {
      console.error('Error with toggling favourites:', error);
    }
  };

  return (
    <div className="product-list-container">
      <h2 className="sr-only">Products</h2>

      <div className="product-grid">
        {products.map((product) => (
          <div key={product.id} className="product-card">
            {isAuthenticated ? <MdFavorite className="fav-icon" size={50} onClick={() => toggleFavourite(product.id, userProfile.id)}/> : <MdFavorite className="fav-icon" size={50} onClick={() => alert("Please login first")}/>}
            <img
              onClick={() => navigate(`/productdetails/${product.id}`)}
              alt={product.name || "Product Image"}
              src={product.image || "placeholder-image-url.jpg"} // Use product.image if available, else placeholder
              className="product-image"
            />
            <h3 className="product-name" onClick={() => navigate(`/productdetails/${product.id}`)}>{product.name || "Unnamed Product"}</h3>

            {/* Check if description exists and display it */}
            <div className="product-description" onClick={() => navigate(`/productdetails/${product.id}`)}>
              {product.description ? (
                product.description.split('\n').map((desc, index) => (
                  <p key={index} className="description-item">{desc}</p>
                ))
              ) : (
                <p className="description-item">No description available.</p>
              )}
            </div>
            {product.description.split("/n").map((index, desc) => (
              <p key={index}>{desc}</p>
            ))}
            {/* Display price */}
            <div className="product-price" onClick={() => navigate(`/productdetails/${product.id}`)}>
              {product.price ? `${product.price.toFixed(2)} Satoshi` : "Price not available"}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProductsList;
