import {Button, Col, Container, Row} from 'react-bootstrap';
import Form from 'react-bootstrap/Form';
import styles from './create-player.module.scss';
import React from "react";
import {createPlayer} from "@/app/create-player/actions";

const Page: React.FC = () => {

    return (
        <Container className={styles.container}>
            <Row>
                <Col lg={{offset: 3, span: 6}}>
                    <h1>Create player</h1>
                </Col>
            </Row>
            <Row>
                <Col lg={{offset: 3, span: 6}}>
                    <Form action={createPlayer}>
                        <Form.Label>Name</Form.Label>
                        <Form.Control name={"name"} size="lg" type="text" placeholder="John Doe"/>
                        <br/>
                        <Form.Label>Initials</Form.Label>
                        <Form.Control name={"initials"} size="lg" type="text" placeholder="JD"/>
                        <br/>
                        <Button variant="primary" type="submit">
                            Create
                        </Button>
                    </Form>
                </Col>
            </Row>
        </Container>
    );
}

export default Page;