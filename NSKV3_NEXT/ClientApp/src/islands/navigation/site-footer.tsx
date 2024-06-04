import { siteConfig } from "~/app";
import { Link } from "~/navigation";
import { cn } from "~/utils";
import {
  Facebook,
  Instagram,
  Linkedin,
  Mail,
  Phone,
  Pin,
  Twitter,
} from "lucide-react";

import { buttonVariants } from "~/islands/primitives/button";
import { Shell } from "~/islands/wrappers/shell-variants";

export async function SiteFooter() {
  return (
    <footer className="w-full border-t bg-gradient-to-t from-blue-200 via-blue-500 to-blue-800">
      <Shell
        as="div"
        id="footer-content"
        aria-labelledby="footer-content-heading"
        className="flex flex-col items-center gap-10 lg:flex-row lg:items-center lg:justify-center"
      >
        <section
          id="footer-links"
          aria-labelledby="footer-links-heading"
          className="grid grid-cols-3 gap-2"
        >
          {siteConfig.footerNav.map((column, index) => (
            <div key={index}>
              {column.map((subColumn) => (
                <div key={subColumn.title} className="mb-4">
                  <h4 className="text-white font-medium mb-2 text-lg">
                    {subColumn.title}
                  </h4>
                  <ul className="space-y-3">
                    {subColumn.items.map((link) => (
                      <li key={link.title}>
                        <Link
                          href={link.href}
                          target={link?.external ? "_blank" : undefined}
                          rel={link?.external ? "noreferrer" : undefined}
                          className="text-sm text-gray-200 transition-colors hover:text-foreground"
                        >
                          {link.title}
                          <span className="sr-only">{link.title}</span>
                        </Link>
                      </li>
                    ))}
                  </ul>
                </div>
              ))}
            </div>
          ))}
        </section>

        <section
          id="newsletter"
          aria-labelledby="newsletter-heading"
          className="space-y-3 flex flex-col items-start self-start"
        >
          <div className="flex items-center gap-2 text-2xl cursor-pointer group text-white">
            <Mail className="h-6 w-6" aria-hidden="true" />
            <h2 className="group-hover:text-primary">Написать нам</h2>
          </div>
          <div className="flex items-center gap-2 text-2xl cursor-pointer group text-white">
            <Phone className="h-6 w-6 " aria-hidden="true" />
            <h2 className="group-hover:text-primary">2258</h2>
          </div>
          <div className="flex items-center gap-2 text-2xl cursor-pointer group text-white">
            <Pin className="h-6 w-6" aria-hidden="true" />
            <h2 className="group-hover:text-primary">Офисы на карте</h2>
          </div>
          <div className="flex items-center space-x-1 text-white">
            <Link
              href="#"
              target="_blank"
              rel="noreferrer"
              className={cn(
                buttonVariants({
                  size: "icon",
                  variant: "ghost",
                }),
              )}
            >
              <Facebook className="h-4 w-4" aria-hidden="true" />
              <span className="sr-only">GitHub</span>
            </Link>
            <Link
              href="#"
              target="_blank"
              rel="noreferrer"
              className={cn(
                buttonVariants({
                  size: "icon",
                  variant: "ghost",
                }),
              )}
            >
              <Twitter className="h-4 w-4" aria-hidden="true" />
              <span className="sr-only">X (known as Twitter)</span>
            </Link>
            <Link
              href="#"
              target="_blank"
              rel="noreferrer"
              className={cn(
                buttonVariants({
                  size: "icon",
                  variant: "ghost",
                }),
              )}
            >
              <Instagram className="h-4 w-4" aria-hidden="true" />
              <span className="sr-only">GitHub</span>
            </Link>
            <Link
              href="#"
              target="_blank"
              rel="noreferrer"
              className={cn(
                buttonVariants({
                  size: "icon",
                  variant: "ghost",
                }),
              )}
            >
              <Linkedin className="h-4 w-4" aria-hidden="true" />
              <span className="sr-only">GitHub</span>
            </Link>
          </div>
          <div
            id="footer-copyright"
            aria-labelledby="footer-copyright-text"
            className="flex items-center space-x-4 justify-center lg:justify-start"
          >
            <div className="block text-sm text-white">
              © {new Date().getFullYear()}{" "}
              <Link
                target="_blank"
                href="https://www.nsk.kz/"
                className="hover:underline"
              >
                {siteConfig.company.name}
              </Link>
            </div>
          </div>
        </section>
      </Shell>
      <Shell
        as="div"
        id="footer-content"
        aria-labelledby="footer-content-heading"
        className="flex flex-col items-center gap-10 lg:flex-row lg:items-center lg:justify-center"
      >
        <section className="grid grid-cols-3">
          <div>
            <img
              src="https://www.nsk.kz//images/logo-fgsb.svg"
              className="w-[120px] h-[50px]"
            />
          </div>
          <div>
            <p className="text-sm text-slate-600">
              Общая сумма выплат за 24 года
            </p>
            <p className="text-2xl font-extrabold text-blue-900">
              36 946 608 050
            </p>
          </div>
          <div>
            <p className="text-sm text-slate-600">
              Рейтинг Standard & Poor's Global
            </p>
            <p className="text-2xl font-extrabold text-blue-900">
              B+ (прогноз стабильный) 13.09.2022
            </p>
          </div>
        </section>
      </Shell>
    </footer>
  );
}
