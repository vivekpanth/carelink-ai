# CareLink AI

**AI-powered community support platform that connects vulnerable Australians with the health, housing, food, mental health, employment, and legal services they need — fast.**

Describe your situation in plain, everyday language and CareLink AI uses **DeepSeek** (via OpenRouter) to understand it and rank the most relevant support services from a curated directory. Built with ASP.NET Core MVC on .NET 10.

🔗 **Live demo:** https://carelink-ai-9843.onrender.com/

> ⏳ The demo runs on Render's free tier, so the first request after a period of inactivity takes ~50 seconds to "cold start" while the instance wakes up. Subsequent requests are fast.

---

## Table of contents

- [Overview](#overview)
- [Features](#features)
- [How the AI matching works](#how-the-ai-matching-works)
- [Tech stack](#tech-stack)
- [Architecture](#architecture)
- [Getting started](#getting-started)
- [Configuration](#configuration)
- [Deployment](#deployment)
- [Project structure](#project-structure)
- [Disclaimer & crisis support](#disclaimer--crisis-support)
- [Project context](#project-context)

---

## Overview

Finding the right community service is hard when you're already in a difficult situation. Directories are long, jargon-heavy, and organised by category rather than by need. CareLink AI flips that: a person types something like *"I was evicted and need emergency housing tonight"* and the app returns a short, ranked list of the services most likely to help, each with a plain-language reason for the match and direct contact details.

The platform pairs a large language model (DeepSeek) with a deterministic keyword-based fallback, so it still returns useful results even if the AI service is unavailable.

## Features

- **AI Service Matcher** — Natural-language intake. A user describes their needs (and optionally their suburb) and DeepSeek ranks up to 5 best-fit services with a match score and a short explanation for each.
- **Browsable service directory** — Filter by category (Health, Mental Health, Housing, Food, Employment, Legal), keyword search, and suburb. Each listing includes description, address, phone, hours, website, and whether the service is free.
- **AI support assistant** — A compassionate chat assistant that helps users find services and always surfaces crisis hotlines when appropriate.
- **Crisis detection** — The AI flags messages that indicate immediate danger and prominently surfaces Lifeline (13 11 14) and Beyond Blue (1300 22 4636).
- **Graceful AI fallback** — If the AI call fails or times out, a keyword/category scoring algorithm still produces relevant matches, so the app never leaves a user with nothing.
- **User accounts** — Registration and login via ASP.NET Core Identity.
- **Responsive UI** — Clean, accessible interface built with Tailwind CSS.

## How the AI matching works

1. The user submits a free-text description of their needs (e.g. *"I feel anxious and depressed and need counselling"*) plus an optional suburb.
2. The full service directory is serialised into a compact list and sent to DeepSeek with a system prompt instructing it to act as a *"compassionate community services navigator"* and respond **only** with valid JSON.
3. DeepSeek returns a ranked JSON array of service IDs with a `score` (0–100) and a short `reason`. The app parses this, maps IDs back to services, and renders the ranked results.
4. **Fallback:** if the API errors out or returns malformed data, a local keyword-matching algorithm scores services against the user's text and suburb and returns the top results instead.

A separate lightweight prompt is used for crisis screening, returning a simple `YES`/`NO` so the UI can react quickly.

## Tech stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Language | C# |
| AI model | DeepSeek (`deepseek/deepseek-chat-v3-0324`) via the [OpenRouter](https://openrouter.ai) API |
| Data | Entity Framework Core + SQLite |
| Auth | ASP.NET Core Identity |
| UI | Tailwind CSS, Razor views |
| Containerisation | Docker (multi-stage build) |
| Hosting | Render (configurable for Railway) |

## Architecture

- **`AIService` / `IAIService`** — Encapsulates all AI calls. Talks to OpenRouter over `HttpClient`, handles JSON parsing, and contains the deterministic fallback matcher. Registered as a singleton.
- **`Service` model** — The core domain entity (title, description, category, address, suburb, state, phone, website, hours, free flag, tags for matching).
- **`ApplicationDbContext` + `SeedData`** — EF Core context with automatic migration on startup and a seeded directory of real Australian support organisations.
- **Controllers / Razor Views** — `HomeController` (landing page with live stats) and `ServicesController` (directory, details, and the `AiMatch` flow).

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- An [OpenRouter API key](https://openrouter.ai/keys) (a free DeepSeek tier is available)

### Run locally

```bash
# 1. Clone
git clone https://github.com/vivekpanth/carelink-ai.git
cd carelink-ai

# 2. Add your OpenRouter API key (user secrets keep it out of source control)
dotnet user-secrets set "OpenRouter:ApiKey" "sk-or-..."

# 3. Restore, build, run
dotnet restore
dotnet run
```

The app applies EF Core migrations and seeds the service directory automatically on first run. Open the URL printed in the console (e.g. `https://localhost:5001`).

> Without an API key the app still runs — the AI matcher transparently falls back to keyword matching.

## Configuration

Settings are read from `appsettings.json`, environment variables, or user secrets:

| Key | Description | Default |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | SQLite connection string | local `app.db` |
| `OpenRouter:ApiKey` | Your OpenRouter API key | *(empty)* |
| `OpenRouter:Model` | Model identifier | `deepseek/deepseek-chat-v3-0324:free` |

> **Never commit your API key.** Use `dotnet user-secrets` locally and environment variables in production.

## Deployment

The repo includes a multi-stage **`Dockerfile`** that builds and publishes the app, then runs it on the ASP.NET runtime image. Kestrel binds to the `PORT` environment variable, so it works out of the box on platforms like Render and Railway.

```bash
docker build -t carelink-ai .
docker run -p 8080:8080 -e OpenRouter__ApiKey="sk-or-..." carelink-ai
```

The live demo is deployed on Render's free tier (hence the cold-start delay noted above). A `railway.json` is also included for Railway deployments.

## Project structure

```
carelink-ai/
├── Areas/Identity/        # ASP.NET Identity UI
├── Controllers/           # Home & Services controllers
├── Data/                  # DbContext + seed data
├── Models/                # Service, ServiceMatch, view models
├── Services/              # AIService + IAIService (DeepSeek/OpenRouter)
├── Views/                 # Razor views (Tailwind-styled)
├── wwwroot/               # Static assets
├── Dockerfile             # Multi-stage container build
├── railway.json           # Railway deployment config
└── Program.cs             # App startup & DI
```

## Disclaimer & crisis support

CareLink AI is a demonstration project and **not** a substitute for professional advice or emergency services. AI-generated matches should be verified before acting on them.

**If you or someone you know is in crisis, contact:**

- **Lifeline** — 13 11 14
- **Beyond Blue** — 1300 22 4636
- In an emergency, call **000**.

## Project context

Built as **ITEC634 Assessment Task 3 (AT3)** — a full-stack web application demonstrating MVC architecture, AI integration, authentication, data persistence, and cloud deployment.

---

*Built by [Vivek Panth](https://github.com/vivekpanth).*
