import { Outlet } from 'react-router-dom'; // Dit zal de pagina-inhoud dynamisch laden
import Header from '../navbar/Header'
import Footer from '../footer/Footer';

const Layout = () => {
  return (
    <div>
      <Header /> {/* De navbar is op elke pagina zichtbaar */}
      <div className="page-content">
        <Outlet /> {/* De inhoud van de huidige pagina wordt hier weergegeven */}
      </div>
      <Footer />
    </div>
  );
};

export default Layout;
