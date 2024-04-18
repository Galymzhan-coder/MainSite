"use client";

import { useTranslations } from "next-intl";
import { Balancer } from "react-wrap-balancer";

export function HeroSection() {
  const t = useTranslations("landing");

  return (
    <>
      <Balancer
        as="h1"
        className="text-3xl font-heading mt-4 leading-[1.1] sm:text-3xl md:text-4xl tracking-tighter bg-gradient-to-br from-primary/60 dark:from-zinc-400 from-10% via-primary/90 dark:via-zinc-300 via-30% to-primary dark:to-zinc-600 to-90% bg-clip-text text-transparent"
      >
        <span className="block max-w-5xl">{t("heading")}</span>
      </Balancer>
    </>
  );
}
