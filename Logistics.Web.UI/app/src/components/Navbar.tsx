import React from 'react';
import { NavLink } from 'react-router-dom';

export const Navbar = () => {
    return <nav className="navbar navbar-expand-lg shadow navbar-light bg-light">
        <a className="navbar-brand" href="/">CODE RUNNERS - Logistics</a>

        <div className="collapse navbar-collapse" id="navbarSupportedContent">
            <ul className="navbar-nav ml-auto">
                <li className="nav-item">
                    <NavLink to="/" className="nav-link" activeClassName="active">Home</NavLink>
                </li>
                <li className="nav-item">
                    <NavLink to="/settlements" className="nav-link" activeClassName="active">Settlements</NavLink>
                </li>
            </ul>
        </div>
    </nav>
}