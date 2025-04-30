import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import PRODUCTS from "../assets/lists/PRODUCTS";  // Assuming you have a list of products
import './ProductDetails.css'

export default function ProductDetails() {
  const { id } = useParams();  // Get the product ID from the URL
  const [productDetails, setProductDetails] = useState(null);

  // Fetch the product details based on the ID in the URL
  useEffect(() => {
    const fetchDetails = async () => {
      try {
        if (id) {
          const fetchedProduct = PRODUCTS.find((item) => item.id.toString() === id);
          setProductDetails(fetchedProduct);
        }
      } catch (error) {
        console.log("Error fetching product details: " + error);
      }
    };
    fetchDetails();
  }, [id]);

  return (
    <div className="product-details-container">
      <div className="product-details-wrapper">
        {productDetails ? (
          <div className="product-details">
            {/* Left: Product Image */}
            <div className="product-image-container">
              <img
                src={productDetails.image.src}
                alt={productDetails.image.alt}
                className="product-image"
              />
            </div>

            {/* Right: Product Info */}
            <div className="product-info-container">
              <h2 className="product-name">{productDetails.name}</h2>
              <p className="product-description">
                {productDetails.description.join(", ")}
              </p>

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
