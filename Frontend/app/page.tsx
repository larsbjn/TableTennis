'use client'
import {Col, Container, Row, Table} from "react-bootstrap";
import {observer} from "mobx-react";
import {rankingStore} from "@/Stores/RankingStore";
import Spinner from "@/components/spinner/spinner";
import styles from "@/app/matches/matches.module.scss";
import React from "react";

const Dashboard = observer(() => {

    return (
        <Container>
            <Row>
                <Col lg={6}>
                    <h1>Ranking</h1>
                    {rankingStore.rankings.length === 0 && <Spinner/>}
                    {rankingStore.rankings.length > 0 && (
                        <Table striped hover>
                            <thead>
                            <tr>
                                <th>Name</th>
                                <th>Played</th>
                                <th>Wins</th>
                                <th>Losses</th>
                                <th>Win %</th>
                                <th>Elo</th>
                            </tr>
                            </thead>
                            <tbody>
                            {rankingStore.rankings.map((ranking, index) => (
                                <tr className={styles.row} key={ranking.name}>
                                    <td>{ranking.name}</td>
                                    <td>{ranking.gamesPlayed}</td>
                                    <td>{ranking.wins}</td>
                                    <td>{ranking.losses}</td>
                                    <td>{ranking.winPercentage}</td>
                                    <td>{ranking.elo}</td>
                                </tr>
                            ))}
                            </tbody>
                        </Table>)}
                </Col>
                <Col lg={6}>
                    <h1>Ritzau</h1>
                </Col>
            </Row>
        </Container>
    );
});

export default Dashboard;