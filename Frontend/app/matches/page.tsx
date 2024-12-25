'use client'
import {Col, Container, Row, Table} from "react-bootstrap";
import {MatchDto} from "@/api-client";
import React, {useEffect} from "react";
import {matchClient} from "@/api-clients";
import styles from './matches.module.scss';

export default function Matches() {
    const [matches, setMatches] = React.useState<Array<MatchDto>>([]);

    useEffect(() => {
        matchClient.getAll().then((response) => {
            setMatches(response);
        });
    }, []);

    return (
        <Container>
            <Row>
                <Col>
                    <h1>Matches</h1>
                </Col>
            </Row>
            <Table striped hover>
                <thead>
                <tr>
                    <th>Player 1</th>
                    <th>Player 2</th>
                    <th>Winner</th>
                    <th>Date</th>
                    <th>Latest news</th>
                    <th>Extra info 2</th>
                    <th>Extra info 3</th>
                </tr>
                </thead>
                <tbody>
                {matches.map((match, index) => (
                    <tr onClick={() => {
                        window.location.href = `/match/${match.id}`;
                    }} className={styles.row} key={match.id}>
                        <td>{match.player1.name}</td>
                        <td>{match.player2.name}</td>
                        <td>{match.winner?.name}</td>
                        <td>{match.date?.toLocaleDateString()}</td>
                        <td>{match.news && match.news?.length > 60 ? `${match.news.substring(0, 60)}...` : match.news}</td>
                        <td>{match.extraInfo1 && match.extraInfo1?.length > 60 ? `${match.extraInfo1.substring(0, 60)}...` : match.extraInfo1}</td>
                        <td>{match.extraInfo2 && match.extraInfo2?.length > 60 ? `${match.extraInfo2.substring(0, 60)}...` : match.extraInfo2}</td>
                    </tr>
                ))}
                </tbody>
            </Table>
        </Container>
    );
}
