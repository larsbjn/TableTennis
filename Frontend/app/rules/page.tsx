'use client'
import {Button, ButtonGroup, Col, Container, Row} from "react-bootstrap";
import {useEffect, useState} from "react";
import {RuleDto} from "@/api-client";
import {ruleClient} from "@/api-clients";
import styles from './rules.module.scss';

export default function Rules() {
    const [language, setLanguage] = useState<'da' | 'en'>('da');
    const [rules, setRules] = useState<RuleDto[]>([]);

    useEffect(() => {
        ruleClient.getAllRules().then((rules) => {
            setRules(rules);
        });
    }, []);

    return (
        <Container>
            <Row>
                <Col className={styles.header}>
                    <h1>Rules</h1>
                    <ButtonGroup aria-label="Basic example">
                        <Button variant="secondary"
                                className={`${styles.button} ${language === 'da' ? styles.active : ''}`}
                                onClick={() => setLanguage('da')}>Dansk</Button>
                        <Button variant="secondary" className={language === 'en' ? styles.active : ''}
                                onClick={() => setLanguage('en')}>English</Button>
                    </ButtonGroup>
                </Col>
            </Row>
            {rules.map((rule, index) => (
                <Row key={index}>
                    <Col className={styles.rule}>
                        <h2>§</h2>
                        <p>{language === 'da' ? rule.danish : rule.english}</p>
                    </Col>
                </Row>
            ))}
        </Container>
    )
}
