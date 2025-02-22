import {makeAutoObservable} from "mobx";
import * as signalR from "@microsoft/signalr";
import {rankingClient, seasonClient} from "@/api-clients";
import {RankingDto} from "@/api-client";

export class RankingStore {
    rankings: Array<RankingDto> = [];
    seasons: Array<string> = [];
    currentSeason: number = 0;

    constructor() {
        makeAutoObservable(this);
        this.fetchInitialRankings();
        const url = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7116";
        const connection = new signalR.HubConnectionBuilder()
            .withUrl(url + "/rankingHub")
            .build();

        connection.on("NotifyAboutUpdatedRanking", () => {
            this.changeSeason(this.currentSeason);
        });

        connection.start().catch((err) => document.write(err));
    }

    fetchInitialRankings() {
        seasonClient.getAllSeasons().then((response) => {
            this.seasons = response;
            this.changeSeason(response.length);       
        });
    }
    
    changeSeason(season: number) {
        this.currentSeason = season;
        if (season === 0) {
            rankingClient.getAllRankings().then((response) => {
                this.updateRankings(response);
            });
        } else {
            rankingClient.getRankingsForSeason(season).then((response) => {
                this.updateRankings(response);
            });
        }
    }

    updateRankings(rankings: Array<RankingDto>) {
        this.rankings = []
        this.rankings.push(...rankings);
    }
}

export const rankingStore = new RankingStore();