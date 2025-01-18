import React from 'react';
import { NavLink } from 'react-router-dom';
import { useAuth } from 'react-oidc-context';
import LoginDisplay from './LoginDisplay';
import CartDisplay from './CartDisplay';

const NavMenu: React.FC = () => {
  const auth = useAuth();

  const isAdmin = Array.isArray(auth.user?.profile?.role) && auth.user?.profile?.role.includes('Admin');

  return (
    <nav className="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
      <div className="container">
        <NavLink className="navbar-brand mb-0 h1" to="/">Game Store</NavLink>

        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            {auth.isAuthenticated && isAdmin && (
              <li className="nav-item">
                <NavLink
                  className={({ isActive }) => "nav-link" + (isActive ? " active" : "")}
                  to="/catalog">
                  Catalog
                </NavLink>
              </li>
            )}
          </ul>
          <div className="d-flex align-items-center">
            <div className="me-3 d-flex">
              <LoginDisplay />
            </div>
            <CartDisplay />
          </div>
        </div>
      </div>
    </nav>
  );
};

export default NavMenu;