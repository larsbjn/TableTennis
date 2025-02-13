'use client';
import {HubConnectionBuilder} from "@microsoft/signalr";
import {observer} from "mobx-react";
import React, {useEffect} from "react";

const KeepAlive = observer(() => {
    
    useEffect(() => {
        const url = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7116";
        const connection = new HubConnectionBuilder()
            .withUrl(url + "/keepAliveHub")
            .build();

        // Start the connection
        const startConnection = async () => {
            try {
                await connection.start();
                console.log('Connected to SignalR');

                // Setup keep-alive
                const keepAliveInterval = setInterval(() => {
                    if (connection.state === 'Connected') {
                        console.log('Sending keep-alive');
                        connection.invoke('KeepAlive').catch(err => console.error(err));
                    }
                }, 30000); // Sends a keep-alive every 30 seconds

                // Clean up on unmount
                return () => {
                    clearInterval(keepAliveInterval);
                    connection.stop();
                };
            } catch (err) {
                console.error('Connection failed: ', err);
            }
        };

        startConnection();

    }, []);
    
    return (
        <div></div>
    );
});

export default KeepAlive;
