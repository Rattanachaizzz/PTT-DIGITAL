import React from 'react'
import { Link } from 'react-router-dom';
import '../css/Navbar.css';

function Navbar() {
    return (
        <div>
            <nav className='navbar'>
                <div className='logo-container'>
                    <h3 className='logo'><a ><Link to="/">Come Here</Link></a></h3>
                </div>
                <div className='link-container'>
                    <a href='#' style={{ paddingRight: '20px' }}><Link to='/login'>Login</Link></a>
                    <a href='#'><Link to="/register">Resgiter</Link></a>
                </div>
            </nav>
        </div>
    )
}

export default Navbar