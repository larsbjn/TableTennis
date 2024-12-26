'use client'
import {matchClient} from "@/api-clients";
import {Button, Col, Container, Row} from "react-bootstrap";
import styles from './specific-match.module.scss';
import {MatchDto} from "@/api-client";
import React, {useEffect} from "react";
import Spinner from "@/components/spinner/spinner";


export default function Match({params}: { params: Promise<{ id: string }> }) {

    const [isLoading, setIsLoading] = React.useState<boolean>(true);
    const [match, setMatch] = React.useState<MatchDto>(new MatchDto());

    useEffect(() => {
        const getId = async () => {
            const matchId = await params;
            matchClient.get(Number(matchId.id)).then((response) => {
                setMatch(response)
            });
            setIsLoading(false);
        }
        getId();
    }, [params]);

    function updateWinner(userId?: number) {
        matchClient.updateWinner(match.id, userId).then(r => {
            setMatch(r);
        })
    }

    if (!isLoading) {
        return (
            <Container className={styles.container}>
                <Row className={styles.header}>
                    <Col>
                        <h1>Match</h1>
                    </Col>
                    <Col className={styles.date}><h5>{match.date?.toLocaleDateString()}</h5></Col>
                </Row>
                <Row>
                    <Col sm={12} lg={4}>
                        <h6>Player 1</h6>
                        <h2>{match.player1.name}</h2>
                        {match.winner?.id === match.player1.id && <h2>🏅🎉</h2>}
                        {!match.winner &&
                            <Button onClick={() => updateWinner(match.player1.id)} variant={"primary"}>Won🏅🎉</Button>
                        }
                    </Col>
                    <Col sm={12} lg={4} className={styles.alignCenter}>
                        <img className={styles.versusIcon} src="/images/table-tennis.png" alt="Table tennis"/>
                    </Col>
                    <Col sm={12} lg={4} className={styles.alignRight}>
                        <h6>Player 2</h6>
                        <h2>{match.player2.name}</h2>
                        {match.winner?.id === match.player2.id && <h2>🏅🎉</h2>}
                        {!match.winner &&
                            <Button onClick={() => updateWinner(match.player2.id)} variant={"primary"}>Won🏅🎉</Button>
                        }
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <h3>News</h3>
                        <p>{match.news}</p>
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <h3>Extra info 2</h3>
                        <p>{match.extraInfo1}</p>
                    </Col>
                    <Col>
                        <h3>Extra info 3</h3>
                        <p>{match.extraInfo2}</p>
                    </Col>
                </Row>
            </Container>
        );
    } else {
        return (
            <Container className={styles.container}>
                <Row>
                    <Col className={styles.alignCenter}>
                        <Spinner />
                    </Col>
                </Row>
            </Container>
        )
    }
}
