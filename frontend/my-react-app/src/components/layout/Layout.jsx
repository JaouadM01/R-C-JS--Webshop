import { Outlet } from 'react-router-dom';
import Header from '../navbar/Header';
import Footer from '../footer/Footer';
import './Layout.css'; 

const Layout = () => {
  return (
    <div className="layout-container">
      <Header />
      <div className="page-content">
        <Outlet />
      </div>
      <Footer />
    </div>
  );
};

export default Layout;
