'use client'
import React from "react";
import {newsStore} from "@/Stores/NewsStore";
import styles from './news-ticker.module.scss';
import {observer} from "mobx-react";
const NewsTicker= observer(() => {
    const [isClient, setIsClient] = React.useState(false);

    React.useEffect(() => {
        setIsClient(true);
    }, []);

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
