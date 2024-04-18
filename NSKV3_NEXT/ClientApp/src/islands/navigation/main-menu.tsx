"use client";

import * as React from "react";
import { settings, siteConfig } from "~/app";
import { Link } from "~/navigation";
import { type MainMenuItem } from "~/types";
import { cn } from "~/utils";
import { ActivitySquare } from "lucide-react";

import { Icons } from "~/islands/icons";
import { Combobox } from "~/islands/navigation/combobox";
import {
  NavigationMenu,
  NavigationMenuContent,
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
  NavigationMenuTrigger,
  navigationMenuTriggerStyle,
} from "~/islands/navigation/nav-menu";
import LocaleSwitcher from "~/islands/switchers/localization-main-switcher";
import { ThemesGeneralSwitcher } from "~/islands/switchers/themes-general-switcher";

interface MainMenuProps {
  items?: MainMenuItem[];
}

export function MainMenu({ items }: MainMenuProps) {
  return (
    <div className="gap-6 hidden md:flex">
      <Link
        aria-label="Home"
        href="/"
        className="items-center space-x-2 lg:flex"
      >
        <Icons.nsk />
      </Link>
      {settings.themeToggleEnabled && <ThemesGeneralSwitcher />}
      {settings.internationalizationEnabled && <LocaleSwitcher />}
      <Combobox />
      <NavigationMenu>
        <NavigationMenuList>
          {items?.map((item) =>
            item?.items ? (
              <NavigationMenuItem key={item.title}>
                <NavigationMenuTrigger className="h-auto capitalize">
                  {item.title}
                </NavigationMenuTrigger>
                <NavigationMenuContent>
                  <ul className="grid w-[400px] gap-3 p-4 md:w-[500px] md:grid-cols-2 lg:w-[600px]">
                    {item.items.map((item) => (
                      <ListItem
                        key={item.title}
                        title={item.title}
                        href={item.href}
                      >
                        {item.description}
                      </ListItem>
                    ))}
                  </ul>
                </NavigationMenuContent>
              </NavigationMenuItem>
            ) : (
              item.href && (
                <NavigationMenuItem key={item.title}>
                  <Link href={item.href} legacyBehavior passHref>
                    <NavigationMenuLink
                      className={cn(
                        navigationMenuTriggerStyle(),
                        "font-heading h-auto",
                      )}
                    >
                      {item.title}
                    </NavigationMenuLink>
                  </Link>
                </NavigationMenuItem>
              )
            ),
          )}
        </NavigationMenuList>
      </NavigationMenu>
    </div>
  );
}

const ListItem = React.forwardRef<
  React.ElementRef<"a">,
  React.ComponentPropsWithoutRef<"a">
>(({ className, title, children, href, ...props }, ref) => {
  return (
    <li>
      <NavigationMenuLink asChild>
        <Link
          ref={ref}
          href={String(href)}
          className={cn(
            "block select-none space-y-1 rounded-md p-3 leading-none no-underline outline-none transition-colors hover:bg-accent hover:text-accent-foreground focus:bg-accent focus:text-accent-foreground",
            className,
          )}
          {...props}
        >
          <div className="text-sm font-medium leading-none">{title}</div>
          <p className="line-clamp-2 text-sm leading-snug text-muted-foreground">
            {children}
          </p>
        </Link>
      </NavigationMenuLink>
    </li>
  );
});
ListItem.displayName = "ListItem";
