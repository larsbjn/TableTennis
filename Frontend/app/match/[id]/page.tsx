'use client'
import {matchClient} from "@/api-clients";
import {Button, Col, Container, FormControl, Row} from "react-bootstrap";
import styles from './specific-match.module.scss';
import {MatchDto, UpdateScoreDto} from "@/api-client";
import React, {useEffect} from "react";
import Spinner from "@/components/spinner/spinner";


export default function Match({params}: { params: Promise<{ id: string }> }) {

    const [isEditing, setIsEditing] = React.useState<boolean>(false);
    const [isLoading, setIsLoading] = React.useState<boolean>(true);
    const [match, setMatch] = React.useState<MatchDto>(new MatchDto());
    const [news, setNews] = React.useState<string>("");
    const [extraInfo1, setExtraInfo1] = React.useState<string>("");
    const [extraInfo2, setExtraInfo2] = React.useState<string>("");
    const [score1, setScore1] = React.useState<string>("");
    const [score2, setScore2] = React.useState<string>("");

    useEffect(() => {
        const getId = async () => {
            const matchId = await params;
            matchClient.getMatch(Number(matchId.id)).then((response) => {
                setMatch(response)
                setIsEditing(!response.isFinished);
                setNews(response.news ? response.news : "");
                setExtraInfo1(response.extraInfo1 ? response.extraInfo1 : "");
                setExtraInfo2(response.extraInfo2 ? response.extraInfo2 : "");
                setScore1(response.players?.[0].score?.toString() ?? "");
                setScore2(response.players?.[1].score?.toString() ?? "");
            });
            setIsLoading(false);
        }
        getId();
    }, [params]);

    function updateWinner(userId?: number) {
        const m = {...match} as MatchDto;
        m.players?.forEach(p => {
            if (p.user.id === userId) {
                p.isWinner = true;
            }
        });
        setMatch(m);
        save(m, true);
    }

    function save(entity: MatchDto = match, updateWinner: boolean = false) {
        const payload = {
            id: entity.id ?? 0,
            winnerId: entity.players?.find(p => p.isWinner)?.user.id,
            news: news,
            extraInfo1: extraInfo1,
            extraInfo2: extraInfo2
        }
        const scores: UpdateScoreDto[] = [];
        scores.push({playerId: entity.players?.[0].user.id ?? 0, score: Number(score1)} as UpdateScoreDto);
        scores.push({playerId: entity.players?.[1].user.id ?? 0, score: Number(score2)} as UpdateScoreDto);

        matchClient.updateMatch(payload.id, payload.winnerId, payload.news, payload.extraInfo1, payload.extraInfo2, updateWinner, scores).then(r => {
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
                        <h2>{match.players?.[0].user.name}</h2>
                        {!isEditing && match.players?.[0].isWinner && <h2>🏅🎉</h2>}
                        {(!match.isFinished || isEditing) &&
                            <>
                                {
                                    match.players?.[0].user.id &&
                                    <FormControl style={{margin: "10px 0", width: "100px"}} as="input" type={"number"}
                                                 value={score1}
                                                 onChange={e => {
                                                     setScore1(e.target.value);
                                                 }}/>
                                }
                                <Button onClick={() => updateWinner(match.players?.[0].user.id)}
                                        variant={"primary"}>Won🏅🎉</Button>
                            </>
                        }

                    </Col>
                    <Col sm={12} lg={4} className={styles.alignCenter} style={{gap: "30px", flexDirection: "column"}}>
                        <div style={{display: "flex", alignItems: "center", gap: "30px"}}>
                            <h2>{score1}</h2>
                            <img className={styles.versusIcon} src="/images/table-tennis.png" alt="Table tennis"/>
                            <h2>{score2}</h2>
                        </div>
                        <h2>Best of {match.numberOfSets}</h2>
                    </Col>
                    <Col sm={12} lg={4} className={styles.alignRight}>
                        <h6>Player 2</h6>
                        <h2>{match.players?.[1].user.name}</h2>
                        {!isEditing && match.players?.[1].isWinner && <h2>🏅🎉</h2>}
                        {(!match.isFinished || isEditing) &&
                            <>
                                <FormControl style={{margin: "10px 0", width: "100px", textAlign: "right"}} as="input"
                                             type={"number"} value={score2 ?? ""} onChange={e => {
                                    setScore2(e.target.value);
                                }}
                                />
                                <Button onClick={() => updateWinner(match.players?.[1].user.id)}
                                        variant={"primary"}>Won🏅🎉</Button>
                            </>
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
