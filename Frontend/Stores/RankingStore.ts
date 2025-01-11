import {makeAutoObservable} from "mobx";
import * as signalR from "@microsoft/signalr";
import {rankingClient} from "@/api-clients";
import {RankingDto} from "@/api-client";

export class RankingStore {
    rankings: Array<RankingDto> = [];

    constructor() {
        makeAutoObservable(this);
        this.fetchInitialRankings();
        const url = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7116";
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(url + "/rankingHub")
            .build();

        connection.on("UpdatedRanking", (rankings: RankingDto[]) => {
            this.updateRankings(rankings);
        });

        connection.start().catch((err) => document.write(err));
    }

    fetchInitialRankings() {
        rankingClient.getAllRankings().then((response) => {
            this.updateRankings(response);
        });
    }

    updateRankings(rankings: Array<RankingDto>) {
        this.rankings = []
        this.rankings.push(...rankings);
    }
}

export const rankingStore = new RankingStore();