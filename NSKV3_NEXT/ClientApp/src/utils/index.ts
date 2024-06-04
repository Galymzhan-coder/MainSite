/**
 * @see https://github.com/sadmann7/skateshop/blob/main/src/lib/utils.ts
 * @see https://github.com/steven-tey/dub/blob/main/packages/utils/src/index.ts
 */

import { isClerkAPIResponseError, useSignIn } from "@clerk/nextjs";
import { type OAuthStrategy } from "@clerk/types";
import { env } from "~/env.mjs";
import { type CartLineItem } from "~/types";
import { clsx, type ClassValue } from "clsx";
import dayjs from "dayjs";
import { toast } from "react-hot-toast";
import { twMerge } from "tailwind-merge";
import * as z from "zod";

/**
 * `clsx` is a tiny utility for constructing className strings conditionally.
 * `cn` function is a small extension for `clsx` to implement better support for TailwindCSS classes.
 */
export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}
export const cls = cn;

export const ERR = {
  unauthenticated: "Unauthenticated",
  unauthorized: "Unauthorized",
  db: "Failed to find in database",
  undefined: "Undefined variable",
  fetch: "Failed to fetch data",
  not_allowed: "User should not be allowed to do this action",
};

export function formatDate(date: Date | string | number) {
  return dayjs(date).format("MMMM D, YYYY");
}

export function formatBytes(
  bytes: number,
  decimals = 0,
  sizeType: "accurate" | "normal" = "normal",
) {
  const sizes = ["Bytes", "KB", "MB", "GB", "TB"];
  const accurateSizes = ["Bytes", "KiB", "MiB", "GiB", "TiB"];
  if (bytes === 0) return "0 Byte";
  const i = Math.floor(Math.log(bytes) / Math.log(1024));
  return `${(bytes / Math.pow(1024, i)).toFixed(decimals)} ${
    sizeType === "accurate" ? accurateSizes[i] ?? "Bytest" : sizes[i] ?? "Bytes"
  }`;
}

export function slugify(str: string) {
  return str
    .toLowerCase()
    .replace(/ /g, "-")
    .replace(/[^\w-]+/g, "")
    .replace(/--+/g, "-");
}

export function unslugify(str: string) {
  return str.replace(/-/g, " ");
}

export function toTitleCase(str: string) {
  return str.replace(
    /\w\S*/g,
    (txt) => txt.charAt(0).toUpperCase() + txt.slice(1).toLowerCase(),
  );
}

export function toSentenceCase(str: string) {
  return str
    .replace(/([A-Z])/g, " $1")
    .replace(/^./, (str) => str.toUpperCase());
}

export function truncate(str: string, length: number) {
  return str.length > length ? `${str.substring(0, length)}...` : str;
}

export function isArrayOfFile(files: unknown): files is File[] {
  const isArray = Array.isArray(files);
  if (!isArray) return false;
  return files.every((file) => file instanceof File);
}

export function absoluteUrl(path: string) {
  return `${env.NEXT_PUBLIC_APP_URL}${path}`;
}

export function catchError(err: unknown) {
  if (err instanceof z.ZodError) {
    const errors = err.issues.map((issue) => {
      return issue.message;
    });
    return toast(errors.join("\n"));
  } else if (err instanceof Error) {
    return toast(err.message);
  } else {
    return toast("Something went wrong, please try again later.");
  }
}

export function catchClerkError(err: unknown) {
  const unknownErr = "Something went wrong, please try again later.";

  if (err instanceof z.ZodError) {
    const errors = err.issues.map((issue) => {
      return issue.message;
    });
    return toast(errors.join("\n"));
  } else if (isClerkAPIResponseError(err)) {
    return toast.error(err.errors[0]?.longMessage ?? unknownErr);
  } else {
    return toast.error(unknownErr);
  }
}

/**
 * @see https://github.com/nextauthjs/next-auth/blob/main/docs/docs/guides/basics/pages.md?plain=1#L47
 */
export function catchAuthError(error?: string | null) {
  switch (error) {
    case "OAuthAccountNotLinked":
      return {
        title: "You already have an account",
        description:
          "Please sign in with the other service you used to sign up.",
      };
    case "EmailSignin":
      return {
        title: "Unable to send login e-mail",
        description: "Sending your login e-mail failed. Please try again.",
      };
    case "CredentialsSignin":
      return {
        title: "Invalid username or password",
        description:
          "The username and password you entered did not match our records. Please double-check and try again.",
      };
    case "SessionRequired":
      return {
        title: "Login required",
        description: "You must be logged in to view this page",
      };
    case "OAuthCallback":
    case "OAuthCreateAccount":
    case "OAuthSignin":
    case "EmailCreateAccount":
    case "Callback":
    case "Default":
    default:
      return {
        title: "Something went wrong.",
        description: "Your sign in request failed. Please try again.",
      };
  }
}

export function isMacOs() {
  if (typeof window === "undefined") return false;

  return window.navigator.userAgent.includes("Mac");
}

export function formatPrice(
  price: number | string,
  options: {
    currency?: "USD" | "EUR" | "GBP" | "BDT";
    notation?: Intl.NumberFormatOptions["notation"];
  } = {},
) {
  const { currency = "USD", notation = "compact" } = options;

  return new Intl.NumberFormat("ru", {
    style: "currency",
    currency,
    notation,
  }).format(Number(price));
}

export const numberToSI = (number: any) => {
  const SI_SYMBOL = ["", "k", "M", "B", "T"];

  const tier = (Math.log10(Math.abs(number)) / 3) | 0;

  if (tier == 0) return number;

  const suffix = SI_SYMBOL[tier];
  const scale = Math.pow(10, tier * 3);

  const scaled = number / scale;

  return scaled.toFixed(1) + suffix;
};

export const numberToMoney = (number: any) => {
  const parsedNumber = parseFloat(number);

  if (isNaN(parsedNumber)) return "--";

  const money = new Intl.NumberFormat("ru", {
    style: "currency",
    currency: "USD",
  });

  return money.format(parsedNumber);
};

export const numberToSIMoney = (number: any) => {
  const parsedNumber = parseFloat(number);

  if (isNaN(parsedNumber)) return "--";

  const money = new Intl.NumberFormat("ru", {
    style: "currency",
    currency: "USD",
    notation: "compact",
  });

  return money.format(parsedNumber);
};

export const numberToPercent = (number: any) => {
  const percent = new Intl.NumberFormat("ru", {
    style: "percent",
    maximumSignificantDigits: 2,
  });

  return percent.format(number / 100);
};
