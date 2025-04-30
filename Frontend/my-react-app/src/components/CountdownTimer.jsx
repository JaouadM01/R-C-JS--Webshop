import React, { useState, useEffect } from 'react';
import './CountdownTimer.css'

function CountdownTimer() {
  // Function to calculate the time left for the 2-week period
  const calculateTimeLeft = () => {
    const now = new Date();
    const targetDate = new Date(now);
    targetDate.setDate(now.getDate() + 14); // Add 14 days
    return targetDate;
  };

  // State for storing the target date (2 weeks from now)
  const [targetDate, setTargetDate] = useState(calculateTimeLeft());
  const [timeLeft, setTimeLeft] = useState({});

  // Function to format time left in days, hours, minutes, seconds
  const formatTimeLeft = (difference) => {
    const days = Math.floor(difference / (1000 * 60 * 60 * 24));
    const hours = Math.floor((difference % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((difference % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((difference % (1000 * 60)) / 1000);
    return { days, hours, minutes, seconds };
  };

  // Update the time left every second
  useEffect(() => {
    const interval = setInterval(() => {
      const now = new Date();
      const difference = targetDate - now;
      
      if (difference <= 0) {
        // If the time is up, reset the target date to 2 weeks later
        setTargetDate(calculateTimeLeft());
      } else {
        setTimeLeft(formatTimeLeft(difference));
      }
    }, 1000);

    return () => clearInterval(interval); // Clear the interval on component unmount
  }, [targetDate]);

return (
    <div className="countdown-container">
        <p className="countdown-title">Counting down till project closes</p>
        <div className="timer">
            {timeLeft.days}d {timeLeft.hours}h {timeLeft.minutes}m {timeLeft.seconds}s
        </div>
        <small>after a purchase the timer resets to 2 weeks</small>
    </div>
);
}

export default CountdownTimer;
