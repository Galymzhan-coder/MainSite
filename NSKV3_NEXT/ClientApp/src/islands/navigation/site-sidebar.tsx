"use client";

import { useState } from "react";
import Image from "next/image";
import Link from "next/link";
import { ChevronRightIcon } from "@radix-ui/react-icons";
import cn from "classnames";
import { useTranslations } from "next-intl";

import { useClickOutside } from "~/hooks/use-click-outside";
import { Icons } from "~/islands/icons";
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "~/islands/primitives/accordion";
import {
  Tooltip,
  TooltipContent,
  TooltipTrigger,
} from "~/islands/primitives/tooltip";

function TooltipSidebar({ title, photo, description, href }) {
  return (
    <Tooltip>
      <TooltipTrigger>
        <Link
          href={href}
          key={title}
          className="flex items-center w-full px-5 py-2 transition-colors duration-200 dark:hover:bg-gray-800 gap-x-2 hover:bg-gray-100 focus:outline-none"
        >
          <div className="text-left rtl:text-right">
            <h1 className="text-sm font-medium dark:text-white">{title}</h1>
          </div>
        </Link>
      </TooltipTrigger>
      <TooltipContent
        side="right"
        className="bg-white p-4 text-black flex flex-col gap-4 items-center border border-gray-200 dark:border-gray-800"
      >
        <Image src={photo} alt={title} width={160} height={160} />
        <h1 className="text-lg font-bold">{title}</h1>
        <h2>{description}</h2>
      </TooltipContent>
    </Tooltip>
  );
}

function AccordionSidebar({ title, children }) {
  return (
    <Accordion type="single" collapsible>
      <AccordionItem className="border-b-0" value={title} key={title}>
        <AccordionTrigger className="p-4 my-0 hover:bg-gray-100">
          {title}
        </AccordionTrigger>
        <AccordionContent>
          <div className="flex flex-col text-slate-500">{children}</div>
        </AccordionContent>
      </AccordionItem>
    </Accordion>
  );
}

export function SiteSidebar() {
  const [sidebarSelected, setSidebarSelected] = useState();
  const [sidebarSecondLevelSelected, setSidebarSecondLevelSelected] =
    useState(true);

  const t = useTranslations("SidebarNav");

  const sidebarNav = [
    {
      title: t("Автомобиль.заголовок"),
      Icon: <Icons.car className="h-6 w-6" />,
      subcategories: [
        {
          title: t("Автомобиль.КАСКО Экспресс"),
          href: "/avtomobil/kasko-express",
        },
        {
          title: t("Автомобиль.КАСКО"),
          href: "/avtomobil/kasko",
        },
        {
          title: t("Автомобиль.ОС ГПО ВТС"),
          href: "/avtomobil/os-gpo-vts",
        },
        {
          title: t("Автомобиль.ДС ГПО ВАТ"),
          href: "/avtomobil/ds-gpo-vat",
        },
        {
          title: t("Автомобиль.ОС ГПО ППП"),
          href: "/avtomobil/os-gpo-ppp",
        },
        {
          title: t("Автомобиль.200 тысяч Плюс"),
          href: "/avtomobil/200-tysyach-plyus",
        },
        {
          title: t("Автомобиль.Временный въезд"),
          href: "/avtomobil/vremennyj-vezd",
        },
      ],
    },
    {
      title: t("Путешествия.заголовок"),
      Icon: <Icons.palmtree className="h-6 w-6" />,
      subcategories: [
        {
          title: t("Путешествия.Страхование выезжающих за рубеж"),
          href: "/puteshestvie/strahovanie-vyezzhayushchih-za-rubezh",
        },
      ],
    },
    {
      title: t("Здоровье.заголовок"),
      Icon: <Icons.heartPulse className="h-6 w-6" />,
      subcategories: [
        {
          title: t("Здоровье.Страхование от несчастных случаев"),
          href: "/zdorove/strahovanie-ot-neschastnyh-sluchaev",
        },
        {
          title: t("Здоровье.Медицинская страховка для частных лиц"),
          href: "/zdorove/medicinskaya-strahovka-dlya-chastnyh-lic",
        },
        {
          title: t(
            "Здоровье.Медицинское страхование для трудовых мигрантов и нерезидентов",
          ),
          href: "/zdorove/medicinskoe-strahovanie-dlya-trudovyh-migrantov-i-nerezidentov",
        },
      ],
    },
    {
      title: t("Имущество.заголовок"),
      Icon: <Icons.home className="h-6 w-6" />,
      subcategories: [
        {
          title: t("Имущество.Классическое страхование имущества"),
          href: "/imushchestvo/klassicheskoe-strahovanie-imushchestva",
        },
        {
          title: t("Имущество.Экспресс-страхование имущества"),
          href: "/imushchestvo/ekspress-strahovanie-imushchestva",
        },
        {
          title: t("Имущество.+ Ваша ответственность перед соседями"),
          href: "/imushchestvo/vasha-otvetstvennost-pered-sosedyami",
        },
        {
          title: t("Имущество.Экспресс-страхование имущества (new)"),
          href: "/imushchestvo/ekspress-strahovanie-imushchestva-new",
        },
      ],
    },
    {
      title: t("Для бизнеса.заголовок"),
      Icon: <Icons.factory className="h-6 w-6" />,
      subcategories: [
        {
          title: t("Для бизнеса.Страхование ТМЦ"),
          href: "/dlya-biznesa/strahovanie-tmc",
        },
        {
          title: t("Для бизнеса.Медстрахование сотрудников"),
          href: "/dlya-biznesa/medstrahovanie-sotrudnikov",
        },
        {
          title: t("Для бизнеса.Страхование грузов"),
          href: "/dlya-biznesa/strahovanie-gruzov",
        },
        {
          title: t("Для бизнеса.Железнодорожный транспорт"),
          href: "/dlya-biznesa/zheleznodorozhnyj-transport",
        },
        {
          title: t("Для бизнеса.Воздушный транспорт"),
          href: "/dlya-biznesa/vozdushnyj-transport",
        },
        {
          title: t("Для бизнеса.Водный транспорт"),
          href: "/dlya-biznesa/vodnyj-transport",
        },
        {
          title: t("Для бизнеса.Нефтяные операции"),
          href: "/dlya-biznesa/neftyanye-operacii",
        },
      ],
    },
    {
      title: t("О компании.заголовок"),
      Icon: <Icons.building2 className="h-6 w-6" />,
      subcategories: [
        {
          href: "/o-kompanii/sovet-directorov",
          component: () => (
            <AccordionSidebar title="Совет директоров">
              <TooltipSidebar
                title="Альжанов Тлек Кабыкенович"
                photo="https://www.nsk.kz/media/top_management/yw/1570446256zuouh.jpg"
                description="Председатель Совета директоров АО «НСК»"
                href="/o-kompanii/sovet-directorov/Alzhanov-Tlek-Kabykenovich"
              />
              <TooltipSidebar
                title="Сартбаев Медет Максутович"
                photo="https://www.nsk.kz/media/top_management/zn/152313256asda.jpg"
                description="Независимый директор – Член Совета директоров АО «НСК»"
                href="/o-kompanii/sovet-directorov/Sartbaev-Medet-Maksutovich"
              />
              <TooltipSidebar
                title="Конурбаев Ержан Еркенович"
                photo="https://www.nsk.kz/media/top_management/l6/1676633741md5jv.jpg"
                description="Председатель Правления, член совета директоров АО «НСК»"
                href="/o-kompanii/sovet-directorov/Konurbaev-Erzhan-Erkenovich"
              />
            </AccordionSidebar>
          ),
        },
        {
          href: "/o-kompanii/pravlenie",
          component: () => (
            <AccordionSidebar title="Правление">
              <TooltipSidebar
                title="Конурбаев Ержан Еркенович"
                photo="https://www.nsk.kz/media/top_management/l6/1676633741md5jv.jpg"
                description="Председатель Правления, член совета директоров АО «НСК»"
                href="/o-kompanii/sovet-directorov/Konurbaev-Erzhan-Erkenovich"
              />
              <TooltipSidebar
                title="Солтанғажы Арман Солтанғажыұлы"
                photo="https://www.nsk.kz/media/top_management/1a/Dsc02301.jpg"
                description="Заместитель Председателя Правления АО «НСК»"
                href="/o-kompanii/pravlenie/Soltangajy-Arman-Soltangajyuly"
              />
              <TooltipSidebar
                title="Ледоховский Алексей Сергеевич"
                photo="https://www.nsk.kz/media/top_management/b1/1677820139iex6y.jpg"
                description="Заместитель Председателя Правления"
                href="/o-kompanii/pravlenie/Ledohovski-Aleksei-Sergeevich"
              />
              <TooltipSidebar
                title="Ермеков Газиз Ерланұлы"
                photo="https://www.nsk.kz/media/top_management/h9/16768804876cph3.jpg"
                description="Заместитель Председателя Правления"
                href="/o-kompanii/pravlenie/Ermekov-Gaziz-Erlanuly"
              />
              <TooltipSidebar
                title="Рахметов Максат Куатович"
                photo="https://www.nsk.kz/media/top_management/ub/1679917751aq4p4.jpg"
                description="Главный бухгалтер, член Правления"
                href="/o-kompanii/pravlenie/Rahmetov-Maksat-Kuatovich"
              />
            </AccordionSidebar>
          ),
        },
        {
          title: t("О компании.Миссия и ценности"),
          href: "/o-kompanii/missiya-i-cennosti",
        },
        {
          title: t("О компании.История"),
          href: "/o-kompanii/istoriya",
        },
        {
          title: t("О компании.Нам доверяют"),
          href: "/o-kompanii/nam-doveryayut",
        },
        {
          title: t("О компании.Карьера в НСК"),
          href: "/o-kompanii/karera-v-nsk",
        },
        {
          title: t("О компании.Отчетность"),
          href: "/o-kompanii/otchetnost",
        },
        {
          title: t("О компании.Лицензии"),
          href: "/o-kompanii/licenzii",
        },
        {
          title: t("О компании.Корпоративная информация"),
          href: "/o-kompanii/korporativnaya-informaciya",
        },
        {
          title: t("О компании.Сведения об акционерах"),
          href: "/o-kompanii/svedeniya-ob-akcionerah",
        },
      ],
    },
    {
      title: t("Информация.заголовок"),
      Icon: <Icons.info className="h-6 w-6" />,
      subcategories: [
        {
          title: t("Информация.Вакансии"),
          href: "/informaciya/vakansii",
        },
        {
          title: t("Информация.Корпоративный блог"),
          href: "/informaciya/korporativnyj-blog",
        },
        {
          title: t("Информация.Вопросы и ответы"),
          href: "/informaciya/voprosy-i-otvety",
        },
        {
          title: t("Информация.Проверить бонус-малус"),
          href: "/informaciya/proverit-bonus-malus",
        },
      ],
    },
    {
      title: t("Обратиться к нам.заголовок"),
      Icon: <Icons.messageCircle className="h-6 w-6" />,
      subcategories: [
        {
          title: t("Обратиться к нам.Служба качества"),
          href: "/obratitsya-k-nam/sluzhba-kachestva",
        },
        {
          title: t("Обратиться к нам.Страховой случай"),
          href: "/obratitsya-k-nam/strahovoj-sluchaj",
        },
        {
          title: t("Обратиться к нам.Продлить полис"),
          href: "/obratitsya-k-nam/prodlit-polis",
        },
        {
          title: t("Обратиться к нам.Контакты"),
          href: "/obratitsya-k-nam/kontakty",
        },
      ],
    },
  ];

  const handleClickOutside = () => {
    setSidebarSelected(undefined);
  };

  const ref = useClickOutside(handleClickOutside);

  const handleSidebarClick = (key) => {
    key === sidebarSelected
      ? setSidebarSelected(undefined)
      : setSidebarSelected(key);
  };

  const selectedSidebar = sidebarNav.find(
    (element) => element.title === sidebarSelected,
  );

  const handleSecondLevelSidebarHover = (data) => {
    setSidebarSecondLevelSelected(data);
  };

  return (
    <aside
      className="fixed z-30 hidden sm:flex bg-white dark:bg-black"
      ref={ref}
    >
      <div
        className={cn(
          "flex flex-col items-center pt-[100px] h-screen border group",
          {
            "w-16": !sidebarSelected,
          },
        )}
      >
        {sidebarNav.map(({ title, Icon }) => (
          <div
            className={cn(
              "flex gap-2 items-center w-full justify-between px-4 py-6 cursor-pointer focus:outline-nones transition-colors duration-200 rounded-lg  hover:bg-gray-100",
              {
                "text-primary bg-blue-100 dark:bg-gray-500 dark:text-white":
                  sidebarSelected === title,
              },
            )}
            key={title}
            onMouseEnter={() => handleSidebarClick(title)}
          >
            <div
              className={cn("self-left", {
                block: sidebarSelected,
                hidden: !sidebarSelected,
              })}
            >
              {title}
            </div>
            <div className="ml-auto">{Icon}</div>
          </div>
        ))}
      </div>

      {selectedSidebar && (
        <SidebarExtended
          {...selectedSidebar}
          handleSidebarClick={handleSidebarClick}
          sidebarSecondLevelSelected={sidebarSecondLevelSelected}
          handleSecondLevelSidebarHover={handleSecondLevelSidebarHover}
        />
      )}
    </aside>
  );
}

function SidebarExtended({
  title,
  subcategories,
  sidebarSecondLevelSelected,
  handleSecondLevelSidebarHover,
  handleSidebarClick,
}) {
  return (
    <div className="flex">
      <div className="h-screen pt-[100px] overflow-y-auto border-l border-r sm:w-64 w-60">
        <h2 className="px-5 text-lg">{title}</h2>
        <div className="mt-8">
          {subcategories.map((subcategory) =>
            subcategory.component ? (
              subcategory.component()
            ) : (
              <Link
                key={subcategory.title}
                href={subcategory.href}
                onClick={() => handleSidebarClick(undefined)}
                onMouseEnter={() =>
                  handleSecondLevelSidebarHover(subcategory.subcategories)
                }
                className="flex px-4 py-4 cursor-pointer focus:outline-nones transition-colors duration-200 rounded-lg  hover:bg-gray-100"
              >
                <div className="text-left rtl:text-right">
                  <h1 className="text-sm font-medium dark:text-white">
                    {subcategory.title}
                  </h1>
                </div>
                {subcategory.subcategories?.length && <ChevronRightIcon />}
              </Link>
            ),
          )}
        </div>
      </div>
      {sidebarSecondLevelSelected?.length > 0 && (
        <div className="h-screen pt-[100px] overflow-y-auto border-l border-r sm:w-64 w-60">
          <div className="mt-8 space-y-4">
            {sidebarSecondLevelSelected.map((subcategory) =>
              subcategory.component ? (
                subcategory.component()
              ) : (
                <button
                  key={subcategory.title}
                  className="flex items-center w-full px-5 py-2 transition-colors duration-200 dark:hover:bg-gray-800 gap-x-2 hover:bg-gray-100 focus:outline-none"
                >
                  <div className="text-left rtl:text-right">
                    <h1 className="text-sm font-medium dark:text-white">
                      {subcategory.title}
                    </h1>
                  </div>
                </button>
              ),
            )}
          </div>
        </div>
      )}
    </div>
  );
}
