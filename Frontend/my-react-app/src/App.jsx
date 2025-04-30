import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import Hero from './components/Hero';
import ProductList from './components/ProductsList'
import PRODUCTS from './assets/lists/PRODUCTS'
import CommunityPage from './components/CommunityPage';
import ProductDetails from './components/ProductDetails';


function App() {

  return (
    <div>
        <Router>
          <Routes>
            <Route element={<Layout />}>
              <Route path='/' element={<Hero />}/>
              <Route path='/productlist' element={<ProductList products={PRODUCTS}/>}/>
              <Route path='/communitypage' element={<CommunityPage />}/>
              <Route path='/productdetails/:id' element={<ProductDetails />}/>
            </Route>
          </Routes>
        </Router>
    </div>
  )
}

export default App
