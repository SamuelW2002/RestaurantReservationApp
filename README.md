# Restaurant Reservation App

Welcome to the Restaurant Reservation App, a project showcasing my understanding of a layered architecture within a simple application designed to manage restaurant reservations.

## Project Overview

This project utilizes a **layered architecture** design. Although it may seem extensive for an application of this scope, my goal was to deliver something unique and well-structured, unlike the typical applications you might see.

The project required approximately **five hours of coding** plus an additional hour for **documentation, testing, and cleanup** (such as spelling corrections and spacing adjustments).

## Key Design Choices

### Layered Architecture

A **layered architecture** helps enforce the **Single Responsibility Principle** by clearly assigning each layer a specific role.

- **Input Layer**: Responsible for handling user inputs, adapted from the original project. While the AppLogic layer also handles a portion of user inputs, a full separation seemed overly complex for the scope of this project. I’m open to feedback on how to simplify this separation further.
  
- **AppLogic Layer**: This layer includes all services and manages the application’s core logic and operations. Separating the core operations in this way makes testing more straightforward and keeps the application well-organized and maintainable.

- **Domain Layer**: The domain layer holds all domain logic and entities. Isolating these components clarifies their location within the project and supports Domain-Driven Design (DDD) principles.

- **Infrastructure Layer**: This layer is solely responsible for database interactions. It contains no business logic, and its functionality relies on frameworks like **Entity Framework** and **LINQ**. Since the layer doesn’t perform any unique operations, it’s excluded from testing.

### SOLID Principles in Practice

This project demonstrates several SOLID principles:

- **Single Responsibility**: Implemented primarily by structuring the project as a layered architecture.
  
- **Open/Closed Principle**: In scenarios requiring extended functionality, new classes can implement existing interfaces without modifying existing code. This approach keeps the code flexible and maintainable, improving testability with mock objects and reducing coupling.
  
- **Liskov Substitution Principle**: Ensured through the use of interfaces, the code abides by Liskov Substitution principles by enabling classes to be interchanged seamlessly.
  
- **Interface Segregation**: The `AppointmentService` interface includes only the methods required by each calling class, making the interface leaner and the code more readable and maintainable. When additional methods are needed by different classes, they can be added in separate interfaces.
  
- **Dependency Inversion**: Dependencies are abstracted through interfaces, enabling flexible, decoupled development where classes can easily interchange without modification.

### Dependency Injection

Dependency Injection (DI) is used within the `Program.cs` file to register services with the DI container, improving the app’s efficiency and flexibility. DI allows us to create instances only when required, saving on resources and improving startup times.

### Utility Class Library

The Utility Class Library reduces code duplication and includes reusable classes, such as:
- **Contract Class**: For exception handling and validation, minimizing repetitive conditional statements.
- **InputChecker Class**: Handles input validation centrally, reducing redundant code throughout the application.

### LineReader

The **LineReader** class, registered within the DI container, allows console input-based methods to be tested with mock objects. This design prevents console input methods from blocking tests.

## Project Link

You can explore the code and documentation in detail on GitHub:

[Restaurant Reservation App on GitHub](https://github.com/SamuelW2002/RestaurantReservationApp.git)
