# Software Specification and Modeling 2023/2024 Team Project

## Project Overview
This project was developed as part of the Software Specification and Modeling course for the academic year 2023/2024. The project required collaboration in a team of five members using C#, .NET, and WPF. The project followed sustainable coding practices, clean architecture principles, and solid design patterns. Lectures covered topics such as Clean Code, Polymorphism, SOLID, Software Design Structures, Sequence Diagrams, Behavior Design using PlanUML, and Business Activity Design.

## Project Roles and Responsibilities
Each team member was assigned a specific role with corresponding responsibilities. The roles included:

### 1. **Owner**
   - Responsible for managing accommodations, including registration, statistics, and renovations.

### 2. **Guest**
   - Manages accommodation search, booking, reviews, and reservation modifications.

### 3. **Guide**
   - Handles the creation, management, and cancellation of tours.

### 4. **System Administrator**
   - Manages user roles, system settings, and overall system maintenance.

### 5. **Driver (My Role)**
   - Implemented the core functionality of managing and displaying tours.
   - Responsible for tracking live tours, marking key points, and handling cancellations.

## Functional Requirements

### Shared Functionality
- **User Authentication:** All users can log in and out using a unified login form. Depending on the user’s role, a corresponding interface is displayed.

### Note on Driver Login:
- Drivers have specific login details. To log in as a driver, check the credentials in the `Resources -> Data -> users` directory. Drivers are assigned the role number **4**.
