# Restaurant Reservation App

Welcome to the Restaurant Reservation App, a project showcasing my understanding of a layered architecture and SOLID design principles within a simple application designed to manage restaurant reservations.

## Project Overview

This project utilizes a **layered architecture** design. Although it may seem extensive for an application of this scope, my goal was to create a project that highlights my understanding and skills.

The project required approximately **five hours of coding** plus an additional hour for **documentation, testing, and cleanup** (such as spelling corrections and spacing adjustments).

## Key Design Choices

### Layered Architecture

A **layered architecture** helps enforce the **Single Responsibility Principle** by clearly assigning each layer a specific role.

- **Input Layer**: Responsible for handling user inputs. While the AppLogic layer also handles a portion of user inputs, a full separation seemed overly complex for the scope of this project. Otherwise, it would require constant communication between the two layers for each input. I’m open to feedback on how to simplify this separation further.
  
- **AppLogic Layer**: This layer includes all services and manages the application’s core logic and operations. Separating the core operations in this way makes testing more straightforward and keeps the application well-organized and maintainable.

- **Domain Layer**: The domain layer holds all domain logic and entities. Isolating these components clarifies their location within the project and supports Domain-Driven Design (DDD) principles. (Note: DDD is not applied in this project)

- **Infrastructure Layer**: This layer is solely responsible for database interactions. It contains no business logic, and its functionality relies on frameworks like **Entity Framework** and **LINQ**. Since the layer doesn’t perform any unique operations, it’s excluded from testing. Testing this layer would primarily test the frameworks themselves, which we trust to function correctly. By using the frameworks we trust that they are fully operational since we cannot change anything if they were to malfunction.

### SOLID Principles in Practice

This project demonstrates several SOLID principles:

- **Single Responsibility**: Implemented primarily by structuring the project as a layered architecture.
  
- **Open/Closed Principle**: In scenarios requiring extended functionality, new classes can implement existing interfaces without modifying existing code. This approach keeps the code flexible and maintainable, improving testability with mock objects and reducing coupling.
Once a class is fully operational and tested, making even minor edits could risk altering its functionality, so changes are better managed by extending functionality through interfaces.
  
- **Liskov Substitution Principle**: Ensured through the use of interfaces, the code abides by Liskov Substitution principles by enabling classes to be interchanged seamlessly.
  
- **Interface Segregation**: For example: The `IReservationService` interface includes only the methods required by each calling class, making the interface leaner and the code more readable and maintainable. When additional methods are   needed by different classes, they can be added in separate interfaces.
  The ReservationService class contains 5 methods:
  ![image](https://github.com/user-attachments/assets/cbe29f66-a68e-4c38-abdd-22b522b5161f)

  Of these 5 methods only 2 are added to the interface:

  ![image](https://github.com/user-attachments/assets/b94514c6-ed4b-468c-b5fd-a996d18af6e7)

  Since the class implementing the service has no need to call the other 3 methods.
  These three methods are set to internal rather than private to allow access by the testing project, enabled by adding a line to properties.cs.

  ![image](https://github.com/user-attachments/assets/5caf37c1-2655-4099-bcc2-db17b81a0173)

- **Dependency Inversion**: Dependencies are abstracted through interfaces, enabling flexible, decoupled development where classes can easily interchange without modification. This way, the class implementing the interface can be modified freely without risking issues in classes that depend on the interface.
The classes calling an implementation of the interface are fully functional and tested, they are tested using mocks. Therefore, changes to the implementing class will not cause failures in the dependent classes, keeping their tests fully operational.

### Dependency Injection

Dependency Injection (DI) is used within the `Program.cs` file to register services with the DI container, improving the app’s efficiency and flexibility. DI allows us to create instances only when required, saving on resources and improving startup times.
![image](https://github.com/user-attachments/assets/af6c0fff-f653-4699-8b70-fc38dc28ad4f)

First, we configure the DI container to reuse an instance of each injectable class using 
```csharp
services.AddSingleton<IService, ServiceImplementation>();
```
Ensuring the same instance is provided whenever required.

Next, we specify that if a class requires Interface X, it will receive an instance of Class X.

Lastly, we configure the services to be lazy-loaded, allowing us to use the following code:
![image](https://github.com/user-attachments/assets/e6d8ede0-b81e-4f6a-ae34-996ff71699a5)

This causes the container to only create the instance if it is called, let's say a user uses the application but only wants to see all the customers. In this case, creating instances of `AppointmentService` and `RestaurantService` would be unnecessary. In the context of cloud computing and containerized environments, these small resource savings can have a significant impact.

The LineReader instance is created right away since this will always be used under any circumstance.

### Utility Class Library

The Utility Class Library reduces code duplication and includes reusable classes, such as:
- **Contract Class**: For exception handling and validation, minimizing repetitive conditional statements.
- **InputChecker Class**: Handles input validation centrally, reducing redundant code throughout the application.

### LineReader

The `LineReader` class, registered within the DI container, allows console input-based methods to be tested with mock objects. This design prevents console input methods from blocking tests.
