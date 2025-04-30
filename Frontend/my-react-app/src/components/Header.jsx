import React from 'react';
import './Header.css';
import { useNavigate } from 'react-router-dom';


export default function Header() {
  const navigate = useNavigate();
  const handleMarketRedirect = () => {
    window.location.href = 'stop hier de link voor de nft shop'; // Replace with your actual Discord invite link
  };

  return (
    <header className="header-container">
      <nav className="navbar">
        <div className="title-image">
          <a href="#">
            <h3 className="project-name" onClick={() => navigate('/')}>Project Name</h3>
            <img
              onClick={() => navigate('/')}
              alt="Project"
              src="path/to/your/image.jpg"
              className="project-image"
            />
          </a>
        </div>
        <ul className="navigation-list">
          <li>
            <a 
            className="nav-link"
            onClick={() => navigate('/productlist')}
            >
              Collection
            </a>
          </li>
          <li>
            <a 
            className="nav-link"
            onClick={() => navigate('/communitypage')}
            >
              Community/Socials
            </a>
          </li>
          <li>
            <a 
            className="nav-link"
            onClick={handleMarketRedirect}
            >
              Marketplace
            </a>
          </li>
          <li>
            <a 
            className="nav-link"
            onClick={() => navigate('/login')}
            >
              Login
            </a>
          </li>
            
        </ul>
      </nav>
    </header>
  );
}
