import React from 'react';
import './Header.css';
import { useNavigate, Link } from 'react-router-dom';
import useAuth from '../hooks/useAuth';

export default function Header() {
  const navigate = useNavigate();
  const { isAuthenticated} = useAuth();

  const handleMarketRedirect = () => {
    window.location.href = 'stop hier de link voor de nft shop'; // Replace with your actual NFT shop link
  };

  return (
    <header className="header-container">
      <nav className="navbar">
        <div className="title-image">
          <Link to="/"> {/* Use Link for routing in SPA */}
            <h3 className="project-name">ByteHeads</h3>
          </Link>
        </div>
        <ul className="navigation-list">
          <li>
            <Link className="nav-link" to="/productlist"> {/* Use Link for routing */}
              Collection
            </Link>
          </li>
          <li>
            <Link className="nav-link" to="/communitypage"> {/* Use Link for routing */}
              Community/Socials
            </Link>
          </li>
          <li>
            <a className="nav-link" onClick={handleMarketRedirect}>
              Marketplace
            </a>
          </li>
          <li>
            <Link className="nav-link" to="/profile"> {/* Use Link for routing */}
              My Profile
            </Link>
          </li>
        </ul>
      </nav>
    </header>
  );
}
