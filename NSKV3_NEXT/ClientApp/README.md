# How to Install and Get Started

1. **Essential Tools**: Ensure that [_VSCode_](https://code.visualstudio.com), [_Git_](https://learn.microsoft.com/en-us/devops/develop/git/install-and-set-up-git), _GitHub Desktop_ ([Windows/macOS](https://desktop.github.com/) | [Linux](https://dev.to/rahedmir/is-github-desktop-available-for-gnu-linux-4a69)), and _Node.js LTS_ ([Windows/macOS](https://nodejs.org) | [Linux](https://youtu.be/NS3aTgKztis)) are installed.
2. **Project Cloning**: [_Create a new fork_](https://github.com/arvisix/nsk/fork) and use GitHub Desktop to download it.
3. **Configuration**: Open VSCode and load the project folder. Press `Ctrl+Shift+P` and search for `>Create New Terminal`. Install _PNPM_ using `corepack enable`. Then, enter `pnpm install` to install the packages. Next, copy the `.env.example` file to a new `.env` file and fill in at least the `DATABASE_URL` field. Finally, send the database schema to your database using `pnpm mysql:push` or `pnpm pg:push`.
4. **Run, Stop, Build**: Use `pnpm dev` to run the app (visit <http://localhost:3000> to check it). Stop it by focusing on the console and pressing `Ctrl+C`. After making changes, build the app using `pnpm build`. Thats okay if you see Clerk's warnings.
5. **Commit and Deploy**: Upload your project to your GitHub profile using GitHub Desktop. Then, deploy it by importing the project into [Vercel](https://vercel.com/new), making your website publicly accessible on the internet. If you wish to share your work, seek feedback, or ask for assistance, you're welcome to do so either [in our Discord server](https://discord.gg/Pb8uKbwpsJ) or [via GitHub discussions](https://github.com/arvisix/nsk/discussions).

Please scroll down the page to see a lot of useful information about how everything works in the project.

## Project Commands

- **`pnpm stripe:listen`**: This command runs the Stripe webhook listener and assists in setting up Stripe environment variables. You may need to have [Stripe CLI](https://stripe.com/docs/stripe-cli) installed to run this command.
- **`pnpm appts`**: This command performs a comprehensive check of the codebase. It sequentially executes `pnpm typecheck` to conduct type-checking and identify any TypeScript errors, `pnpm lint` for code linting, `pnpm format` to format with Prettier, and finally, `pnpm:build`.
- **`pnpm latest`**: This command updates all project packages to their latest stable versions and updates `next-intl` to the latest beta version. Please update the latest line in the `scripts` section of `package.json` if a [newer beta version of _next-intl_](https://github.com/amannn/next-intl/pull/149) is released.
- **`pnpm latest:canary`**: This command runs `pnpm latest` and updates Next.js and React to the latest versions available on their canary branches. Use this only if you are certain about why you need it.

## Environment Variables (`.env` file)

**Refer to the [`.env.example`](https://github.com/arvisix/nsk/blob/main/.env.example) file as your guide. Simply copy data from it to a new `.env` file.**
