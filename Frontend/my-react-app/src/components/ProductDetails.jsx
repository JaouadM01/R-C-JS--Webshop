import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useAuth } from "./context/AuthProvider"; // Assuming you have this context
import './ProductDetails.css';
import { MdFavorite, MdFavoriteBorder } from "react-icons/md"; // MdFavorite for filled, MdFavoriteBorder for outline

export default function ProductDetails() {
  const { id } = useParams();  // Get the product ID from the URL
  const [productDetails, setProductDetails] = useState(null);
  const [isModalOpen, setIsModalOpen] = useState(false); // State for modal visibility
  const [isFavourite, setIsFavourite] = useState(false); // State to track if the product is in the favourite list
  const navigate = useNavigate();
  const { isAuthenticated, userProfile } = useAuth();

  const RarityTypes = {
    0 : "Rare",
    1 : "Uncommon",
    2 : "Common"
  }

  // Fetch the product details based on the ID in the URL
  useEffect(() => {
    const fetchDetails = async () => {
      try {
        const response = await fetch(`http://localhost:5224/api/Products/${id}`);
        if (response.ok) {
          const data = await response.json();
          setProductDetails(data);

          // Check if this product is in the user's favourite list
          if (userProfile?.favourites?.includes(id)) {
            setIsFavourite(true);
          }
        }
      } catch (error) {
        console.log("Error fetching product details: " + error);
      }
    };

    fetchDetails();
  }, [id, userProfile]);

  // Handle Purchase
  const Purchase = async () => {
    try {
      const response = await fetch(`http://localhost:5224/api/Products/updateOwner?id=${userProfile.id}&productId=${id}`, {
        method: 'PUT',  // Assuming PUT request for updating ownership
      });
      if (response.ok) {
        setIsModalOpen(false);  // Close modal after successful purchase
        alert("Purchase successful!");  // Notify user
      } else {
        alert("There was an error with the purchase. Please try again.");
      }
    } catch (error) {
      console.error('Error changing ownership: ', error);
      alert("Error making the purchase. Please try again later.");
    }
  }

  const toggleFavourite = async (productId, userId) => {
    try {
      // Send request to toggle the favourite status (Add or Remove)
      const response = await fetch(`http://localhost:5224/api/User/Favourite?productId=${productId}&userId=${userId}`, {
        method: 'PUT', // Assuming PUT is used for toggling
      });
      if (response.ok) {
        setIsFavourite(prev => !prev); // Toggle the local state for favorite status
        alert(isFavourite ? "Removed from favourites" : "Added to favourites");
      }
    } catch (error) {
      console.error('Error with toggling favourites:', error);
    }
  };

  return (
    <div className="product-details-container">
      <button onClick={() => navigate('/productlist')} className="add-to-cart-button">Go Back</button>

      {/* Heart icon that toggles the favorite status */}
      {isAuthenticated ? (
        isFavourite ? (
          <MdFavorite className="fav-icon" size={50} onClick={() => toggleFavourite(id, userProfile.id)} />
        ) : (
          <MdFavoriteBorder className="fav-icon" size={50} onClick={() => toggleFavourite(id, userProfile.id)} />
        )
      ) : (
        <MdFavoriteBorder className="fav-icon" size={50} onClick={() => alert("Please login first")} />
      )}

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
              <h4 className="product-name">{RarityTypes[productDetails.rarity]}</h4>

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
              {isAuthenticated ? (
                <button className="add-to-cart-button" onClick={() => setIsModalOpen(true)}>Get it now!</button>
              ) : (
                <button className="add-to-cart-button" onClick={() => navigate("/login")}>Login first</button>
              )}
            </div>
          </div>
        ) : (
          <h3 className="error-message">Product Not Found</h3>
        )}
      </div>

      {/* Confirmation Modal */}
      {isModalOpen && (
        <div className="confirmation-modal">
          <div className="modal-content">
            <h3>Confirm Purchase</h3>
            <p>Are you sure you want to purchase this product?</p>
            <button className="confirm-button" onClick={Purchase}>Confirm</button>
            <button className="cancel-button" onClick={() => setIsModalOpen(false)}>Cancel</button>
          </div>
        </div>
      )}
    </div>
  );
}
