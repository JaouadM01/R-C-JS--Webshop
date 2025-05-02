import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/layout/Layout';
import Hero from './components/Hero';
import ProductList from './components/ProductsList';
import PRODUCTS from './assets/lists/PRODUCTS';
import CommunityPage from './components/CommunityPage';
import ProductDetails from './components/ProductDetails';
import Profile from './components/Profile';
import Login from './components/Authentication/Login';
import { AuthProvider } from './components/context/AuthProvider';

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<Hero />} />
            <Route path="/productlist" element={<ProductList products={PRODUCTS} />} />
            <Route path="/communitypage" element={<CommunityPage />} />
            <Route path="/productdetails/:id" element={<ProductDetails />} />
            <Route path='/profile' element={<Profile />} />
            <Route path='/login' element={<Login />} />
          </Route>
        </Routes>
      </Router>
    </AuthProvider>
  );
}

export default App;
