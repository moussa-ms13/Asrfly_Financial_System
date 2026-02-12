# Asrfly Financial System | نظام أصرفلي المالي

A comprehensive financial management system built with a **Desktop App** (C# WinForms) and a **Mobile App** (Flutter), both backed by a cloud **Supabase** (PostgreSQL) database.

---

## 📋 Project Overview

**Asrfly** (أصرفلي) is a bilingual (Arabic-first) accounting and financial tracking platform designed for small-to-medium businesses. It provides full CRUD capabilities for income, expenses, suppliers, customers, categories, and projects — with receipt printing on Desktop and interactive charts on Mobile.

| Component | Path | Technology |
|---|---|---|
| Desktop App | `Asrfly3/` | C# .NET 8, WinForms, Entity Framework Core |
| Mobile App | `asrfly_mobile/` | Flutter (Dart), Supabase SDK |

---

## ✨ Features

### Desktop Application (WinForms)
- **Income & Expense Management** — Record, edit, delete, and search financial transactions
- **Project Accounting** — Track income/expenses per project with linked categories
- **Supplier & Customer Management** — Full CRUD with search functionality
- **Category System** — Organize transactions into custom categories
- **Receipt Printing** — Print professionally formatted Arabic receipts (سند قبض / سند صرف)
- **User Authentication** — Role-based login system with user management
- **Settings Panel** — Configure database connection, company info, and logo
- **Backup & Restore** — Database backup and restore utilities
- **System Records** — Track system-level events and records

### Mobile Application (Flutter)
- **Dashboard** — Real-time financial overview with total income, expenses, and balance
- **Interactive Charts** — 7-day income vs. expense bar chart (fl_chart)
- **Recent Transactions** — Quick-view list of the latest financial operations
- **Quick Actions** — Add income or expense entries directly from the home screen
- **Project Management** — Create, edit, and view project-specific financials
- **Category & Supplier/Customer Management** — Full CRUD screens
- **Image Attachments** — Attach photos to transactions via camera or gallery
- **Session Management** — Persistent login with auto-session restoration
- **RTL Support** — Full right-to-left Arabic interface

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Desktop UI | C# WinForms (.NET 8) |
| Desktop ORM | Entity Framework Core (Npgsql) |
| Mobile UI | Flutter 3.x (Dart) |
| Mobile Backend | Supabase Flutter SDK |
| Database | PostgreSQL (Supabase hosted) |
| Charts | fl_chart |
| Image Handling | image_picker |
| Local Storage | shared_preferences |
| Architecture | 3-Layer (Core → Data → Presentation) |
| Testing | MSTest (Desktop) |

---

## 📁 Project Structure

```
Asrfly_Financial_System/
├── Asrfly3/                        # Desktop Application
│   ├── Asrfly/                     # Main WinForms project
│   │   ├── Code/                   # Utilities (Receipt printing, navigation, messages)
│   │   ├── Gui/                    # WinForms screens by module
│   │   │   ├── GuiHome/            # Main dashboard
│   │   │   ├── GuiIncome/          # Income management
│   │   │   ├── GuiOutcome/         # Expense management
│   │   │   ├── GuiProjects/        # Project management
│   │   │   ├── GuiCategories/      # Category management
│   │   │   ├── GuiCustomers/       # Customer management
│   │   │   ├── GuiSuppliers/       # Supplier management
│   │   │   ├── GuiUsers/           # User & login management
│   │   │   └── GuiSettings/        # Application settings
│   │   └── Properties/             # App settings & resources
│   ├── Asrfly.Core/                # Domain models (entities)
│   ├── Asrfly.Data/                # Data access layer (EF Core)
│   │   ├── IDataHelper.cs          # Generic repository interface
│   │   └── SqlServer/              # Entity implementations & DbContext
│   ├── Asrfly.Tests/               # Unit tests (MSTest)
│   └── Asrfly.sln                  # Solution file
│
├── asrfly_mobile/                  # Mobile Application
│   ├── lib/
│   │   ├── main.dart               # App entry point & Supabase init
│   │   ├── models/                 # Data models
│   │   ├── screens/                # UI screens
│   │   │   ├── home/               # Dashboard with charts
│   │   │   ├── income/             # Add/edit income
│   │   │   ├── outcome/            # Add/edit expenses
│   │   │   ├── projects/           # Project management
│   │   │   ├── categories/         # Category screens
│   │   │   ├── customers/          # Customer screens
│   │   │   ├── suppliers/          # Supplier screens
│   │   │   ├── transactions/       # Transaction history
│   │   │   └── users/              # Login screen
│   │   ├── services/               # Business logic & Supabase queries
│   │   └── widgets/                # Reusable UI components
│   └── pubspec.yaml                # Flutter dependencies
│
├── assets/screenshots/             # App screenshots (add yours here)
├── .env.example                    # Environment variable template
└── README.md
```

---

## ⚙️ Prerequisites

### Desktop App
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022+ (with .NET Desktop Development workload)
- PostgreSQL database (Supabase or self-hosted)

### Mobile App
- [Flutter SDK](https://flutter.dev/docs/get-started/install) (3.x+)
- Dart SDK (included with Flutter)
- Android Studio / VS Code with Flutter extension
- A Supabase project with the required tables

---

## 🚀 Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/moussa-ms13/Asrfly_Financial_System.git
cd Asrfly_Financial_System
```

### 2. Configure Environment Variables

Copy the example file and fill in your Supabase credentials:
```bash
cp .env.example .env
```

**Desktop App** — Set the system environment variable:
```
SUPABASE_DB_CONN=Host=<your-host>;Port=5432;Database=postgres;User Id=<user>;Password=<password>;Ssl Mode=Require;Trust Server Certificate=true;CommandTimeout=300;
```

**Mobile App** — Pass credentials at build time:
```bash
flutter run --dart-define=SUPABASE_URL=https://your-project.supabase.co --dart-define=SUPABASE_ANON_KEY=your-anon-key
```

### 3. Run the Desktop App
```bash
cd Asrfly3
dotnet restore
dotnet run --project Asrfly
```
Or open `Asrfly.sln` in Visual Studio and press **F5**.

### 4. Run the Mobile App
```bash
cd asrfly_mobile
flutter pub get
flutter run --dart-define=SUPABASE_URL=<url> --dart-define=SUPABASE_ANON_KEY=<key>
```

### 5. Run Tests (Desktop)
```bash
cd Asrfly3
dotnet test
```

---

## 📸 Screenshots

> Add your application screenshots to `assets/screenshots/` and reference them here.

| Desktop | Mobile |
|---|---|
| ![Desktop Screenshot](assets/screenshots/desktop_home.png) | ![Mobile Screenshot](assets/screenshots/mobile_home.png) |

---

## 📄 License

This project is private and proprietary.
