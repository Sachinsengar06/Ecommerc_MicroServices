# Microservices Assignment - Ecommerce Backend


## Overview

This project implements a backend for an ecommerce application using Microservices Architecture.
It's designed to be scalable and extensible for future functionalities. 

## Technologies

* **Programming Language:** C# (.NET Core)
* **Service Discovery:** Ocelot (API Gateway)
* **Communication Pattern:** Message-based communication with RabbitMQ
* **Authentication:** JWT-based authentication

## Microservices

* **Product Service:** Manages product information (add, remove, update) and fetches details from other services.
* **Cart:** Stores and manages product in cart.
* **AuthService:** Provide authentication.
* **CheckOut:** Not implement(for future developments)

**Note:** This initial implementation focuses on basic functionalities. Additional services like Cart and Order services can be integrated in future iterations.

## Deliverables

* **README.md:** This file describes the project overview, technologies, and functionalities.
* **URL Definitions:** A Postman collection with API endpoints and sample requests/responses will be provided in a separate file ([https://learning.postman.com/docs/publishing-your-api/documenting-your-api/](https://learning.postman.com/docs/publishing-your-api/documenting-your-api/)).
* **Dockerfiles:** Dockerfiles for each microservice will be included in the project directory. 
* **Source Code:** The source code for all microservices will be available in the project directory.

## Assumptions

* In-memory data structures are used for this assignment. Integration with a database can be implemented in future.
* User interface development is not included in this scope.


## Getting Started

1. Clone this repository.
2. Install required dependencies using NuGet Package Manager.
3. Run the provided `.bat` script to start all microservices using Docker.
4. Use the provided Postman collection to test the API endpoints.

## Further Development

This project provides a starting point for building a scalable ecommerce backend using Microservices Architecture. Future features can include:

* Integration with database for persistent data storage.
* Implementation of additional services like Cart and Order services.
* User interface development for a complete application.
* Integration with payment gateway for checkout functionality.


This project demonstrates the concepts of Microservices Architecture and their implementation in a .NET Core environment. It serves as a foundation for building a robust and scalable ecommerce application.
