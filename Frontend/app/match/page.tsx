'use client'
import {Button, Col, Container, Row} from "react-bootstrap";
import React, {useEffect} from "react";
import dynamic from "next/dynamic";
import {SingleValue, Theme} from "react-select";
import styles from './match.module.scss';
import {matchClient, userClient} from "@/api-clients";
import Spinner from "@/components/spinner/spinner";
import {NumberOfSets, UserDto} from "@/api-client";
import Dropdown, {Option} from "@/components/dropdown/dropdown";

export default function StartGame() {

    const [isLoading, setIsLoading] = React.useState<boolean>(true);
    const [players, setPlayers] = React.useState<Array<Option>>([]);
    const [player1, setPlayer1] = React.useState<string>('');
    const [player2, setPlayer2] = React.useState<string>('');
    const [numberOfSets, setNumberOfSets] = React.useState<NumberOfSets>(NumberOfSets._3);

    useEffect(() => {
        userClient.getAllUsers().then((response) => {
            setPlayers(response.map((player: UserDto) => ({
                value: player.id?.toString() || '',
                label: player.name || ''
            })));
            setIsLoading(false);
        });
    }, []);


    function createMatch() {
        matchClient.createMatch(Number(player1), Number(player2), numberOfSets).then((response) => {
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
                    <Dropdown options={players}
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
                    <Dropdown options={players}
                              onChange={(newValue: unknown) => {
                                  setPlayer2((newValue as SingleValue<Option>)?.value || '');
                              }}
                    />
                </Col>
            </Row>
            <Row>
                <Col className={`${styles.alignCenter} ${styles.settings}`}>
                    <Dropdown options={[{value: NumberOfSets._3, label: 'Best of 3'}, {
                        value: NumberOfSets._5,
                        label: 'Best of 5'
                    }]}
                              defaultValue={{value: NumberOfSets._3, label: 'Best of 3'}}
                              onChange={(newValue: unknown) => {
                                  setNumberOfSets((newValue as SingleValue<{
                                      value: NumberOfSets,
                                      label: string
                                  }>)?.value || NumberOfSets._3);
                              }}
                    />
                    <Button disabled={player1 === '' || player2 === ''} onClick={createMatch} variant="primary"
                            type="submit" style={{marginTop: "15px"}}>
                        Start Match
                    </Button>
                </Col>
            </Row>
        </Container>
    );
}
