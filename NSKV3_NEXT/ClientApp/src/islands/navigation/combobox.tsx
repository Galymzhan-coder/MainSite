"use client";

import {
  Fragment,
  useCallback,
  useEffect,
  useState,
  useTransition,
} from "react";
import { useRouter } from "next/navigation";
import { cn } from "~/utils";
import { Circle, File, Laptop, Moon, Sun } from "lucide-react";
import { useTranslations } from "next-intl";
import { useTheme } from "next-themes";

import { navItems } from "~/server/links";
import { type Product } from "~/data/db/schema";
import { useDebounce } from "~/hooks/use-debounce";
import { useHotkeys } from "~/hooks/use-hotkeys";
import { Icons } from "~/islands/icons";
import { Button } from "~/islands/primitives/button";
import {
  CommandDialog,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
  CommandSeparator,
} from "~/islands/primitives/command";
import { Skeleton } from "~/islands/primitives/skeleton";

type RouteHref = never;

export function Combobox() {
  const t = useTranslations("islands");

  const router = useRouter();
  const [isOpen, setIsOpen] = useState(false);
  const [query, setQuery] = useState("");
  const debouncedQuery = useDebounce(query, 300);
  const [data, setData] = useState<
    | {
        category: Product["category"];
        products: Pick<Product, "id" | "name" | "category">[];
      }[]
    | null
  >(null);
  const [isPending, startTransition] = useTransition();

  useEffect(() => {
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.key === "k" && (e.metaKey || e.ctrlKey)) {
        e.preventDefault();
        setIsOpen((isOpen) => !isOpen);
      }
    };
    window.addEventListener("keydown", handleKeyDown);
    return () => window.removeEventListener("keydown", handleKeyDown);
  }, []);

  const handleSelect = useCallback((callback: () => unknown) => {
    setIsOpen(false);
    callback();
  }, []);

  useEffect(() => {
    if (!isOpen) {
      setQuery("");
    }
  }, [isOpen]);

  const { setTheme } = useTheme();

  useHotkeys([
    ["ctrl+K", () => setIsOpen((open) => !open)],
    ["meta+K", () => setIsOpen((open) => !open)],
  ]);

  const runCommand = useCallback(
    (command: () => void) => () => {
      setIsOpen(false);
      command();
    },
    [],
  );

  return (
    <>
      <Button
        variant="outline"
        className="relative h-9 w-9 p-0 xl:h-10 xl:w-60 xl:justify-start xl:px-3 xl:py-2"
        onClick={() => setIsOpen(true)}
      >
        <Icons.search className="h-4 w-4 xl:mr-2" aria-hidden="true" />
        <span className="hidden xl:inline-flex">{t("search.title")}</span>
        <span className="sr-only">Search products</span>
        <kbd className="pointer-events-none absolute right-1.5 top-2 hidden h-6 select-none items-center gap-1 rounded border bg-muted px-1.5 font-mono text-[10px] font-medium opacity-100 xl:flex">
          <abbr title="Control">⌘</abbr>K
        </kbd>
      </Button>
      <CommandDialog position="top" open={isOpen} onOpenChange={setIsOpen}>
        <CommandInput
          placeholder={t("search.placeholder")}
          value={query}
          onValueChange={setQuery}
        />
        <CommandList>
          <CommandEmpty
            className={cn(isPending ? "hidden" : "py-6 text-center text-sm")}
          >
            No product, page, or command found.
            <br className="my-2" />
            Try find something in another ways.
          </CommandEmpty>
          {isPending ? (
            <div className="space-y-1 overflow-hidden px-1 py-2">
              <Skeleton className="h-4 w-10 rounded" />
              <Skeleton className="h-8 rounded-sm" />
              <Skeleton className="h-8 rounded-sm" />
            </div>
          ) : (
            data?.map((group) => (
              <CommandGroup
                key={group.category}
                className="capitalize"
                heading={group.category}
              >
                {group.products.map((item) => (
                  <CommandItem
                    key={item.id}
                    onSelect={() =>
                      handleSelect(() => router.push(`/product/${item.id}`))
                    }
                  >
                    {item.name}
                  </CommandItem>
                ))}
              </CommandGroup>
            ))
          )}
          {navItems.mainNav.map((item) => (
            <Fragment key={item.title}>
              <CommandSeparator />
              <CommandGroup heading={item.title}>
                {item.subcategories.map((subcategory) => (
                  <CommandItem
                    key={subcategory.slug}
                    value={t(`main.${subcategory.title}`)}
                    onSelect={runCommand(() => router.push("#"))}
                    className="capitalize"
                  >
                    <File className="mr-2 h-4 w-4" />
                    {subcategory.title}
                  </CommandItem>
                ))}
              </CommandGroup>
            </Fragment>
          ))}
          <CommandGroup heading={t("command.theme")}>
            <CommandItem onSelect={runCommand(() => setTheme("light"))}>
              <Sun className="mr-2 h-4 w-4" />
              <span>{t("command.light")}</span>
            </CommandItem>
            <CommandItem onSelect={runCommand(() => setTheme("dark"))}>
              <Moon className="mr-2 h-4 w-4" />
              <span>{t("command.dark")}</span>
            </CommandItem>
            <CommandItem onSelect={runCommand(() => setTheme("system"))}>
              <Laptop className="mr-2 h-4 w-4" />
              <span>{t("command.system")}</span>
            </CommandItem>
          </CommandGroup>
        </CommandList>
      </CommandDialog>
    </>
  );
}
