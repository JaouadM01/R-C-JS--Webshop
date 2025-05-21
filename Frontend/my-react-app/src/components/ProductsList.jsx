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

  const [sortOption, setSortOption] = useState("default");

  const sortedProducts = [...products].sort((a, b) => {
    if (sortOption === "priceAsc") return a.price - b.price;
    if (sortOption === "priceDesc") return b.price - a.price;
    if (sortOption === "rarity") return a.rarity - b.rarity; // assuming rarity: 0 = Rare, 1 = Uncommon, 2 = Common
    return 0; // default order
  });  


  const RarityTypes = {
    0: "Rare",
    1: "Uncommon",
    2: "Common"
  }

  useEffect(() => {
    const getProducts = async () => {
      try {
        const response = await fetch(`http://localhost:5224/api/Products/listed`);
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
    <div className="productcollection-list-container">
      <h2>Products</h2>
      <div>
        <button
          className="add-product-button"
          onClick={() => navigate("/createproduct")}
        >
          + Add New Product
        </button>

        <select value={sortOption} onChange={(e) => setSortOption(e.target.value)} className="sort-dropdown">
          <option value="default">Sort by: Default</option>
          <option value="priceAsc">Price ↑</option>
          <option value="priceDesc">Price ↓</option>
          <option value="rarity">Rarity</option>
        </select>

      </div>
      <div className="product-grid">
        {sortedProducts.map((product) => (
          <div key={product.id} className="product-card">
            {isAuthenticated ? <MdFavorite className="fav-icon" size={50} onClick={() => toggleFavourite(product.id, userProfile.id)} /> : <MdFavorite className="fav-icon" size={50} onClick={() => alert("Please login first")} />}
            <img
              onClick={() => navigate(`/productdetails/${product.id}`)}
              alt={product.name || "Product Image"}
              src={product.image || "placeholder-image-url.jpg"} // Use product.image if available, else placeholder
              className="product-image"
            />
            <h3 className="product-name" onClick={() => navigate(`/productdetails/${product.id}`)}>{product.name || "Unnamed Product"}</h3>
            <h4 className="product-name" onClick={() => navigate(`/productdetails/${product.id}`)}>{RarityTypes[product.rarity] || "Rarity Unkown"}</h4>

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
