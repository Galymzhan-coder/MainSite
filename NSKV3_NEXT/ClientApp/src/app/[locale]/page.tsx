import Image from "next/image";
import { REPOSITORY_URL, siteConfig } from "~/app";
import { Link, redirect, type Locale } from "~/navigation";
import { eq } from "drizzle-orm";

import { db } from "~/data/db";
import { User, users } from "~/data/db/schema";
import { seo } from "~/data/meta";
import Calculator from "~/islands/sections/calculator";
import CallToAction from "~/islands/sections/call-to-action";
import Contacts from "~/islands/sections/contacts";
import HeroSlider from "~/islands/sections/hero-slider";
import News from "~/islands/sections/news";
import QuickActions from "~/islands/sections/quick-actions";
import Reviews from "~/islands/sections/reviews";
import { GeneralShell } from "~/islands/wrappers/general-shell";
import { getServerAuthSession } from "~/utils/users";

export const metadata = seo({
  title: `Главная
 – ${siteConfig.name}`,
});

type HomePageProps = { params: { locale: Locale } };

export default async function HomePage({ params: { locale } }: HomePageProps) {
  // Get the user session for NextAuth.js and Clerk
  const session = await getServerAuthSession();

  // Ensure that the user is ready to use the app
  if (session) {
    const user: User = await db
      .select()
      .from(users)
      .where(eq(users.id, session.id))
      .then((res: User[]) => res[0] ?? null);
    if (!user || (user && !user.emailVerified)) {
      return redirect(`/auth`);
    }
  }

  return (
    <div className="sm:pl-16">
      <HeroSlider />
      <QuickActions />
      <Calculator />
      <CallToAction />
      <Reviews />
      <News />
      <Contacts />
    </div>
  );
}

// ===== [TODO SECTION] =================================================

// todo: fix typescript errors when using next-intl without "use client"
// unstable_setRequestLocale(locale); // needs for static pages rendering
// const t = useTranslations("landing"); // traditional page translations
// const t = await getTranslator(locale, "landing"); // also static pages
/** @see https://github.com/amannn/next-intl/pull/149 */

// ===== [INTERESTING THINGS ] ==========================================

// note: information below is not updated constantly and may be outdated

/**
 * The 'await' Keyword:
 *
 * The 'await' keyword is crucial in asynchronous programming in JavaScript.
 * It pauses the execution of an async function until a Promise is fulfilled,
 * ensuring that subsequent lines of code are executed only after the awaited
 * operation completes. This makes handling asynchronous operations more
 * intuitive and allows for a sequential programming style even with
 * asynchronous code.
 *
 * Usage Example:
 * When fetching data from an API, using 'await' ensures that the variable
 * storing the fetched data is assigned only after the data has been
 * successfully retrieved.
 */

/**
 * [Important Next.js Caveat: Dynamic Rendering with cookies()]
 *
 * Understanding cookies() in Next.js:
 * The function cookies(), in the context of Next.js, is dynamic by nature.
 * This means that its output varies and is determined at the time of request.
 *
 * Impact on Static Rendering:
 * Using cookies() within key components like Layout.tsx, which are common
 * across multiple pages, can affect the static rendering capabilities of your
 * application. Specifically, it causes these pages to be rendered dynamically
 * at request time, rather than being pre-rendered as static HTML.
 *
 * Why Caution is Needed:
 * For large applications with numerous static pages, opting into dynamic
 * rendering for all pages by using cookies() in a common layout component can
 * potentially impact performance and loading times. It's advisable to assess
 * the necessity and placement of cookies() within your application, especially
 * if static rendering is a priority.
 *
 * @see https://nextjs.org/docs/app/api-reference/functions/cookies
 * @see https://michaelangelo.io/blog/darkmode-rsc#important-nextjs-caveat
 * @see https://github.com/pacocoursey/next-themes/issues/152#issuecomment-1693979442
 */

/**
 * !📄 "TRPC EXAMPLE 1"
 *
 * import { api } from "~/data/api/package/server";
 * import { CreateTodo } from "~/islands/data-api/create-todo";
 *
 * const hello = await api.todo.hello.query({ text: "from tRPC" });
 *
 * <CrudShowcase />
 *
 */
/*
<section className="flex flex-col items-center gap-2 pb-20">
  <p className="text-2xl text-white">
    {hello ? hello.greeting : "Loading tRPC query..."}
  </p>
  <div className="flex flex-col items-center justify-center gap-4">
    <p className="text-center text-2xl text-white">
      {session && <span>Logged in as {session.user?.name}</span>}
    </p>
  </div>
</section>
*/
/*
async function CrudShowcase() {
  const session = await getServerAuthSession();
  if (!session?.user) return null;
  const latestTodo = await api.todo.getLatest.query();
  return (
    <div className="w-full max-w-xs">
      {latestTodo ? (
        <p className="truncate">Your most recent todo: {latestTodo.name}</p>
      ) : (
        <p>You have no todos yet.</p>
      )}
      <CreateTodo />
    </div>
  );
}
*/

/**
 * !📄 "TRPC EXAMPLE 2"
 *
 * ?1️⃣ Import the component and forse cache:
 * import { serverClient } from "~/islands/wrappers/trpc/server-client";
 * import TodoList from "~/islands/features/todo-list";
 * export const dynamic = "force-dynamic";
 *
 * ?2️⃣ Place this in your component before return:
 * const todos = await serverClient.getTodos();
 *
 * ?3️⃣ Use this inside the component:
 * <TodoList initialTodos={todos} />
 *
 * @see https://youtu.be/qCLV0Iaq9zU?t=996
 */

/**
 * !📄 "HOW TO GENERATE METADATA USING NEXT-INTL (CURRENTLY UNSTABLE)"
 *
 * ?1️⃣ Import the next things:
 * import { type Metadata } from "next";
 * import { LocaleLayoutParams } from "~/types";
 * import { getTranslator } from "next-intl/server";
 *
 * ?2️⃣ Use the next function:
 * export async function generateMetadata({
 *   params,
 * }: LocaleLayoutParams): Promise<Metadata> {
 *   const t = await getTranslator(params.locale, "landing");
 *   return {
 *     description: t(""),
 *   };
 * }
 *
 * @see https://next-intl-docs.vercel.app/docs/environments/metadata-route-handlers
 */

/**
 * !📄 "HOW TO GET AUTH-SESSION IN SERVER-COMPONENT"
 *
 * ?1️⃣ Import:
 * import { getServerSession } from "next-auth";
 * import { authOptions } from "~/server/auth";
 *
 * ?2️⃣ Add variables inside the component:
 * const session = await getServerSession(authOptions);
 *
 * ?2️⃣ Use code example inside the component:
 * <div>
 *   getServerSession Result
 *   {session?.user?.name ? (
 *     <div>{session?.user?.name}</div>
 *   ) : (
 *     <div>Not logged in</div>
 *   )}
 * </div>
 *
 * @see https://next-auth.js.org/configuration/nextjs
 */

/**
 * !📄 "HOW TO GET AUTH-SESSION IN CLIENT-COMPONENT"
 *
 * ?1️⃣ Import:
 * import { getServerSession } from "next-auth";
 *
 * ?2️⃣ Add hook and vars inside the component:
 * const { data: session } = useSession();
 * const name = `${session?.user?.name ?? ""}`;
 *
 * ?2️⃣ Use code example inside the component:
 * <div>
 *   useSession User Name
 *   {name ? (
 *     <div>{name}</div>
 *   ) : (
 *     <div>Not logged in</div>
 *   )}
 * </div>
 *
 * @see https://next-auth.js.org/configuration/nextjs
 */
