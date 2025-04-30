import React from 'react';
import './Footer.css';
import { useNavigate } from 'react-router-dom';
import { FaGithub, FaTwitter, FaFacebook, FaInstagram } from 'react-icons/fa';

export default function Footer() {
  const navigate = useNavigate();
  const handleMarketRedirect = () => {
    window.location.href = 'stop hier de link voor de nft shop'; // Replace with your actual NFT shop link
  };

  return (
    <footer className="footer-container">
      <div className="footer-content">
        <div className="footer-links">
          <ul className="footer-nav-list">
            <li>
              <a
                className="footer-nav-link"
                onClick={() => navigate('/')}
              >
                Home
              </a>
            </li>
            <li>
              <a
                className="footer-nav-link"
                onClick={() => navigate('/productlist')}
              >
                Collection
              </a>
            </li>
            <li>
              <a
                className="footer-nav-link"
                onClick={() => navigate('/communitypage')}
              >
                Community/Socials
              </a>
            </li>
            <li>
              <a
                className="footer-nav-link"
                onClick={handleMarketRedirect}
              >
                Marketplace
              </a>
            </li>
          </ul>
        </div>

        <div className="footer-socials">
          <a href="https://github.com" className="social-icon-link">
            <FaGithub className="social-icon" />
          </a>
          <a href="https://twitter.com" className="social-icon-link">
            <FaTwitter className="social-icon" />
          </a>
          <a href="https://facebook.com" className="social-icon-link">
            <FaFacebook className="social-icon" />
          </a>
          <a href="https://instagram.com" className="social-icon-link">
            <FaInstagram className="social-icon" />
          </a>
        </div>
      </div>

      <div className="footer-bottom">
        <p>© 2024 Your Company, Inc. All rights reserved.</p>
      </div>
    </footer>
  );
}
