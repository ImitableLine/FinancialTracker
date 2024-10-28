# Financial Tracker Web App
## Overview
The Financial Tracker is a web application developed with ASP.NET Core and PostgreSQL. This project serves as a learning exercise to gain familiarity with using PostgreSQL as a backend database in ASP.NET applications. The app provides basic financial management tools, allowing users to create, view, update, and delete accounts, budgets, and transactions.

## Features
User Authentication: Secure login system for access control.
Account Management: Add, edit, and delete financial accounts.
Budget Tracking: Create and manage budgets with fields for categories, amounts, and date ranges.
Transaction Tracking: Record transactions linked to specific accounts.
Planned Visualizations: A dashboard with charts and graphs for insights into financial data (in development).
Technologies Used
Backend: ASP.NET Core (C#)
Database: PostgreSQL, managed with pgAdmin
Frontend: Razor Pages with custom CSS for UI styling
Session Management: ASP.NET Core's session handling for user authentication

## Database Structure
The PostgreSQL database consists of multiple tables designed to capture key elements of personal financial management:

Users: Stores user information for authentication and association with financial data.
Accounts: Contains details of each financial account, including type and balance.
Budgets: Holds user-defined budget information, including category, amount, and date range.
Transactions: Logs transactions associated with specific accounts, enabling tracking of financial flows.
