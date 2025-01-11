import {MatchesClient, NewsClient, RankingsClient, RulesClient, UsersClient} from "@/api-client";
const url = process.env.NEXT_PUBLIC_API_URL || "https://localhost:7116";

if (process.env.NODE_ENV === 'development') {
    process.env.NODE_TLS_REJECT_UNAUTHORIZED = "0";
}

export const userClient = new UsersClient(url, {
    fetch: (url, init) => fetch(url, init)
});
export const matchClient = new MatchesClient(url, {
    fetch: (url, init) => fetch(url, init)
});

export const rankingClient = new RankingsClient(url, {
    fetch: (url, init) => fetch(url, init)
});

export const newsClient = new NewsClient(url, {
    fetch: (url, init) => fetch(url, init)
});

export const ruleClient = new RulesClient(url, {
    fetch: (url, init) => fetch(url, init)
});
