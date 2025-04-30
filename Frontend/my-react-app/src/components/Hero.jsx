import "./Hero.css";
import CountdownTimer from './CountdownTimer';
import { useNavigate } from "react-router-dom";

export default function Hero() {
  const navigate = useNavigate();

  return (
    <div className="hero-container">
      <div className="main-content">
        <div className="background-blur"></div>
        <div className="hero-text">
          <h1 className="headline">STOP being normal... Be extra normal</h1>
          <p className="description">
            "Build an intriguing and inspiring project description:"
          </p>
          <CountdownTimer />
          <div className="cta-buttons">
            <a href="#" className="get-started-btn" onClick={() => navigate('/productlist')}>
              Intrigued?
            </a>
            <a href="#" className="learn-more-link">
              Already a "name project"
            </a>
          </div>
          <div className="announcement">
            Announcing our next projects{' '}
            <a href="#" className="announcement-link">
              Here →
            </a>
          </div>
        </div>
        <div className="background-blur2"></div>
      </div>
    </div>
  );
}
