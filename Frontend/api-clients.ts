import {MatchClient, RankingClient, UserClient} from "@/api-client";
const url = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7116";

if (process.env.NODE_ENV === 'development') {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
}

export const userClient = new UserClient(url, {
    fetch: (url, init) => fetch(url, init)
});
export const matchClient = new MatchClient(url, {
    fetch: (url, init) => fetch(url, init)
});

export const rankingClient = new RankingClient(url, {
    fetch: (url, init) => fetch(url, init)
});
