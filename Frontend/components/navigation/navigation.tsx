import {Container, Nav, Navbar, NavbarBrand, NavbarCollapse, NavbarToggle, NavLink} from "react-bootstrap";
import React from "react";
import styles from './navigation.module.scss';
import Link from "next/link";

const Navigation: React.FC = () => {
    return (
        <Navbar expand="lg" className={`bg-body-tertiary ${styles.nav}`}>
            <Container>
                <Link className={"navbar-brand"} href={"/"}>Table Tennis</Link>
                <NavbarToggle aria-controls="basic-navbar-nav"/>
                <NavbarCollapse id="basic-navbar-nav">
                    <Nav className="ms-auto">
                        <Link className={"nav-link"} href={"/rules"}>Rules</Link>
                        <Link className={"nav-link"} href={"/matches"}>Matches</Link>
                        <Link className={"nav-link"} href={"/create-player"}>Create player</Link>
                        <Link className={"nav-link"} href={"/match"}>Start match</Link>
                    </Nav>
                </NavbarCollapse>
            </Container>
        </Navbar>
    )
}

export default Navigation;