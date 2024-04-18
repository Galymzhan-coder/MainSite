/**
 * This module provides utilities for managing navigation and
 * locale information in Next.js application using next-intl.
 *
 * @see https://next-intl-docs.vercel.app/docs/routing/navigation
 * @see https://github.com/meienberger/runtipi/blob/develop/src/shared/internationalization/locales.ts
 */

import {
  createLocalizedPathnamesNavigation,
  createSharedPathnamesNavigation,
  Pathnames,
} from "next-intl/navigation";

// Default locale for the application.
// export const defaultLocale = "en-us";
export const defaultLocale = "ru" as const;

// Supported locales.
export const locales = [
  "ru",
  "kz",
] as const;
// ];

// Labels for each supported locale, used for displaying human-readable names.
export const labels = {
  ru: "Русский",
  kz: "Қазақша",
} as const;

// Type representing valid locale strings.
export type Locale = (typeof locales)[number];

// Ensure every locale has a label.
if (process.env.NODE_ENV === "development") {
  locales.forEach((locale) => {
    if (!labels[locale]) {
      console.warn(`No label found for locale: ${locale}`);
    }
  });
}

// Navigation utilities configured for the defined locales.
export const { Link, redirect, usePathname, useRouter } =
  createSharedPathnamesNavigation({ locales });

// === NEXT-INTERNATIONAL ===

/* export const localesNI = {
  "en-us": () => import("~/data/i18n/en-us.json"),
  "uk-ua": () => import("~/data/i18n/uk-ua.json"),
} as const;

type LocalesKeys = keyof typeof locales;

export const localeListNI = Object.keys(locales) as LocalesKeys[];

const allLocales = {
  "en-us": "en-us",
  "uk-ua": "uk-ua",
} as const;

export const LOCALES_NI: typeof allLocales = allLocales;

export const defaultLocaleNI = "en-us" as const satisfies keyof typeof localesNI; */
