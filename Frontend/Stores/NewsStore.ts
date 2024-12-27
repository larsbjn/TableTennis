import {makeAutoObservable} from "mobx";
import * as signalR from "@microsoft/signalr";
import {newsClient} from "@/api-clients";
import {NewsDto} from "@/api-client";

export class NewsStore {
    news: Array<NewsDto> = [];

    constructor() {
        makeAutoObservable(this);
        this.fetchInitialNews();
        const url = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7116";
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(url + "/newsHub")
            .build();

        connection.on("UpdatedNews", (news: NewsDto[]) => {
            this.updateNews(news);
        });

        connection.start().catch((err) => document.write(err));
    }

    fetchInitialNews() {
        newsClient.getLatest(5).then((response) => {
            this.updateNews(response);
        });
    }

    updateNews(news: Array<NewsDto>) {
        this.news = news.map(item => {
            item.date = item.date ? new Date(item.date) : undefined;
            return item;
        });
    }
}

export const newsStore = new NewsStore();