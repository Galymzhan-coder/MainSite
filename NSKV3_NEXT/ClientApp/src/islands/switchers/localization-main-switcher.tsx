"use client";

import { useSearchParams } from "next/navigation";
import { labels, locales, usePathname, useRouter } from "~/navigation";
import { useLocale, useTranslations } from "next-intl";

import { useIsClient } from "~/hooks/use-is-client";
import { Button, type ButtonProps } from "~/islands/primitives/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuLabel,
  DropdownMenuRadioGroup,
  DropdownMenuRadioItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "~/islands/primitives/dropdown";

type LocalizationMainSwitcherProps = ButtonProps & {
  iconClassName?: string;
  className?: string;
};

export default function LocalizationMainSwitcher({
  iconClassName = "mr-2",
  className,
  ...props
}: LocalizationMainSwitcherProps) {
  const t = useTranslations("LanguageSwitch");
  const client = useIsClient();
  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();
  const locale = useLocale();

  if (!client)
    return (
      <Button
        disabled
        aria-label="Language Switcher"
        className="border rounded-md"
        variant="ghost"
        {...props}
      >
        {/* <span className="hidden sm:block">
          <LocaleFlagIcon locale={locale} />
          {labels[locale as keyof typeof labels]}
        </span> */}
        <span className="block -mr-2">
          <LocaleFlagIcon locale={locale} />
        </span>
      </Button>
    );

  const handleClick = (locale: string) => {
    router.replace(pathname + "?" + searchParams, {
      locale,
    });
  };

  return (
    <div>
      <DropdownMenu>
        <DropdownMenuTrigger asChild className="outline-none">
          <Button variant="outline">
            {/* <span className="hidden md:block">
              <span className="flex">
                <LocaleFlagIcon locale={locale} />
                {labels[locale as keyof typeof labels]}
              </span>
            </span> */}
            <LocaleFlagIcon locale={locale} />
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
          <DropdownMenuLabel className="font-heading">
            {t("choose-language")}
          </DropdownMenuLabel>
          <DropdownMenuSeparator />
          <DropdownMenuRadioGroup
            value={locale}
            onValueChange={(val) => handleClick(val)}
          >
            {locales.map((locale: string) => (
              <DropdownMenuRadioItem
                key={locale}
                value={locale}
                className="flex"
              >
                <LocaleFlagIcon locale={locale} />
                {labels[locale as keyof typeof labels]}
              </DropdownMenuRadioItem>
            ))}
          </DropdownMenuRadioGroup>
        </DropdownMenuContent>
      </DropdownMenu>
    </div>
  );
}

type LocaleFlagIconProps = { locale: string };

function LocaleFlagIcon({ locale }: LocaleFlagIconProps) {
  const baseLocale = locale.split("-")[0];

  return <span aria-hidden="true" className={`fi fi-${baseLocale}`}></span>;
}
