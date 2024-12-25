import {Container, Nav, Navbar, NavbarBrand, NavbarCollapse, NavbarToggle, NavLink} from "react-bootstrap";
import React from "react";
import styles from './navigation.module.scss';

const Navigation: React.FC = () => {
    return (
        <Navbar expand="lg" className={`bg-body-tertiary ${styles.nav}`}>
            <Container>
                <NavbarBrand href="/">Table tennis</NavbarBrand>
                <NavbarToggle aria-controls="basic-navbar-nav" />
                <NavbarCollapse id="basic-navbar-nav">
                    <Nav className="ms-auto">
                        <NavLink href="/matches">Matches</NavLink>
                        <NavLink href="/create-player">Create player</NavLink>
                        <NavLink href="/match">Start match</NavLink>
                    </Nav>
                </NavbarCollapse>
            </Container>
        </Navbar>
    )
}

export default Navigation;