USE [master]
GO
/****** Object:  Database [Driving License Management]    Script Date: 22/05/2025 15:26:28 ******/
CREATE DATABASE [Driving License Management]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Driver License Management', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Driver License Management.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Driver License Management_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Driver License Management_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Driving License Management] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Driving License Management].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Driving License Management] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Driving License Management] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Driving License Management] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Driving License Management] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Driving License Management] SET ARITHABORT OFF 
GO
ALTER DATABASE [Driving License Management] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Driving License Management] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Driving License Management] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Driving License Management] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Driving License Management] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Driving License Management] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Driving License Management] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Driving License Management] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Driving License Management] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Driving License Management] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Driving License Management] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Driving License Management] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Driving License Management] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Driving License Management] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Driving License Management] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Driving License Management] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Driving License Management] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Driving License Management] SET RECOVERY FULL 
GO
ALTER DATABASE [Driving License Management] SET  MULTI_USER 
GO
ALTER DATABASE [Driving License Management] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Driving License Management] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Driving License Management] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Driving License Management] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Driving License Management] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Driving License Management] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Driving License Management', N'ON'
GO
ALTER DATABASE [Driving License Management] SET QUERY_STORE = ON
GO
ALTER DATABASE [Driving License Management] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Driving License Management]
GO
/****** Object:  Table [dbo].[People]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[NationalNo] [nvarchar](20) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[SecondName] [nvarchar](20) NOT NULL,
	[ThirdName] [nvarchar](20) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[NationalityCountry_id] [int] NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestAppointments]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestAppointments](
	[TestAppointment_ID] [int] IDENTITY(1,1) NOT NULL,
	[TestType_id] [int] NOT NULL,
	[LocalDrivingLicense_id] [int] NOT NULL,
	[AppointmentDate] [datetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[IsLocked] [bit] NOT NULL,
	[CreatedByUser_id] [int] NOT NULL,
	[RetakeTestApplicationID] [int] NULL,
 CONSTRAINT [PK_TestAppointments] PRIMARY KEY CLUSTERED 
(
	[TestAppointment_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[Test_ID] [int] IDENTITY(1,1) NOT NULL,
	[TestAppointment_id] [int] NOT NULL,
	[Result] [bit] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedByUser_id] [int] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[Test_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseClasses]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseClasses](
	[LicenseClass_ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](max) NOT NULL,
	[ClassDescription] [nvarchar](max) NOT NULL,
	[MinimumAllowedAge] [tinyint] NOT NULL,
	[ValidityLength] [tinyint] NOT NULL,
	[ClassFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_LicenseClasses] PRIMARY KEY CLUSTERED 
(
	[LicenseClass_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[Application_ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationType_id] [int] NOT NULL,
	[Person_id] [int] NOT NULL,
	[CreatedByUser_id] [int] NOT NULL,
	[ApplicationDate] [datetime] NOT NULL,
	[ApplicationStatus] [tinyint] NOT NULL,
	[LastStatusDate] [datetime] NOT NULL,
	[PaidFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[Application_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocalDrivingLicenseApplications]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocalDrivingLicenseApplications](
	[LocalDrivingLicenseApplication_ID] [int] IDENTITY(1,1) NOT NULL,
	[Application_id] [int] NOT NULL,
	[LicenseClass_id] [int] NOT NULL,
 CONSTRAINT [PK_LocalDrivingLicenseApplications] PRIMARY KEY CLUSTERED 
(
	[LocalDrivingLicenseApplication_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[LocalDrivingLicenseApplications_View]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[LocalDrivingLicenseApplications_View]
AS
SELECT        dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplication_ID, dbo.LicenseClasses.ClassName, dbo.People.PersonID, dbo.People.NationalNo, 
                         dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + dbo.People.ThirdName + ' ' + dbo.People.LastName AS FullName, dbo.Applications.ApplicationDate,
                             (SELECT        COUNT(dbo.TestAppointments.TestType_id) AS PassedTestCount
                               FROM            dbo.Tests INNER JOIN
                                                         dbo.TestAppointments ON dbo.Tests.TestAppointment_id = dbo.TestAppointments.TestAppointment_ID
                               WHERE        (dbo.TestAppointments.LocalDrivingLicense_id = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplication_ID) AND (dbo.Tests.Result = 1)) AS PassedTestCount, 
                         CASE WHEN Applications.ApplicationStatus = 1 THEN 'New' WHEN Applications.ApplicationStatus = 2 THEN 'Canceled' WHEN Applications.ApplicationStatus = 3 THEN 'Completed' END AS Status
FROM            dbo.LocalDrivingLicenseApplications INNER JOIN
                         dbo.Applications ON dbo.LocalDrivingLicenseApplications.Application_id = dbo.Applications.Application_ID INNER JOIN
                         dbo.LicenseClasses ON dbo.LocalDrivingLicenseApplications.LicenseClass_id = dbo.LicenseClasses.LicenseClass_ID INNER JOIN
                         dbo.People ON dbo.Applications.Person_id = dbo.People.PersonID
GO
/****** Object:  Table [dbo].[TestTypes]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTypes](
	[TestType_ID] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeTitle] [nvarchar](50) NOT NULL,
	[TestTypeDescription] [nvarchar](max) NOT NULL,
	[TestTypeFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_TestTypes] PRIMARY KEY CLUSTERED 
(
	[TestType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[TestAppointments_View]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TestAppointments_View]
AS
SELECT        dbo.TestAppointments.TestAppointment_ID, dbo.TestAppointments.LocalDrivingLicense_id, dbo.LicenseClasses.ClassName, dbo.TestTypes.TestTypeTitle, 
                         dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + dbo.People.ThirdName + ' ' + dbo.People.LastName AS FullName, dbo.TestAppointments.AppointmentDate, dbo.TestAppointments.PaidFees, 
                         dbo.TestAppointments.IsLocked
FROM            dbo.LicenseClasses INNER JOIN
                         dbo.TestTypes INNER JOIN
                         dbo.TestAppointments ON dbo.TestTypes.TestType_ID = dbo.TestAppointments.TestType_id INNER JOIN
                         dbo.Applications INNER JOIN
                         dbo.People ON dbo.Applications.Person_id = dbo.People.PersonID INNER JOIN
                         dbo.LocalDrivingLicenseApplications ON dbo.Applications.Application_ID = dbo.LocalDrivingLicenseApplications.Application_id ON 
                         dbo.TestAppointments.LocalDrivingLicense_id = dbo.LocalDrivingLicenseApplications.LocalDrivingLicenseApplication_ID ON dbo.LicenseClasses.LicenseClass_ID = dbo.LocalDrivingLicenseApplications.LicenseClass_id
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[License_ID] [int] IDENTITY(1,1) NOT NULL,
	[Application_id] [int] NOT NULL,
	[Driver_id] [int] NOT NULL,
	[LicenseClass_id] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[PaidFees] [smallmoney] NOT NULL,
	[IssueReason] [tinyint] NOT NULL,
	[CreatedByUser_id] [int] NOT NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED 
(
	[License_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Drivers](
	[Driver_ID] [int] IDENTITY(1,1) NOT NULL,
	[Person_id] [int] NOT NULL,
	[CreatedByUser] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Drivers] PRIMARY KEY CLUSTERED 
(
	[Driver_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Drivers_View]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Drivers_View]
AS
SELECT        dbo.Drivers.Driver_ID, dbo.People.NationalNo, dbo.People.FirstName + N' ' + dbo.People.SecondName + N' ' + dbo.People.ThirdName + N' ' + dbo.People.LastName AS [Full Name], dbo.Drivers.CreatedDate,
                             (SELECT        COUNT(License_ID) AS Expr1
                               FROM            dbo.Licenses
                               WHERE        (IsActive = 1) AND (Driver_id = dbo.Drivers.Driver_ID)) AS NumberOfActiveLicenses, dbo.Drivers.Person_id
FROM            dbo.Drivers INNER JOIN
                         dbo.People ON dbo.Drivers.Person_id = dbo.People.PersonID
GO
/****** Object:  Table [dbo].[DetainedLicenses]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetainedLicenses](
	[DetainID] [int] IDENTITY(1,1) NOT NULL,
	[LicenseID] [int] NOT NULL,
	[DetainDate] [datetime] NOT NULL,
	[FineFees] [smallmoney] NOT NULL,
	[CreatedByUserID] [int] NOT NULL,
	[IsReleased] [bit] NOT NULL,
	[ReleaseDate] [datetime] NULL,
	[ReleasedByUserID] [int] NULL,
	[ReleaseApplicationID] [int] NULL,
 CONSTRAINT [PK_DetainedLicenses] PRIMARY KEY CLUSTERED 
(
	[DetainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[DetainedLicenses_View]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DetainedLicenses_View]
AS
SELECT        TOP (100) PERCENT dbo.DetainedLicenses.DetainID, dbo.DetainedLicenses.LicenseID, dbo.DetainedLicenses.DetainDate, dbo.DetainedLicenses.IsReleased, dbo.DetainedLicenses.FineFees, 
                         dbo.DetainedLicenses.ReleaseDate, dbo.People.NationalNo, dbo.People.FirstName + ' ' + dbo.People.SecondName + ' ' + dbo.People.ThirdName + ' ' + dbo.People.LastName AS FullName, 
                         dbo.DetainedLicenses.ReleaseApplicationID
FROM            dbo.People INNER JOIN
                         dbo.Drivers ON dbo.People.PersonID = dbo.Drivers.Person_id INNER JOIN
                         dbo.Licenses ON dbo.Drivers.Driver_ID = dbo.Licenses.Driver_id RIGHT OUTER JOIN
                         dbo.DetainedLicenses ON dbo.Licenses.License_ID = dbo.DetainedLicenses.LicenseID
ORDER BY dbo.DetainedLicenses.IsReleased, dbo.DetainedLicenses.DetainID
GO
/****** Object:  Table [dbo].[Application Types]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application Types](
	[ApplicationType_ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationType_Title] [nvarchar](50) NOT NULL,
	[ApplicationFees] [smallmoney] NOT NULL,
 CONSTRAINT [PK_Application Types] PRIMARY KEY CLUSTERED 
(
	[ApplicationType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[CountryID] [int] NOT NULL,
	[CountryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Countrie] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InternationalLicenses]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InternationalLicenses](
	[InternationalLicense_ID] [int] IDENTITY(1,1) NOT NULL,
	[LocalLicense_id] [int] NOT NULL,
	[Driver_id] [int] NOT NULL,
	[Application_id] [int] NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedByUser_id] [int] NOT NULL,
 CONSTRAINT [PK_InternationalLicenses] PRIMARY KEY CLUSTERED 
(
	[InternationalLicense_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22/05/2025 15:26:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Person_id] [int] NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastPasswordChange] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [LastPasswordChange]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Application Types] FOREIGN KEY([ApplicationType_id])
REFERENCES [dbo].[Application Types] ([ApplicationType_ID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Application Types]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_People] FOREIGN KEY([Person_id])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_People]
GO
ALTER TABLE [dbo].[Applications]  WITH CHECK ADD  CONSTRAINT [FK_Applications_Users] FOREIGN KEY([CreatedByUser_id])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Applications] CHECK CONSTRAINT [FK_Applications_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Applications] FOREIGN KEY([ReleaseApplicationID])
REFERENCES [dbo].[Applications] ([Application_ID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Applications]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Licenses] FOREIGN KEY([LicenseID])
REFERENCES [dbo].[Licenses] ([License_ID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Licenses]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users] FOREIGN KEY([CreatedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users]
GO
ALTER TABLE [dbo].[DetainedLicenses]  WITH CHECK ADD  CONSTRAINT [FK_DetainedLicenses_Users1] FOREIGN KEY([ReleasedByUserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[DetainedLicenses] CHECK CONSTRAINT [FK_DetainedLicenses_Users1]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_People] FOREIGN KEY([Person_id])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_People]
GO
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK_Drivers_Users] FOREIGN KEY([CreatedByUser])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK_Drivers_Users]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Applications] FOREIGN KEY([Application_id])
REFERENCES [dbo].[Applications] ([Application_ID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Applications]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Drivers] FOREIGN KEY([Driver_id])
REFERENCES [dbo].[Drivers] ([Driver_ID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Drivers]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Licenses] FOREIGN KEY([LocalLicense_id])
REFERENCES [dbo].[Licenses] ([License_ID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Licenses]
GO
ALTER TABLE [dbo].[InternationalLicenses]  WITH CHECK ADD  CONSTRAINT [FK_InternationalLicenses_Users] FOREIGN KEY([CreatedByUser_id])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[InternationalLicenses] CHECK CONSTRAINT [FK_InternationalLicenses_Users]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Applications] FOREIGN KEY([Application_id])
REFERENCES [dbo].[Applications] ([Application_ID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Applications]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Drivers] FOREIGN KEY([Driver_id])
REFERENCES [dbo].[Drivers] ([Driver_ID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Drivers]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_LicenseClasses] FOREIGN KEY([LicenseClass_id])
REFERENCES [dbo].[LicenseClasses] ([LicenseClass_ID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_LicenseClasses]
GO
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Users] FOREIGN KEY([CreatedByUser_id])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Users]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_LocalDrivingLicenseApplications_Applications] FOREIGN KEY([Application_id])
REFERENCES [dbo].[Applications] ([Application_ID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_LocalDrivingLicenseApplications_Applications]
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications]  WITH CHECK ADD  CONSTRAINT [FK_LocalDrivingLicenseApplications_LicenseClasses] FOREIGN KEY([LicenseClass_id])
REFERENCES [dbo].[LicenseClasses] ([LicenseClass_ID])
GO
ALTER TABLE [dbo].[LocalDrivingLicenseApplications] CHECK CONSTRAINT [FK_LocalDrivingLicenseApplications_LicenseClasses]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Countries] FOREIGN KEY([NationalityCountry_id])
REFERENCES [dbo].[Countries] ([CountryID])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Countries]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications] FOREIGN KEY([LocalDrivingLicense_id])
REFERENCES [dbo].[LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplication_ID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_LocalDrivingLicenseApplications]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_TestTypes] FOREIGN KEY([TestType_id])
REFERENCES [dbo].[TestTypes] ([TestType_ID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_TestTypes]
GO
ALTER TABLE [dbo].[TestAppointments]  WITH CHECK ADD  CONSTRAINT [FK_TestAppointments_Users] FOREIGN KEY([CreatedByUser_id])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[TestAppointments] CHECK CONSTRAINT [FK_TestAppointments_Users]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestAppointments] FOREIGN KEY([TestAppointment_id])
REFERENCES [dbo].[TestAppointments] ([TestAppointment_ID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestAppointments]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Users] FOREIGN KEY([CreatedByUser_id])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_People] FOREIGN KEY([Person_id])
REFERENCES [dbo].[People] ([PersonID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_People]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 - New | 2 - Canceled | 3 - Completed' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Applications', @level2type=N'COLUMN',@level2name=N'ApplicationStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DetainedLicenses"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Licenses"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 136
               Right = 457
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Drivers"
            Begin Extent = 
               Top = 6
               Left = 495
               Bottom = 136
               Right = 665
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 703
               Bottom = 136
               Right = 908
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DetainedLicenses_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DetainedLicenses_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Drivers"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 451
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Drivers_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Drivers_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Applications"
            Begin Extent = 
               Top = 6
               Left = 347
               Bottom = 136
               Right = 537
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LicenseClasses"
            Begin Extent = 
               Top = 6
               Left = 575
               Bottom = 136
               Right = 781
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 819
               Bottom = 136
               Right = 1024
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LocalDrivingLicenseApplications"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 309
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LocalDrivingLicenseApplications_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LocalDrivingLicenseApplications_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TestTypes"
            Begin Extent = 
               Top = 15
               Left = 78
               Bottom = 145
               Right = 273
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TestAppointments"
            Begin Extent = 
               Top = 6
               Left = 1302
               Bottom = 136
               Right = 1519
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "LicenseClasses"
            Begin Extent = 
               Top = 6
               Left = 506
               Bottom = 136
               Right = 712
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "People"
            Begin Extent = 
               Top = 6
               Left = 1059
               Bottom = 136
               Right = 1264
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "Applications"
            Begin Extent = 
               Top = 6
               Left = 311
               Bottom = 136
               Right = 501
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LocalDrivingLicenseApplications"
            Begin Extent = 
               Top = 6
               Left = 750
               Bottom = 119
               Right = 1021
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestAppointments_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N' = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestAppointments_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'TestAppointments_View'
GO
USE [master]
GO
ALTER DATABASE [Driving License Management] SET  READ_WRITE 
GO
