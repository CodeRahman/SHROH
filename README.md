# 💸 SHROH – Personal Finance Tracker

**SHROH** is a personal finance management app built with .NET MAUI. It helps users track income, expenses, budgets, with a clean mobile UI.

---
### 🧾 Course:
**CSC 317 – Final Project**  
*Spring 2025 – Group 3*

### 👥 Team Members & Responsibilities:
- **Alexander Harrington** – Project Manager  
- **Abhishek Ray** – Quality Assurance Tester  
- **Christopher Halbert** – Back-End Developer  
- **Abdurrahman Oyediran** – Front-End Engineer  


---

## ✅ Final Project Submission Checklist

This submission includes:

1. ✅ All project files (`.cs`, `.xaml`, `.csproj`, `.sqlite`, and images if applicable)
2. ✅ Full implementation of **all pages** from the original Front-End Design Document
3. ✅ App navigation that links every page together via a central navigation bar
4. ✅ A complete documentation package including this `README.md` and "How to Run" instructions


---

## 📱 Features

- ✅ **Dashboard** showing income, expenses, balance, and recent transactions
- ✅ **Transactions page** for adding, editing, and deleting financial entries
- ✅ **Budgets page** to set category-based spending limits and track usage
- ✅ **Settings page** to choose currency (USD, GBP, EUR) and toggle light/dark theme
- ✅ Local **SQLite database** for saving all records

---

## 🛠️ How to Run

### 🔧 Requirements

- **Visual Studio 2022 or newer**
- **.NET 7 or 8 SDK**
- **.NET MAUI workload**
- Android emulator OR physical device

### ▶️ Steps

1. **Clone or open the project in Visual Studio**

   Open the solution file:

2. **Restore NuGet packages**


3. **Build the project**

  Press `Ctrl+Shift+B` or right click the project and click Build


4. **Run the app**

- Select windows machine.
- click the ▶️ Start button.

---

## 💾 Data Handling

- All user data is stored locally using **SQLite**
- Settings, transactions, and budgets persist between app launches

---

## 🌓 Theme & Currency Support

- Set your preferred **currency** (USD / GBP / EUR) in **Settings**
- Choose between **Light** and **Dark** mode — applied app-wide instantly (under construction)
- Currency conversion automatically updates transaction, budget, and dashboard values

---

## 📂 Project Structure

| Folder/File | Purpose |
|-------------|---------|
| `Views/` | All UI pages (XAML + code-behind) |
| `Models/` | Data models for Budget, Transaction, and Settings |
| `Services/DatabaseService.cs` | Handles SQLite operations |
| `Helpers/CurrencyHelper.cs` | Centralized currency formatting logic |

---

## 🧪 Example Use

1. Add a budget for “Utilities” at $500
2. Add a transaction in “Utilities” for $150
3. See the budget update and balance reflect instantly
4. Change to “EUR” and watch all amounts update dynamically

---

### 🔧 Challenges We Faced

- Setting up and using SQLite across multiple views
- Creating a reusable NavigationBar with MAUI
- Maintaining consistent XAML UI layout
- Handling dynamic binding errors from user input
- Implementing light and dark theme toggling

---

### ✅ Project Successes

- Fully working backend with real-time SQLite integration
- Seamless navigation between pages
- Clean UI consistent with original design document
- Successfully met all Milestone 3 requirements

---

## ✅ Author & License

Created by SHROH Group for CSC 317.  
Licensed under USM.




