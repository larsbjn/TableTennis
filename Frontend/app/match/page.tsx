'use client'
import {Button, Col, Container, Row} from "react-bootstrap";
import React, {useEffect} from "react";
import dynamic from "next/dynamic";
import {SingleValue, Theme} from "react-select";
import styles from './match.module.scss';
import {matchClient, userClient} from "@/api-clients";
import Spinner from "@/components/spinner/spinner";
import {UserDto} from "@/api-client";

interface Option {
    value: string;
    label: string;
}

const NoSSR = dynamic(() => import('react-select'), {ssr: false})
export default function StartGame() {

    const [isLoading, setIsLoading] = React.useState<boolean>(true);
    const [players, setPlayers] = React.useState<Array<Option>>([]);
    const [player1, setPlayer1] = React.useState<string>('');
    const [player2, setPlayer2] = React.useState<string>('');

    useEffect(() => {
        userClient.getAll().then((response) => {
            setPlayers(response.map((player: UserDto) => ({
                value: player.id?.toString() || '',
                label: player.name || ''
            })));
            setIsLoading(false);
        });
    }, []);

    const theme = (theme: Theme) => ({
        ...theme,
        colors: {
            ...theme.colors,
            primary: '#4a4a4a',
            primary25: '#4a4a4a',
            neutral50: 'white',
            neutral80: 'white',
            neutral0: 'rgb(43, 48, 53)',
        }
    })

    function createMatch() {
        matchClient.create(Number(player1), Number(player2)).then((response) => {
            window.location.href = `/match/${response}`;
        });
    }


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
        <Container className={styles.container}>
            <Row>
                <Col>
                    <h1>Start Match</h1>
                </Col>
            </Row>
            <Row className={styles.playerSelect}>
                <Col sm={12} lg={4}>
                    <h3>Player 1</h3>
                    <NoSSR options={players}
                           theme={theme}
                           onChange={(newValue: unknown) => {
                               setPlayer1((newValue as SingleValue<Option>)?.value || '');
                           }}
                    />
                </Col>
                <Col sm={12} lg={4} className={styles.alignCenter}>
                    <img className={styles.versusIcon} src="/images/table-tennis.png" alt="Table tennis"/>
                </Col>
                <Col sm={12} lg={4}>
                    <h3>Player 2</h3>
                    <NoSSR options={players}
                           theme={theme}
                           onChange={(newValue: unknown) => {
                               setPlayer2((newValue as SingleValue<Option>)?.value || '');
                           }}
                    />
                </Col>
            </Row>
            <Row>
                <Col className={styles.alignCenter}>
                    <Button disabled={player1 === '' || player2 === ''} onClick={createMatch} variant="primary"
                            type="submit" style={{marginTop: "15px"}}>
                        Start Match
                    </Button>
                </Col>
            </Row>
        </Container>
    );
}
