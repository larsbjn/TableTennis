'use client'
import {matchClient} from "@/api-clients";
import {Button, Col, Container, FormControl, Row} from "react-bootstrap";
import styles from './specific-match.module.scss';
import {MatchDto} from "@/api-client";
import React, {useEffect} from "react";
import Spinner from "@/components/spinner/spinner";


export default function Match({params}: { params: Promise<{ id: string }> }) {

    const [isEditing, setIsEditing] = React.useState<boolean>(false);
    const [isLoading, setIsLoading] = React.useState<boolean>(true);
    const [match, setMatch] = React.useState<MatchDto>(new MatchDto());
    const [news, setNews] = React.useState<string>("");
    const [extraInfo1, setExtraInfo1] = React.useState<string>("");
    const [extraInfo2, setExtraInfo2] = React.useState<string>("");

    useEffect(() => {
        const getId = async () => {
            const matchId = await params;
            matchClient.get(Number(matchId.id)).then((response) => {
                setMatch(response)
                setIsEditing(response.winner === undefined);
                setNews(response.news ? response.news : "");
                setExtraInfo1(response.extraInfo1 ? response.extraInfo1 : "");
                setExtraInfo2(response.extraInfo2 ? response.extraInfo2 : "");
            });
            setIsLoading(false);
        }
        getId();
    }, [params]);

    function updateWinner(userId?: number) {
        let m = {...match, winner: userId == match.player1.id ? match.player1 : match.player2} as MatchDto;
        setMatch(m);
        save(m, true);
    }

    function save(entity: MatchDto = match, updateWinner: boolean = false) {
        const payload = {
            id: entity.id,
            winner: entity.winner,
            news: news,
            extraInfo1: extraInfo1,
            extraInfo2: extraInfo2
        }
        matchClient.update(payload.id, payload.winner?.id, payload.news, payload.extraInfo1, payload.extraInfo2, updateWinner).then(r => {
            setMatch(r);
        });
        setIsEditing(false);
    }

    if (!isLoading) {
        return (
            <Container className={styles.container}>
                <Row className={styles.header}>
                    <Col>
                        <h1>Match</h1>
                    </Col>
                    <Col className={styles.date}>
                        <h5>{match.date?.toLocaleDateString()}</h5>
                        {isEditing && <Button variant={"primary"} onClick={() => save(match)}>Save</Button>}
                        {!isEditing && <Button variant={"secondary"} onClick={() => setIsEditing(true)}>Edit</Button>}

                    </Col>
                </Row>
                <Row>
                    <Col sm={12} lg={4}>
                        <h6>Player 1</h6>
                        <h2>{match.player1.name}</h2>
                        {!isEditing && match.winner?.id === match.player1.id && <h2>🏅🎉</h2>}
                        {(!match.winner || isEditing) &&
                            <Button onClick={() => updateWinner(match.player1.id)} variant={"primary"}>Won🏅🎉</Button>
                        }

                    </Col>
                    <Col sm={12} lg={4} className={styles.alignCenter}>
                        <img className={styles.versusIcon} src="/images/table-tennis.png" alt="Table tennis"/>
                    </Col>
                    <Col sm={12} lg={4} className={styles.alignRight}>
                        <h6>Player 2</h6>
                        <h2>{match.player2.name}</h2>
                        {!isEditing && match.winner?.id === match.player2.id && <h2>🏅🎉</h2>}
                        {(!match.winner || isEditing) &&
                            <Button onClick={() => updateWinner(match.player2.id)} variant={"primary"}>Won🏅🎉</Button>
                        }
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <h3>News</h3>
                        {!isEditing && <p>{match.news}</p>}
                        {(isEditing) && (
                            <FormControl as="textarea" rows={3} value={news} onChange={e => setNews(e.target.value)}/>
                        )}
                    </Col>
                </Row>
                <Row>
                    <Col>
                        <h3>Extra info 2</h3>
                        {!isEditing && <p>{match.extraInfo1}</p>}
                        {(isEditing) && (
                            <FormControl as="textarea" rows={3} value={extraInfo1}
                                         onChange={e => setExtraInfo1(e.target.value)}/>
                        )}
                    </Col>
                    <Col>
                        <h3>Extra info 3</h3>
                        {!isEditing && <p>{match.extraInfo2}</p>}
                        {(isEditing) && (
                            <FormControl as="textarea" rows={3} value={extraInfo2}
                                         onChange={e => setExtraInfo2(e.target.value)}/>
                        )}
                    </Col>
                </Row>
            </Container>
        );
    } else {
        return (
            <Container className={styles.container}>
                <Row>
                    <Col className={styles.alignCenter}>
                        <Spinner/>
                    </Col>
                </Row>
            </Container>
        )
    }
}
