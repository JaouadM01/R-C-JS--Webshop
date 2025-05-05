import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import './ProductDetails.css';

export default function ProductDetails() {
  const { id } = useParams();  // Get the product ID from the URL
  const [productDetails, setProductDetails] = useState(null);
  const navigate = useNavigate();

  // Fetch the product details based on the ID in the URL
  useEffect(() => {
    const fetchDetails = async () => {
      try {
        const response = await fetch(`http://localhost:5224/api/Products/${id}`);
        if (response.ok) {
          const data = await response.json();
          setProductDetails(data);
        }
      } catch (error) {
        console.log("Error fetching product details: " + error);
      }
    };

    fetchDetails();
  }, [id]);

  return (
    <div className="product-details-container">
      <button onClick={() => navigate('/productlist')} className="add-to-cart-button">Go Back</button>
      <div className="product-details-wrapper">
        {productDetails ? (
          <div className="product-details">
            {/* Left: Product Image */}
            <div className="product-image-container">
              <img
                src={productDetails.image}
                alt={productDetails.name}
                className="product-image"
              />
            </div>

            {/* Right: Product Info */}
            <div className="product-info-container">
              <h2 className="product-name">{productDetails.name}</h2>
              
              {/* Description */}
              <div className="product-description">
                {productDetails.description.split("\n").map((desc, index) => (
                  <p key={index} className="description-item">{desc}</p>
                ))}
              </div>

              {/* Product Price */}
              <div className="product-price">
                {productDetails.price ? `${productDetails.price.toFixed(2)} Satoshi` : "Price not available"}
              </div>

              {/* Add to Cart Button */}
              <button className="add-to-cart-button">Get it now!</button>
            </div>
          </div>
        ) : (
          <h3 className="error-message">Product Not Found</h3>
        )}
      </div>
    </div>
  );
}
