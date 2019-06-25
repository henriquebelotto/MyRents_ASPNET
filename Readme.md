# ASP.NET MVC Example Project

This project is in development using ASP.NET and it intends to demonstrate some features available in this framework. This project includes a local database, MVC (Model-View-Controller) architecure with multiple views, controllers and models, also web forms.

### Features
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

*Project developed by Henrique Belotto following an Udemy course example @ 2019*