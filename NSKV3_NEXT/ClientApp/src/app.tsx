/**
 * [app.ts] App Configuration
 * ==========================
 *
 * To reduce the number of config files, we aim to
 * combine as much as possible into a single file.
 */

import { ContentSection, MainMenuItem, type FooterItem } from "~/types";

import { networks } from "~/server/config/socials";
import { Icons } from "~/islands/icons";

export const appts = {
  name: "NSK",
  debug: false,
  social: networks({
    youtube: "@bleverse_com",
    discord: "Pb8uKbwpsJ",
    facebook: "groups/bleverse",
    twitter: "arvisix",
    github: "arvisix",
  }),
};

export default appts;

const links = {};

export const contactConfig = {
  email: "arvisix@gmail.com",
};

export const REPOSITORY_OWNER = "arvisix";
export const REPOSITORY_NAME = "nsk";
export const REPOSITORY_URL = `https://github.com/${REPOSITORY_OWNER}/${REPOSITORY_NAME}`;
export const DISCORD_URL = "https://discord.gg/Pb8uKbwpsJ";
export const baseUrl = "https://nsk.kz";

export const BASE_URL =
  process.env.NODE_ENV === "production" ? baseUrl : "http://localhost:3000";
export const BRAND_NAME = "Nsk";
export const BRAND_DESCRIPTION =
  "Next.js 14 free store and dashboard template. It helps you build great eCommerce and SaaS apps faster than ever. Get it now!";

export const OWNER_ROLE = "owner";
export const ADMIN_ROLE = "admin";
export const MEMBER_ROLE = "member";

export const TRIAL_LENGTH_IN_DAYS = 7;
export const ROLES = [OWNER_ROLE, ADMIN_ROLE, MEMBER_ROLE] as const;

export const settings = {
  internationalizationEnabled: true,
  themeToggleEnabled: true,
};

export const siteConfig = {
  name: "Нефтяная страховая компания",
  shortName: "NSK",
  author: "Maxim Moroz",
  description: "",
  company: {
    name: "АО «Нефтяная страховая компания»",
    link: "https://github.com/arvisix",
    email: "arvisix@gmail.com",
    twitter: "@arvisix",
  },
  handles: {
    twitter: "@arvisix",
  },
  keywords: [],
  url: {
    base: baseUrl,
    author: REPOSITORY_OWNER,
  },
  ogImage: `${baseUrl}/og-image.png`,
  links,
  footerNav: [
    [
      {
        title: "О компании",
        items: [
          {
            title: "О нас",
            href: "",
            external: false,
          },
          {
            title: "Руководство",
            href: "",
            external: false,
          },
          {
            title: "Клиенты и партнеры",
            href: "",
            external: false,
          },
          {
            title: "Финансовая отчетность",
            href: "",
            external: false,
          },
          {
            title: "Лицензии и сертификаты",
            href: "",
            external: false,
          },
          {
            title: "Вакансии",
            href: "",
            external: false,
          },
          {
            title: "Контакты и реквизиты",
            href: "",
            external: false,
          },
          {
            title: "Новости",
            href: "",
            external: false,
          },
          {
            title: "Правила страхования",
            href: "/o-kompanii/pravila-strahovaniya",
            external: false,
          },
        ],
      },
    ],
    [
      {
        title: "Частным клиентам",
        items: [
          {
            title: "Автострахование",
            href: "#",
            external: false,
          },
          {
            title: "Путешествие",
            href: "#",
            external: false,
          },
          {
            title: "Здоровье",
            href: "#",
            external: false,
          },
          {
            title: "Имущество",
            href: "#",
            external: false,
          },
        ],
      },
      {
        title: "Корпоративным клиентам",
        items: [
          {
            title: "Обязательное страхование по отраслям",
            href: "#",
            external: true,
          },
          {
            title: "Добровольные виды страхования",
            href: "#",
            external: true,
          },
        ],
      },
    ],
    [
      {
        title: "Поддержка клиентов",
        items: [
          {
            title: "Часто задаваемые вопросы",
            href: "#",
            external: true,
          },
          {
            title: "Оценить качество обслуживания",
            href: "#",
            external: true,
          },
          {
            title: "Оплата страхового взноса",
            href: "#",
            external: true,
          },
          {
            title: "Страховой случай",
            href: "#",
            external: true,
          },
          {
            title: "Заявления и правила",
            href: "#",
            external: true,
          },
        ],
      },
    ],
  ] satisfies FooterItem[],
};
