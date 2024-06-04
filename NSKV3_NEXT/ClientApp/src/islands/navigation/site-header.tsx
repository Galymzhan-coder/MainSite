import "/node_modules/flag-icons/css/flag-icons.min.css";

import { currentUser } from "@clerk/nextjs";
import { settings, siteConfig } from "~/app";
import { env } from "~/env.mjs";
import { type Session } from "next-auth";
import { tv, type VariantProps } from "tailwind-variants";

import { Icons } from "~/islands/icons";
import { MainMenu } from "~/islands/navigation/main-menu";
import { MobileMenu } from "~/islands/navigation/mobile-menu";
import LocaleSwitcher from "~/islands/switchers/localization-main-switcher";
import { getCurrentUser } from "~/utils/users";

import { ButtonPlaceholder } from "../placeholders";
import UserMenu from "./user-menu";

const NavbarStyles = tv({
  base: "w-full border-b border-transparent bg-background/95 backdrop-blur-sm",
  variants: {
    border: {
      true: "border-border",
    },
    sticky: {
      true: "sticky top-0 z-40",
    },
    animated: {
      true: "animate-in fade-in slide-in-from-top-full duration-slow",
    },
  },
});

export type SiteHeaderProps = {
  session?: Session | null;
} & VariantProps<typeof NavbarStyles>;

export async function SiteHeader({
  border = true,
  sticky = true,
}: SiteHeaderProps) {
  let session: any;
  if (env.NEXT_PUBLIC_AUTH_PROVIDER === "clerk") {
    session = await currentUser();
  } else if (env.NEXT_PUBLIC_AUTH_PROVIDER === "authjs") {
    session = await getCurrentUser();
  } else {
    throw new Error("❌ [SiteHeader] authProvider is not defined");
  }

  const mainNav = [
    {
      title: "Калькуляторы",
      items: [
        {
          title: "КАСКО",
          href: "#",
          description: "",
          items: [],
        },
        {
          title: "Каско ЭКСПРЕСС",
          href: "#",
          description: "",
          items: [],
        },
        {
          title: "ОГПО",
          href: "#",
          description: "",
          items: [],
        },
        {
          title: "ДС ГПО автовладельцев",
          href: "#",
          description: "",
          items: [],
        },
        {
          title: "Недвижимость",
          href: "/blog",
          description: "",
          items: [],
        },
        {
          title: "Туризм",
          href: "#",
          description: "",
          items: [],
        },
        {
          title: "Несчастный случай",
          href: "#",
          description: "",
          items: [],
        },
        {
          title: "Медицинское страхование",
          href: "#",
          description: "",
          items: [],
        },
        {
          title: "ОГПО ППП",
          href: "#",
          description: "",
          items: [],
        },
      ],
    },
    {
      title: "ЕСБД",
      href: "https://id.mkb.kz/#/esbd/checkInsurancePolicy",
    },
    {
      title: "Проверить полис",
      href: "#",
    },
    {
      title: "Контакты",
      href: "#",
    },
  ];

  return (
    <header className={NavbarStyles({ border, sticky })}>
      <nav className="container flex justify-between h-16 items-center">
        <MainMenu items={mainNav} />
        <div className="flex flex-1 items-center justify-end space-x-2 gap-2">
          <a className="flex items-center gap-2" href="tel:2258">
            <span className="flex">
              <Icons.phone className="-mr-2 h-6 w-6" />
              <span className="relative flex h-2 w-2">
                <span className="animate-ping absolute inline-flex h-full w-full rounded-full bg-sky-400 opacity-75"></span>
                <span className="relative inline-flex rounded-full h-2 w-2 bg-sky-500"></span>
              </span>
            </span>
            <span className="text-2xl">2258</span>
            <span className="font-light">-</span>
            <span className="font-light">бесплатно</span>
          </a>
          {env.NEXT_PUBLIC_AUTH_PROVIDER === "clerk" ? (
            <UserMenu session={session} />
          ) : env.NEXT_PUBLIC_AUTH_PROVIDER === "authjs" ? (
            <>
              {(env.GITHUB_CLIENT_ID && env.GITHUB_CLIENT_SECRET) ||
              (env.GOOGLE_CLIENT_ID && env.GOOGLE_CLIENT_SECRET) ||
              (env.DISCORD_CLIENT_ID && env.DISCORD_CLIENT_SECRET) ? (
                <UserMenu session={session} />
              ) : (
                <ButtonPlaceholder text="Личный кабинет" />
              )}
            </>
          ) : null}
        </div>
      </nav>
    </header>
  );
}
