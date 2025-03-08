# Gym Membership Management System

A .NET MAUI application for managing gym memberships, built with .NET 8 and SQL Server.

## Features

- View all gym members with status indicators
- Add new gym members
- Edit existing member information
- Delete members from the system
- View detailed member information
- Track membership types and expiry dates

## Technology Stack

- .NET MAUI (Multi-platform App UI) targeting .NET 8
- SQL Server database
- ADO.NET for database connectivity
- MVVM architectural pattern

## Project Structure

```
GymMembershipApp/
│
├── App.xaml                  # Application resources
├── AppShell.xaml             # Application shell/navigation
├── MauiProgram.cs            # MAUI configuration
│
├── Converters/               # Value converters
│   └── BoolToColorConverter.cs  # Convert boolean values to UI colors
│
├── Helpers/                  # Helper classes
│   └── ConfigHelper.cs       # Database connection management
│
├── Models/                   # Data models
│   └── Member.cs             # Member entity class
│
├── Services/                 # Business logic and services
│   └── DatabaseService.cs    # Database CRUD operations
│
├── Views/                    # UI pages
│   └── MemberDetailPage.xaml # Member detail/edit page
│
└── MainPage.xaml             # Main members list page
```

## Setup Instructions

### Database Setup

1. Create a new database named `GymMemberDB` in SQL Server
2. Execute the following SQL script to create the necessary table:

```sql
-- Create Member Table
CREATE TABLE Members (
    MemberID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(255),
    DateOfBirth DATE,
    MembershipType NVARCHAR(50) NOT NULL,
    JoinDate DATE DEFAULT GETDATE(),
    ExpiryDate DATE,
    ActiveStatus BIT DEFAULT 1
);
```

3. (Optional) Add sample data:

```sql
-- Add sample data
INSERT INTO Members (FirstName, LastName, Email, PhoneNumber, Address, DateOfBirth, MembershipType, JoinDate, ExpiryDate, ActiveStatus)
VALUES 
('John', 'Doe', 'john.doe@example.com', '123-456-7890', '123 Main St', '1990-05-15', 'Premium', '2023-01-15', '2024-01-15', 1),
('Jane', 'Smith', 'jane.smith@example.com', '098-765-4321', '456 Oak Ave', '1985-09-23', 'Standard', '2023-02-20', '2023-08-20', 1),
('Mike', 'Johnson', 'mike.johnson@example.com', '555-123-4567', '789 Pine Blvd', '1995-11-07', 'Basic', '2023-03-10', '2023-06-10', 0);
```

### Application Setup

1. Clone this repository
2. Open the solution in Visual Studio 2022
3. Update the connection string in `Helpers/ConfigHelper.cs` to point to your SQL Server:

```csharp
private static readonly string ConnectionString = "Data Source=YOUR-SERVER-NAME;Initial Catalog=GymMemberDB;Integrated Security=True";
```

4. Build and run the application

## Building for Android

### Prerequisites

- Android SDK installed (included with Visual Studio 2022)
- Android build tools
- Java Development Kit (JDK)

### Build Steps

1. Right-click on the project in Solution Explorer
2. Select "Properties"
3. Go to the "Android Manifest" tab
4. Configure package name and application details
5. Build the project in Release mode
6. The APK will be generated in the `bin/Release/net8.0-android` folder

## Debugging on Android Device

1. Enable USB debugging on your Android device
2. Connect your device to your computer
3. In Visual Studio, select your device from the deployment target dropdown
4. Press F5 to build, deploy, and debug

Alternatively, using Android Studio:
1. Connect your device to your computer
2. Verify connection using Android Studio's Device Manager
3. In Visual Studio, ensure ADB can see your device
4. Build and deploy from Visual Studio

## Future Enhancements

- Search and filter functionality for members
- Payment tracking
- Attendance monitoring
- Reporting and analytics
- User authentication for staff
- Cloud synchronization

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Microsoft .NET MAUI team for the framework
- SQL Server team for the database engine

## Contact

For any queries or feedback, please contact:

Your Name - your.email@example.com
