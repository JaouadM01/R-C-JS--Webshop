import React from 'react';
import './CommunityPage.css'

const communityReviews = [
  {
    name: "Alice",
    review: "Joining this NFT community has been an incredible experience. The collaboration, creativity, and innovation here is unmatched. I feel like part of something special!",
  },
  {
    name: "Bob",
    review: "This is more than just an NFT project; it's a movement. The community is so supportive and the projects are truly revolutionary. I'm proud to be a part of it.",
  },
  {
    name: "Charlie",
    review: "If you're looking for a place to be inspired and connect with passionate individuals, this is it. The possibilities with NFTs and our community are endless.",
  },
];

const CommunityPage = () => {
  const handleJoinDiscord = () => {
    window.location.href = 'https://discord.com/invite/your-server-link'; // Replace with your actual Discord invite link
  };

  return (
    <div className="community-page-container">
      <section className="intro-section">
        <h1 className="page-title">Join the NFT Revolution</h1>
        <p className="intro-description">
          Our NFT community is built on collaboration, creativity, and groundbreaking projects that change the way we view the digital world. 
          This is more than just a community — it’s a movement. A movement that you can be a part of.
        </p>
        <button className="join-button" onClick={handleJoinDiscord}>Join Our Discord</button>
      </section>

      <section className="reviews-section">
        <h2 className="reviews-title">What Our Community Members Are Saying</h2>
        <div className="reviews-list">
          {communityReviews.map((review, index) => (
            <div key={index} className="review-card">
              <p className="review-text">"{review.review}"</p>
              <p className="reviewer-name">- {review.name}</p>
            </div>
          ))}
        </div>
      </section>

      <section className="cta-section">
        <h2 className="cta-title">Are You Ready to Join Us?</h2>
        <p className="cta-description">
          Don't miss out on this incredible opportunity to be part of a forward-thinking NFT community. 
          The time to act is now.
        </p>
        <button className="cta-button" onClick={handleJoinDiscord}>Join the Movement</button>
      </section>
    </div>
  );
};

export default CommunityPage;
