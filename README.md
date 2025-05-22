# 🚗 Driver License Management System

A comprehensive **Windows Forms application** for managing driver licenses, applications, and related administrative tasks.  
This system provides a robust solution for driving license authorities to manage licenses, applications, tests, and driver records efficiently.

---

## ✨ Features

### 👤 User Management

- Secure user authentication and authorization
- Role-based access control
- User profile management

### 👥 Driver Management

- Driver registration and profile management
- Driver history tracking
- License status monitoring

### 🪪 License Management

- Local driving license processing
- International license management
- License renewal and updates
- License Replacement For Damaged OR Lost
- License Detain and Release
- License class management

### 📄 Application Processing

- New license applications
- License renewal applications
- Application status tracking
- Fee management

### 🧪 Test Management

- Test scheduling and appointments
- Different test types (Vission, Theory, Practical)
- Test results tracking
- Test type management

### 📊 Reporting

- License status reports
- Application statistics
- Test results reports
- Licenses Driver history reports

---

## 🛠️ Prerequisites

- .NET Framework
- SQL Server
- Windows Operating System
- Visual Studio 2019 or later

---

## 🚀 Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Div-Ahmed-Ashraf/Driver-License-Management-System.git
   ```

2. Create a new SQL Server database named "Driving License Management"

3. Run the database scripts in the `Database` folder to set up the database schema

4. Copy `appsettings.example.json` to `appsettings.json` and update the connection string:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=Driving License Management;User Id=YourUsername;Password=YourPassword;"
     }
   }
   ```

5. Open the solution in Visual Studio and restore NuGet packages

6. Build and run the application

## Backup

A full backup of the database is saved as `Project_Database_BackUp.bak`.  
You can restore it via SQL Server Management Studio if needed.

## Configuration

The application uses `appsettings.json` for configuration. The following settings can be modified:

- Database connection string
- Application settings (if added in future versions)

## Contributing

Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Version History

See [CHANGELOG.md](CHANGELOG.md) for a list of changes and version history.

## Acknowledgments

- Icons provided by Project_Icons directory
- Database design and implementation
- All contributors who have helped shape this project

## Contact

For any queries or support, please open an issue in the [GitHub repository](https://github.com/Dev-Ahmed-Ashraf/Driver-License-Management-System/issues) or contact Ahmed Ashraf.

## Screenshots

Check the `ScreenShots` directory for application screenshots and usage examples.