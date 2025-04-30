import React from "react";
import PropTypes from 'prop-types';

import './ProductsList.css'; // Import the custom CSS
import { useNavigate } from "react-router-dom";

const ProductsList = ({ products }) => {
  const navigate = useNavigate();

  return (
    <div className="product-list-container">
      <h2 className="sr-only">Products</h2>

      <div className="product-grid">
        {products.map((product) => (
          <div key={product.id} className="product-card" onClick={() => navigate(`/productdetails/${product.id}`)}>
            <img
              alt={product.name}
              src={product.image.src}
              className="product-image"
            />
            <h3 className="product-name">{product.name}</h3>
            <div className="product-description">
              {product.description.map((desc, index) => (
                <p key={index} className="description-item">{desc}</p>
              ))}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

ProductsList.propTypes = {
  products: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number.isRequired,
      name: PropTypes.string.isRequired,
      description: PropTypes.arrayOf(PropTypes.string).isRequired,
      image: PropTypes.shape({
        src: PropTypes.string.isRequired,
      }).isRequired,
    })
  ).isRequired,
};

export default ProductsList;
