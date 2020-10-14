# Reaction Timer - Web App

[![License MIT](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Project uses Razor Pages & MS SQL Server Express v13.

It features:
*	User registration
*	Login and Logout of user accounts
*	Reaction game which collects every game after it's finished in the database
*	Top 10 high scores that any player has achieved and their username and at what place the current user's top game is placed
*	Statistics of the current user's games
(Account registration and login use [BCrypt.Net-Core](https://www.nuget.org/packages/BCrypt.Net-Core/) for secure storage of passwords.)

## Prerequisites
* ASP .NET Core
* MS SQL Server Express v13

## Installation

Navigate to a new folder of your choice. Clone this repository.
```git
git clone https://github.com/kartaggen/ReactionTimer.git
```

During installation of the database, “ReactionTimerDB.dacpac” can be used or “ReactionTimerDB.sql” but the inner variables need to be updated.

Also, "appsettings.json" needs to be updated with your SQL Server Instance.
