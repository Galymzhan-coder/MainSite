/**
 * TypeScript Project Types
 * ========================
 *
 * Each of these types represents a distinct aspect of the app categories, such as user management,
 * subscription services, API interactions, UI components, and store/product management.
 * This categorization can help in better organizing the types and understanding
 * their roles within the application's structure.
 */

import { NextResponse, type NextRequest } from "next/server";
import { siteConfig } from "~/app";
import { type FileWithPath } from "react-dropzone";
import { type z, type ZodIssue } from "zod";

import { Network } from "~/server/config/socials";
import { type accounts, type Store } from "~/data/db/schema";
import { type userPrivateMetadataSchema } from "~/data/validations/auth";
import type {
  cartItemSchema,
  cartLineItemSchema,
  checkoutItemSchema,
} from "~/data/validations/cart";
import { type Icons } from "~/islands/icons";

/* User-Related Types */

export type PlanProps = "starter" | "professional" | "enterprise";

export type UserProps = {
  id: string;
  name?: string;
  email?: string;
  image?: string;
  plan: PlanProps;
  createdAt?: Date;
  usage?: number; // how many stores the user has created
  usageLimit?: number; // how many stores the user can create
  billingCycleStart?: number;
  stores?: { storeId: string }[];
  role?: "admin" | "seller" | "buyer";
};

export type SellerProps = {
  id: string;
  name?: string;
  slug?: string;
  logo?: string;
  createdAt?: Date;
  domains?: { slug: string }[];
  users?: { role: "admin" | "seller" | "buyer" }[];
};

export type UserRole = z.infer<typeof userPrivateMetadataSchema.shape.role>;

/* Subscription and Plan-Related Types */

export type SubscriptionPlanTypes = {
  id: "starter" | "professional" | "enterprise";
  name: string;
  description: string;
  features: string[];
  price: number;
};

export type UserSubscriptionPlan = SubscriptionPlanTypes & {
  isSubscribed: boolean;
  isCanceled: boolean;
  isActive: boolean;
};

/* API Response and Data Handling Types */

export type ApiResponseError = {
  ok: false;
  error: string;
  issues?: ZodIssue[];
};

export type ApiResponseSuccess<T> = {
  ok: true;
  data: T;
};

export type ApiResponse<T> = ApiResponseSuccess<T> | ApiResponseError;

/* Next.js Context and Route Handler Types */

export type NextRequestContext<T> = {
  params: T;
};

export type NextRouteContext<T = undefined> = { params: T };

export type NextRouteHandler<T = void, U = NextRouteContext> = (
  request: NextRequest,
  context: U,
) => NextResponse<T> | Promise<NextResponse<T>>;

/* UI Component and Layout Types */

export type WithChildren<T = unknown> = T & { children: React.ReactNode };

export type NavItem = {
  title: string;
  href?: string;
  disabled?: boolean;
  external?: boolean;
  icon?: keyof typeof Icons;
  label?: string;
  description?: string;
};

export type NavItemWithChildren = NavItem & {
  items: NavItemWithChildren[];
};

export type NavItemWithOptionalChildren = NavItem & {
  items?: NavItemWithChildren[];
};

export type FooterItem = {
  title: string;
  items: {
    title: string;
    href: string;
    external?: boolean;
  }[];
}[];

export type MainMenuItem = NavItemWithOptionalChildren;

export type SidebarNavItem = NavItemWithChildren;

export type GeneralShellParams = { header?: React.ReactNode };

export type LocaleLayoutParams = { params: { locale: string } };

export type SiteConfig = typeof siteConfig;

export type Config = {
  social: Network[];
  name: string;
};

export type ContactConfig = {
  email: string;
};

export type Settings = {
  themeToggleEnabled: boolean;
};

export type Layout = {
  featureCards: string;
  headers: {
    featureCards: string;
    features: string;
  };
};

export type Content = {
  text: string;
  subtext: string;
  image?: string;
};

export type ContentSection = {
  header: string;
  subheader: string;
  image?: string;
  content: Array<Content>;
};

/* Store and Product-Related Types */

export type Option = {
  label: string;
  value: string;
  icon?: React.ComponentType<{ className?: string }>;
};

export type FileWithPreview = FileWithPath & {
  preview: string;
};

export type StoredFile = {
  id: string;
  name: string;
  url: string;
};

export type DataTableSearchableColumn<TData> = {
  id: keyof TData;
  title: string;
};

export type DataTableFilterableColumn<TData> =
  DataTableSearchableColumn<TData> & {
    options: Option[];
  };

export type CuratedStore = {
  id: Store["id"];
  name: Store["name"];
  description?: Store["description"];
  productCount?: number;
};

export type CartItem = z.infer<typeof cartItemSchema>;

export type CheckoutItem = z.infer<typeof checkoutItemSchema>;

export type CartLineItem = z.infer<typeof cartLineItemSchema>;
