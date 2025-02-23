'use client'
import {Col, Container, Row, Table} from "react-bootstrap";
import {observer} from "mobx-react";
import {rankingStore} from "@/Stores/RankingStore";
import Spinner from "@/components/spinner/spinner";
import React from "react";
import {newsStore} from "@/Stores/NewsStore";
import styles from "@/app/dashboard.module.scss";
import Dropdown, {Option} from "@/components/dropdown/dropdown";

const Dashboard = observer(() => {


    const [newNews, setNewNews] = React.useState(false);


    React.useEffect(() => {
        setNewNews(true);
        const timer = setTimeout(() => {
            setNewNews(false);
        }, 5000);

        return () => clearTimeout(timer);
    }, [newsStore.news]);

    return (
        <Container className={styles.container} fluid>
            <Row>
                <Col className={styles.ranking} xl={7}>
                    <Ranking/>
                </Col>
                <Col className={`${styles.ritzau} ${newNews && styles.newNews}`} xl={{offset: 1, span: 4}}>
                    <News/>
                </Col>
            </Row>
        </Container>
    )
        ;
});

const Ranking = observer(() => {
    const [rankingIsLoading, setRankingIsLoading] = React.useState(true);
    const [seasons, setSeasons] = React.useState<Array<Option>>([]);

    React.useEffect(() => {
        setSeasons([
            ...rankingStore.seasons.map((season, index) => {
                return {value: rankingStore.seasons.length - index, label: season};
            }),
            {value: 0, label: "All Time"}
        ]);
    }, [rankingStore.seasons]);

    React.useEffect(() => {
        setRankingIsLoading(rankingStore.rankings.length === 0);
    }, [rankingStore.rankings.length]);

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

        if (taa == 0) {
            return '🤷';
        } else if (taa < winPercentage) {
            return '📉';
        } else if (taa > winPercentage) {
            return '📈';
        } else {
            return '😐';
        }
    }

    return (
        <>
            <div className={styles.header}>
                <h2>Ranking</h2>

                <Dropdown options={seasons}
                          defaultValue={seasons[0]}
                          searchable={false}
                          onChange={(newValue: Option) => {
                              rankingStore.changeSeason(newValue.value);
                          }}
                /></div>
            {rankingIsLoading ? <Spinner/> : (
                <Table className={styles.table} striped hover>
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
        </>
    );
});

const News = observer(() => {
    const [newsIsLoading, setNewsIsLoading] = React.useState(true);

    React.useEffect(() => {
        setNewsIsLoading(newsStore.news.length === 0);
    }, [newsStore.news.length]);

    return (
        <>
            <h2>🚨‼️ B R E A K I N G ‼️🚨</h2>
            {newsIsLoading ? <Spinner/> : (
                <>
                    <div>
                        <p className={styles.newsText}>
                            {newsStore.news[0].news}
                        </p>
                    </div>
                    <p className={styles.date}>{newsStore.news[0].date?.toLocaleString('en-GB', {timeZone: 'CET'})} -
                        Ritzau</p>
                </>
            )}
        </>
    );
});

export default Dashboard;