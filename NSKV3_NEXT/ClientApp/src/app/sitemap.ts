import { type MetadataRoute } from "next";
import { absoluteUrl } from "~/utils";

export default async function sitemap(): Promise<MetadataRoute.Sitemap> {
  const routes = [
    "",
    "/blog",
    "/custom/clothing",
    "/dashboard/account",
    "/dashboard/billing",
    "/dashboard/purchases",
    "/dashboard/settings",
    "/dashboard/stores",
    "/products",
    "/stores",
  ].map((route) => ({
    url: absoluteUrl(route),
    lastModified: new Date().toISOString(),
  }));

  return routes;
}
