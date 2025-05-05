import React, { useEffect, useState } from "react";
import PropTypes from 'prop-types';
import './ProductsList.css'; // Import the custom CSS
import { useNavigate } from "react-router-dom";

const ProductsList = () => {
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);

  useEffect(() => {
    const getProducts = async () => {
      try {
        const response = await fetch(`http://localhost:5224/api/Products`);
        if(response.ok){
          const data = await response.json();
          setProducts(data);
        } else {
          console.error("Failed to fetch products");
        }
      }
      catch(error){
        console.error('Error fetching products: ', error);
      }
    };

    getProducts();
  }, []);

  return (
    <div className="product-list-container">
      <h2 className="sr-only">Products</h2>

      <div className="product-grid">
        {products.map((product) => (
          <div key={product.id} className="product-card" onClick={() => navigate(`/productdetails/${product.id}`)}>
            <img
              alt={product.name || "Product Image"}
              src={product.image || "placeholder-image-url.jpg"} // Use product.image if available, else placeholder
              className="product-image"
            />
            <h3 className="product-name">{product.name || "Unnamed Product"}</h3>

            {/* Check if description exists and display it */}
            <div className="product-description">
              {product.description ? (
                product.description.split('\n').map((desc, index) => (
                  <p key={index} className="description-item">{desc}</p>
                ))
              ) : (
                <p className="description-item">No description available.</p>
              )}
            </div>

            {/* Display price */}
            <div className="product-price">
              {product.price ? `${product.price.toFixed(2)} Satoshi` : "Price not available"}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProductsList;
