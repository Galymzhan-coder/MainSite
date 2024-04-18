import { type Route } from "next";
import { REPOSITORY_URL } from "~/app";

export const navItems = {
  mainNav: [
    {
      title: "Автомобиль",
      subcategories: [
        {
          title: "КАСКО Экспресс",
          slug: "#",
        },
        {
          title: "КАСКО",
          slug: "#",
        },
        {
          title: "ОС ГПО ВТС",
          slug: "#",
        },
        {
          title: "ДС ГПО ВАТ",
          slug: "#",
        },
        {
          title: "ОС ГПО ППП",
          slug: "#",
        },
        {
          title: "200 тысяч Плюс",
          slug: "#",
        },
      ],
    },
    {
      title: "Путешествия",
      image: "/images/clothing-one.webp",
      subcategories: [
        {
          title: "Страхование выезжающих за рубеж",
          slug: "#",
        },
      ],
    },
    {
      title: "Здоровье",
      subcategories: [
        {
          title: "Страхование от несчастных случаев",
          slug: "low-tops",
        },
        {
          title: "Медицинская страховка для частных лиц",
          slug: "high-tops",
        },
        {
          title:
            "Медицинское страхование для трудовых мигрантов и нерезидентов",
          slug: "slip-ons",
        },
      ],
    },
    {
      title: "Имущество",
      subcategories: [
        {
          title: "Классическое страхование имущества",
          slug: "skate-tools",
        },
        {
          title: "Экспресс-страхование имущества",
          slug: "bushings",
        },
        {
          title: " + Ваша ответственность перед соседями",
          slug: "shock-riser-pads",
        },
        {
          title: "Экспресс-страхование имущества (new)",
          slug: "skate-rails",
        },
      ],
    },
    {
      title: "Для бизнеса",
      subcategories: [
        {
          title: "Страхование ТМЦ",
          slug: "skate-tools",
        },
        {
          title: "Медстрахование сотрудников",
          slug: "bushings",
        },
        {
          title: "Страхование грузов",
          slug: "shock-riser-pads",
        },
        {
          title: "Железнодорожный транспорт",
          slug: "skate-rails",
        },
        {
          title: "Воздушный транспорт",
          slug: "wax",
        },
        {
          title: "Водный транспорт",
          slug: "socks",
        },
        {
          title: "Нефтяные операции",
          slug: "backpacks",
        },
      ],
    },
  ],
  sidebarNav: [
    {
      id: "tools",
      title: "Features",
      items: [
        {
          id: "todo",
          title: "Todo List",
          href: "/features/todo",
          items: [],
        },
      ],
    },
  ],
} as const;

export const navLinks = [
  {
    route: "Home",
    path: `/` as Route,
  },
  {
    route: "Features",
    path: "/#features",
  },
  {
    route: "Contact",
    path: "/contact",
  },
];
