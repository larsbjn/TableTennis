'use client'
import React, {useEffect} from "react";
import {newsStore} from "@/Stores/NewsStore";
import styles from './news-ticker.module.scss';
import {observer} from "mobx-react";

const NewsTicker = observer(() => {
    const [isClient, setIsClient] = React.useState(false);

    React.useEffect(() => {
        setIsClient(true);
    }, []);

    useEffect(() => {
        let words = 0;
        newsStore.news.map((newsItem) => {
            if (newsItem.news) {
                words += newsItem.news.split(" ").length;
            }
        });
        let timer = words / 200 * 60;
        if (timer < 15) {
            timer = 15;
        }
        const root = document.documentElement;
        root.style.setProperty('--timer', `${timer}s`);
    }, [newsStore.news]);

    if (!isClient) {
        return null;
    }


    return (
        <div className={styles.tickerWrap}>
            <div className={styles.ticker}>
                {newsStore.news.map((newsItem, index) => (
                    <div key={index} className={styles.ticker__item}>{newsItem.news}</div>
                ))}
            </div>
        </div>
    );
});

export default NewsTicker;
