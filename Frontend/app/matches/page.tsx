'use client'
import {Col, Container, Row, Table} from "react-bootstrap";
import Pagination from 'react-bootstrap/Pagination';
import {MatchDto} from "@/api-client";
import React, {useEffect} from "react";
import {matchClient} from "@/api-clients";
import styles from './matches.module.scss';
import Spinner from "@/components/spinner/spinner";

export default function Matches() {
    const [isLoading, setIsLoading] = React.useState<boolean>(true);
    const [matches, setMatches] = React.useState<Array<MatchDto>>([]);
    const [page, setPage] = React.useState<number>(0);
    const itemsPerPage = 10;
    const numberOfPages = Math.ceil(matches.length / itemsPerPage);

    const items = [];
    if (numberOfPages <= 5) {
        for (let number = 0; number < numberOfPages; number++) {
            items.push(
                <Pagination.Item key={number} onClick={() => {
                    setPage(number);
                }} active={number === page}>
                    {number + 1}
                </Pagination.Item>,
            );
        }
    } else {
        if (page < 3) {
            for (let number = 0; number < 5; number++) {
                items.push(
                    <Pagination.Item key={number} onClick={() => {
                        setPage(number);
                    }} active={number === page}>
                        {number + 1}
                    </Pagination.Item>,
                );
            }
            items.push(
                <Pagination.Ellipsis key={numberOfPages - 2} onClick={() => {
                    setPage(numberOfPages - 2);
                }}/>
            );
            items.push(
                <Pagination.Item key={numberOfPages} onClick={() => {
                    setPage(numberOfPages);
                }} active={numberOfPages === page}>
                    {numberOfPages}
                </Pagination.Item>,
            );
        } else if (page > numberOfPages - 6) {
            for (let number = numberOfPages - 7; number < numberOfPages; number++) {
                items.push(
                    <Pagination.Item key={number} onClick={() => {
                        setPage(number);
                    }} active={number === page}>
                        {number + 1}
                    </Pagination.Item>,
                );
            }
        } else {
            for (let number = page - 2; number < page + 3; number++) {
                items.push(
                    <Pagination.Item key={number} onClick={() => {
                        setPage(number);
                    }} active={number === page}>
                        {number + 1}
                    </Pagination.Item>,
                );
            }
            items.push(
                <Pagination.Ellipsis key={numberOfPages - 1} onClick={() => {
                    setPage(numberOfPages - 1);
                }}/>
            );
            items.push(
                <Pagination.Item key={numberOfPages} onClick={() => {
                    setPage(numberOfPages);
                }} active={numberOfPages === page}>
                    {numberOfPages}
                </Pagination.Item>,
            );
        }
    }

    useEffect(() => {
        matchClient.getAllMatches().then((response) => {
            setMatches(response.reverse());
            setIsLoading(false);
        });
    }, []);

    if (isLoading) {
        return <Container className={styles.container}>
            <Row>
                <Col className={styles.alignCenter}>
                    <Spinner/>
                </Col>
            </Row>
        </Container>
    }

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
                {matches.slice(page * itemsPerPage, page * itemsPerPage + itemsPerPage).map((match) => (
                    <tr onClick={() => {
                        window.location.href = `/match/${match.id}`;
                    }} className={styles.row} key={match.id}>
                        {match.players?.map((player) => (
                            <td key={player.user.name}>{player.user.name}</td>
                        ))}
                        <td>{match.players?.find(player => player.isWinner)?.user.name}</td>
                        <td>{match.date?.toLocaleDateString()}</td>
                        <td>{match.news && match.news?.length > 60 ? `${match.news.substring(0, 60)}...` : match.news}</td>
                        <td>{match.extraInfo1 && match.extraInfo1?.length > 60 ? `${match.extraInfo1.substring(0, 60)}...` : match.extraInfo1}</td>
                        <td>{match.extraInfo2 && match.extraInfo2?.length > 60 ? `${match.extraInfo2.substring(0, 60)}...` : match.extraInfo2}</td>
                    </tr>
                ))}
                </tbody>
            </Table>
            {numberOfPages > 1 && <Pagination className={styles.pagination}>{items}</Pagination>}
        </Container>
    );
}
