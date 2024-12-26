'use client'
import {Col, Container, Row, Table} from "react-bootstrap";
import {observer} from "mobx-react";
import {rankingStore} from "@/Stores/RankingStore";
import Spinner from "@/components/spinner/spinner";
import styles from "@/app/matches/matches.module.scss";
import React from "react";

const Dashboard = observer(() => {

    function getEmoji(ranking: number, length: number) {
        switch (ranking) {
            case 0:
                return '👑';
            case 1:
                return '🥈';
            case 2:
                return '🥉';
            case length - 3:
                return '🗑️';
            case length - 2:
                return '🤡';
            case length - 1:
                return '💩';
            default:
                return '🤗';
        }
    }
    
    function getTrend(winPercentage?: number, taa?: number) {
        if (winPercentage == undefined || taa == undefined) {
            return '??';
        }
        
        if (taa < winPercentage) {
            return '📉';
        } else if (taa > winPercentage) {
            return '📈';
        } else {
            return '😐';
        }
    }
    
    return (
        <Container>
            <Row>
                <Col lg={8}>
                    <h1>Ranking</h1>
                    {rankingStore.rankings.length === 0 && <Spinner/>}
                    {rankingStore.rankings.length > 0 && (
                        <Table striped hover>
                            <thead>
                            <tr>
                                <th>Emoji</th>
                                <th>Name</th>
                                <th>Played</th>
                                <th>Wins</th>
                                <th>Losses</th>
                                <th>Win %</th>
                                <th>TAA</th>
                                <th>Trend</th>
                                <th>Elo</th>
                            </tr>
                            </thead>
                            <tbody>
                            {rankingStore.rankings.map((ranking, index) => (
                                <tr className={styles.row} key={ranking.name}>
                                    <td>{getEmoji(index, rankingStore.rankings.length)}</td>
                                    <td>{ranking.name}</td>
                                    <td>{ranking.gamesPlayed}</td>
                                    <td>{ranking.wins}</td>
                                    <td>{ranking.losses}</td>
                                    <td>{ranking.winPercentage}</td>
                                    <td>{ranking.taa}</td>
                                    <td>{getTrend(ranking.winPercentage, ranking.taa)}</td>
                                    <td>{ranking.elo}</td>
                                </tr>
                            ))}
                            </tbody>
                        </Table>)}
                </Col>
                <Col lg={4}>
                    <h1>Ritzau</h1>
                </Col>
            </Row>
        </Container>
    );
});

export default Dashboard;