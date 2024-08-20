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

## Main Page Help

![Main Page](https://github.com/user-attachments/assets/bef44c22-5359-46a8-aea4-e86e7fd1603e)

This section provides an overview of the Main Page and its key features:

1. **Today's Drives:**
   - View a list of all your scheduled drives, including the start and end addresses, and the scheduled time.

2. **Notifications Tab:**
   - Stay updated with the latest notifications.

3. **Dismiss Drive Button:**
   - Easily dismiss a drive.

4. **Status Buttons:**
   - Indicate your status with 'Late' and 'On Location' buttons.

5. **Timer:**
   - Track your driving time accurately with the built-in timer.

6. **Action Buttons:**
   - Use the 'Leave' and 'Start' buttons to manage your drive actions.

7. **Profile Button:**
   - Check your profile and vehicle information.

## Profile Page Help

![Profile Page](https://github.com/user-attachments/assets/d1541701-72e6-46ea-a92d-1de7d0a93ab9)

This page is designed to provide a comprehensive overview of the driver's profile and vehicle information. Here are the key sections and functionalities:

1. **Driver's Information:**
   - Includes important details about the driver and personal information.

2. **Driving Points:**
   - Shows the total driving points accumulated by the driver, used to become a SuperDriver (15 points).

3. **Drive Statistics Button:**
   - Provides access to detailed statistics about the driver's trips, including driving price, time spent driving, and number of drives.

4. **Next Drive Timer:**
   - A countdown timer showing the time remaining until the next scheduled drive.

5. **Request Break:**
   - A button for the driver to request a break.

6. **Vehicle Information:**
   - Includes details about the registered vehicle.

7. **Dismiss Vehicle:**
   - A button to unregister a vehicle.
