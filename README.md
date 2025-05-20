# Driver License Management System

A comprehensive Windows Forms application for managing driver licenses, applications, and related services. This system helps automate and streamline the process of managing driver licenses, from initial applications to renewals and replacements.

## Features

### License Management

- Issue new driver licenses
- Renew existing licenses
- Replace lost or damaged licenses
- Detain and release licenses
- Track license history
- Manage license classes and fees

### Application Processing

- Process new license applications
- Schedule and manage tests (Vision, Written, Practical)
- Track application status
- Filter and search applications
- Manage application fees

### User Management

- User authentication and authorization
- Role-based access control
- User activity tracking
- Secure password management

### Reporting

- License status reports
- Application statistics
- Test results tracking
- Financial reports

## Technical Details

### Built With

- C# Windows Forms (.NET Framework)
- SQL Server Database
- Three-Tier Architecture:
  - Presentation Layer (Windows Forms)
  - Business Layer (Class Library)
  - Data Access Layer (Class Library)

### Project Structure

```
Driver License Management Project/
├── Applications/
│   ├── ManageApplications/
│   │   ├── Local Driving License Application/
│   │   └── International License Application/
│   └── Driving Licenses Services/
│       ├── Renew/
│       ├── Replacement/
│       └── Release Detained License/
├── Licenses/
│   ├── Detain License/
│   └── Local Licenses/
├── People/
│   └── Manage People/
├── Business Layer/
│   └── [Business Logic Classes]
├── Data Access Layer/
│   └── [Data Access Classes]
└── GlobalClasses/
    └── [Shared Utilities]
```

## Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server 2019 or later

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/yourusername/driver-license-management.git
   ```
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Update the database connection string in the configuration
5. Build and run the application

### Database Setup

1. Run the provided SQL scripts in the `Database` folder
2. Update the connection string in the application configuration
3. Ensure the database user has appropriate permissions

## Usage

### User Roles

- Administrator: Full system access
- Manager: Application and license management
- Operator: Basic operations and data entry

### Key Workflows

1. New License Application

   - Create new application
   - Schedule required tests
   - Process test results
   - Issue license upon completion

2. License Renewal

   - Verify eligibility
   - Process renewal application
   - Update license details
   - Issue new license

3. License Replacement

   - Verify license status
   - Process replacement request
   - Issue replacement license

4. License Detention
   - Record detention reason
   - Process fine fees
   - Update license status
   - Handle release process

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

- [List any acknowledgments, credits, or inspiration]

## Contact

Your Name - [example@gmail.com](mailto:ahmedashraf01085@gmail.com)
Project Link: [https://github.com/yourusername/driver-license-management](https://github.com/yourusername/driver-license-management)
