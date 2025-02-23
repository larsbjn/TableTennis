'use client'
import {Button, Col, Container, Row} from 'react-bootstrap';
import Form from 'react-bootstrap/Form';
import styles from './create-player.module.scss';
import React, {useState} from "react";
import {userClient} from "@/api-clients";
import {redirect} from "next/navigation";

const Page: React.FC = () => {
    const [name, setName] = useState("");
    return (
        <Container className={styles.container}>
            <Row>
                <Col lg={{offset: 3, span: 6}}>
                    <h1>Create player</h1>
                </Col>
            </Row>
            <Row>
                <Col lg={{offset: 3, span: 6}}>
                    <Form>
                        <Form.Label>Name</Form.Label>
                        <Form.Control name={"name"} size="lg" type="text" placeholder="John Doe" value={name}
                                      onChange={(e) => setName(e.target.value)}/>
                        <br/>
                        <Button variant="primary" onClick={async (e) => {
                            e.preventDefault();
                            e.currentTarget.disabled = true;
                            await userClient.createUser(name, "");
                            redirect('/');
                        }}>
                            Create
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
    );
}

export default Page;