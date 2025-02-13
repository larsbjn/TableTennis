import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import 'bootstrap/dist/css/bootstrap.min.css';
import Navigation from "@/components/navigation/navigation";
import React from "react";
import NewsTicker from "@/components/news-banner/news-ticker";
import "./global.scss";
import KeepAlive from "@/components/keep-alive";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Table tennis",
  description: "A tennis table scoring app",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body data-bs-theme={"dark"} className={`${geistSans.variable} ${geistMono.variable}`}>
        <Navigation />
        <div className={"content-container"}>
        {children}
        </div>
        <NewsTicker />
        <KeepAlive />
      </body>
    </html>
  );
}
