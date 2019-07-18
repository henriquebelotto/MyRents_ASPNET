# ASP.NET MVC and Web API Example Project - Entity Framework, DI using Autofac and more

This project is in development using ASP.NET and it intends to demonstrate some features available in this framework. 
It includes a local database, MVC (Model-View-Controller) architecure with multiple views, Web API, controllers and models, also web forms.

## How to use this project:
1- Clone this repository to a local folder;
2- Enable-migration using the Package manager console
3- Update the database

### Accounts:
There are two regular accounts that you can use:
Administrator account - Includes features such as add new movies and customers and delete them.
Login: admin@myrents.ca
password: @Aa123456

Guest account - Just use New Rental features and see the list of movies and customers
Login: guest@myrents.ca
password: @Aa123456

## Features: 
- Entity Framework and Code First methodology;
- Migrations;
- ViewModel;
- Bootstrap;
- Form validation and Data Annotation(client and server-side);
- Use of Anti-forgery Token to avoid "Cross-Site Request Forgery" (CSRF) attack;
- DTOs (Data Transfer Object);
- Automapper to map the DTO and models;
- CRUD operations with Web API:
  - Get Customers & Movies
    - (api/customers & api/customers/{id}   api/movies & api/movies/{id})
  - Post Customers & Movies
    - (api/customers  api/movies)
    - Add json with the customer/movie information
  - Update Customers & Movies
    - (api/customers/{id}  api/movies/{id})
    - Add json with the customer/movie information
  - Delete Customers & Movies
    - (api/customers/{id}  api/movies/{id})
- JQuery, DataTable, and AJAX used to create tables and call the Web APIs;
- Stateless (No sessions)
- Dependency Injection using Autofac both for "regular" controllers and Web API controllers
- Security features using Identity Framework;
- Use of TypeAhead plugin to enable auto-complete in web forms by querying the web API to request the possible values according to the user's input


*Project developed by Henrique Belotto following an Udemy course example @ 2019*