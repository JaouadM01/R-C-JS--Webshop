import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './ProductCreationForm.css';
import { useAuth } from './context/AuthProvider';

export default function ProductCreationForm() {
    const { isAuthenticated, userProfile } = useAuth();
    const navigate = useNavigate();

    const [form, setForm] = useState({
        Name: '',
        Type: 'Electronics',
        Description: '',
        Price: 0,
        Image: '',
        Status: 0,
        Rarity: 0,
    });

    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setSuccess('');
        console.log(form);

        const defaultImage = "https://brown-abstract-panther-296.mypinata.cloud/ipfs/bafkreiaplp3byr2xhkempkdhpjqxuedl5ez5anygqs6lfzoaqcdgg5rauu";

        // Prepare the form data with fallback image
        const payload = {
            ...form,
            Price: parseFloat(form.Price),
            Image: form.Image.trim() === '' ? defaultImage : form.Image
        };

        try {
            const response = await fetch(`http://localhost:5224/api/Products/CreateProduct?userId=${userProfile.id}`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(payload)
            });

            if (response.ok) {
                setSuccess('Product created successfully!');
                setTimeout(() => navigate('/productlist'), 1500);
            } else {
                const message = await response.text();
                setError(`Failed to create product: ${message}`);
            }
        } catch (err) {
            setError(`Error: ${err.message}`);
        }
    };


    if (!isAuthenticated) {
        return <div><h2>You have no creation permissions</h2></div>;
    }

    if (userProfile.role !== "ProductSeller" && userProfile.role !== "BackendMedewerker") {
        return <div><h2>You have no creation permissions</h2></div>
    }



    return (
        <div className="product-form-container">
            <h2>Create New Product</h2>
            {error && <div className="error-msg">{error}</div>}
            {success && <div className="success-msg">{success}</div>}

            <form onSubmit={handleSubmit} className="product-form">
                <input type="text" name="Name" placeholder="Product Name" value={form.Name} onChange={handleChange} required />

                <select name="Type" value={form.Type} onChange={handleChange}>
                    {[
                        'Electronics', 'Clothing', 'HomeAppliances', 'Books', 'Toys', 'Groceries',
                        'Furniture', 'SportsEquipment', 'BeautyProducts', 'Automotive'
                    ].map(type => (
                        <option key={type} value={type}>{type}</option>
                    ))}
                </select>

                <textarea name="Description" placeholder="Description" value={form.Description} onChange={handleChange} required />

                <input type="number" name="Price" placeholder="Price (in Satoshi)" value={form.Price} onChange={handleChange} min="0" step="0.01" required />

                <input type="text" name="Image" placeholder="Image URL (optional)" value={form.Image} onChange={handleChange} />

                <select name="Status" value={form.Status} onChange={(e) => setForm(prev => ({ ...prev, Status: Number(e.target.value) }))}>
                    <option value={1}>Listed</option>
                    <option value={0}>Owned</option>
                </select>

                <select name="Rarity" value={form.Rarity} onChange={(e) => setForm(prev => ({ ...prev, Rarity: Number(e.target.value) }))}>
                    <option value={0}>Rare</option>
                    <option value={1}>Uncommon</option>
                    <option value={2}>Common</option>
                </select>

                <button type="submit">Create Product</button>
            </form>
        </div>
    );
}
