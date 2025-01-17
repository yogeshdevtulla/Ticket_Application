USE [master]
GO
/****** Object:  Database [Masters]    Script Date: 1/15/2025 11:12:46 AM ******/
CREATE DATABASE [Masters]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Masters', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Masters.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Masters_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Masters_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Masters] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Masters].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Masters] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Masters] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Masters] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Masters] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Masters] SET ARITHABORT OFF 
GO
ALTER DATABASE [Masters] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Masters] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Masters] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Masters] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Masters] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Masters] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Masters] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Masters] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Masters] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Masters] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Masters] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Masters] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Masters] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Masters] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Masters] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Masters] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Masters] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Masters] SET RECOVERY FULL 
GO
ALTER DATABASE [Masters] SET  MULTI_USER 
GO
ALTER DATABASE [Masters] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Masters] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Masters] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Masters] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Masters] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Masters] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Masters', N'ON'
GO
ALTER DATABASE [Masters] SET QUERY_STORE = ON
GO
ALTER DATABASE [Masters] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Masters]
GO
/****** Object:  UserDefinedFunction [dbo].[CalculateWorkingHours]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Function to calculate working hours
Create FUNCTION [dbo].[CalculateWorkingHours](@StartDate DATETIME, @EndDate DATETIME)
RETURNS INT
AS
BEGIN
    DECLARE @WorkingHours INT = 0;

    -- Initialize loop variables
    DECLARE @CurrentDate DATETIME = @StartDate;

    WHILE @CurrentDate <= @EndDate
    BEGIN
        -- Exclude weekends
        IF DATEPART(WEEKDAY, @CurrentDate) NOT IN (1, 7) -- 1=Sunday, 7=Saturday
        BEGIN
            -- Count hours only within working hours (9 AM to 6 PM)
            IF DATEPART(HOUR, @CurrentDate) BETWEEN 9 AND 17
                SET @WorkingHours = @WorkingHours + 1;
        END

        -- Increment by 1 hour
        SET @CurrentDate = DATEADD(HOUR, 1, @CurrentDate);
    END

    RETURN @WorkingHours;
END

GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[DepartmentName] [varchar](100) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedIP] [varchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [varchar](100) NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedIP] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogException]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogException](
	[ErrorID] [int] IDENTITY(1,1) NOT NULL,
	[ErrorDate] [datetime] NULL,
	[Error] [nvarchar](max) NULL,
	[Source] [nvarchar](255) NULL,
	[Message] [nvarchar](max) NULL,
	[ErrorType] [nvarchar](255) NULL,
	[StackType] [nvarchar](max) NULL,
	[Created_By] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ErrorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMaster]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMaster](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubCategory]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubCategory](
	[SubCategoryID] [int] IDENTITY(1,1) NOT NULL,
	[SubCategoryName] [nvarchar](100) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[Status] [nvarchar](10) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedIP] [nvarchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedIP] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SubCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[TicketNo] [nvarchar](50) NOT NULL,
	[DepartmentID] [int] NULL,
	[CategoryID] [int] NULL,
	[SubCategoryID] [int] NULL,
	[Priority] [nvarchar](50) NULL,
	[Feedback] [nvarchar](max) NULL,
	[Status] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[CreateIP] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMaster]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMaster](
	[UserMasterID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[MiddleName] [varchar](100) NULL,
	[LastName] [varchar](100) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[Designation] [varchar](100) NOT NULL,
	[MobileNo] [varchar](15) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Status] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserMasterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_UserMaster_UserID] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[LogException] ADD  DEFAULT (getdate()) FOR [ErrorDate]
GO
ALTER TABLE [dbo].[LogException] ADD  DEFAULT ('System') FOR [Created_By]
GO
ALTER TABLE [dbo].[SubCategory] ADD  DEFAULT ('ACTIVE') FOR [Status]
GO
ALTER TABLE [dbo].[SubCategory] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT ('Open') FOR [Status]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Tickets] ADD  DEFAULT ('127.0.0.1') FOR [CreateIP]
GO
ALTER TABLE [dbo].[SubCategory]  WITH NOCHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[SubCategory]  WITH CHECK ADD FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD FOREIGN KEY([SubCategoryID])
REFERENCES [dbo].[SubCategory] ([SubCategoryID])
GO
ALTER TABLE [dbo].[UserMaster]  WITH CHECK ADD FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[UserMaster]  WITH CHECK ADD  CONSTRAINT [FK__UserMaste__UserI__72C60C4A] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[UserMaster] CHECK CONSTRAINT [FK__UserMaste__UserI__72C60C4A]
GO
/****** Object:  StoredProcedure [dbo].[AddOrUpdateRoleMaster]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddOrUpdateRoleMaster]
    @RoleID INT = NULL,
    @RoleName NVARCHAR(100),
    @Status NVARCHAR(20)
    
AS
BEGIN
    BEGIN TRY
        IF @RoleID IS NULL
        BEGIN
            -- Insert new role
            INSERT INTO RoleMaster (RoleName, Status)
            VALUES (@RoleName, @Status);
        END
        ELSE
        BEGIN
            -- Update existing role
            UPDATE RoleMaster
            SET RoleName = @RoleName,
                Status = @Status
            WHERE RoleID = @RoleID;
        END
    END TRY
    BEGIN CATCH
        -- Log error using the passed user context
        EXEC [dbo].[LogError] System;
        THROW; 
    END CATCH
END;


GO
/****** Object:  StoredProcedure [dbo].[AddTicket]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTicket]
    @DepartmentID INT,
    @CategoryID INT,
    @SubCategoryID INT,
    @Priority NVARCHAR(50),
    @Feedback NVARCHAR(MAX),
    @CreateIP NVARCHAR(50)
AS
BEGIN
    BEGIN TRY
       
        DECLARE @TicketNo NVARCHAR(50);
        SET @TicketNo = CONCAT('BSL-', (SELECT ISNULL(MAX(CAST(SUBSTRING(TicketNo, 5, LEN(TicketNo)) AS INT)), 0) + 1 FROM Tickets));
        
        INSERT INTO Tickets (TicketNo, DepartmentID, CategoryID, SubCategoryID, Priority, Feedback, CreateIP)
        VALUES (@TicketNo, @DepartmentID, @CategoryID, @SubCategoryID, @Priority, @Feedback, @CreateIP);

        SELECT 'Ticket Added Successfully!' AS Message;
    END TRY
    BEGIN CATCH

        EXEC [dbo].[LogError] System;
        THROW; 
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteCategory]
    @CategoryID INT
AS
BEGIN
    BEGIN TRY
        DELETE FROM dbo.Category WHERE CategoryID = @CategoryID;
		
    END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteDepartment]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteDepartment]
    @Id INT
	
AS
BEGIN
    BEGIN TRY
    DELETE FROM Departments
    END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteRoleMaster]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteRoleMaster]
    @RoleID INT
AS
BEGIN
    BEGIN TRY

    DELETE FROM RoleMaster WHERE RoleID = @RoleID;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteSubCategory]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteSubCategory]
    @SubCategoryID INT
AS
BEGIN
    BEGIN TRY

    DELETE FROM SubCategory WHERE SubCategoryID = @SubCategoryID;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteUser]
    @UserID INT
AS
BEGIN
    BEGIN TRY

    DELETE FROM Users WHERE UserID = @UserID;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteUserMaster]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteUserMaster] 
    @UserID INT
AS
BEGIN
    BEGIN TRY

    DELETE FROM UserMaster WHERE UserID = @UserID;

    -- Delete the record from the Users table
    DELETE FROM Users WHERE UserID = @UserID;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[EditUserMaster]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EditUserMaster]
    @UserID INT,
    @FirstName VARCHAR(100),
    @MiddleName VARCHAR(100),
    @LastName VARCHAR(100),
    @Role VARCHAR(50),
    @Password VARCHAR(255),
    @DepartmentID INT,
    @Designation VARCHAR(100),
    @MobileNo VARCHAR(15),
    @Email VARCHAR(100),
    @Status VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        UPDATE Users
        SET 
            Username = ISNULL(@FirstName, '') + ISNULL(@MiddleName, '') + ISNULL(@LastName, ''),
            Password = @Password
        WHERE UserID = @UserID;

        UPDATE UserMaster
        SET
            FirstName = @FirstName,
            MiddleName = @MiddleName,
            LastName = @LastName,
            Role = @Role,
            Password = @Password,
            DepartmentID = @DepartmentID,
            Designation = @Designation,
            MobileNo = @MobileNo,
            Email = @Email,
            Status = @Status
        WHERE UserID = @UserID;
    END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllCategories]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllCategories]

AS
BEGIN
    BEGIN TRY
        SELECT CategoryID, CategoryName, DepartmentName, Status
        FROM dbo.Category;
    END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllDepartments]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------Create stored procedure for Fetch values

CREATE PROCEDURE [dbo].[GetAllDepartments]

AS
BEGIN
    BEGIN TRY

    SELECT * FROM Departments
	END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;;
        THROW;
    END CATCH
END;


GO
/****** Object:  StoredProcedure [dbo].[GetAllRoles]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllRoles]

AS
BEGIN
    BEGIN TRY

    SELECT RoleID, RoleName, Status FROM RoleMaster;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllSubCategories]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllSubCategories]
AS
BEGIN
    BEGIN TRY
    SELECT SC.SubCategoryID, SC.SubCategoryName, 
           C.CategoryName, D.DepartmentName, SC.Status
    FROM SubCategory SC
    INNER JOIN Category C ON SC.CategoryID = C.CategoryID
    INNER JOIN Departments D ON SC.DepartmentID = D.ID;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllTickets]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllTickets]
AS
BEGIN
    BEGIN TRY
    SELECT 
        TicketNo, DepartmentID, CategoryID, SubCategoryID, Priority, Feedback, Status, cast(CreateDate as date) CreateDate  , CreateIP
    FROM Tickets
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;;
        THROW;
    END CATCH
END;

GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllUsers]
AS
BEGIN
    SELECT 
        u.UserID,
		u.Username, 
       u.FirstName,
	   u.MiddleName,
	   u.LastName,
	   u.Role,

        d.DepartmentName ,
        u.MobileNo,
        u.Email,
        u.Status
    FROM Users u
    INNER JOIN Departments d ON u.DepartmentID = d.Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetCategoryById]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCategoryById]
    @CategoryID INT
AS
BEGIN
    BEGIN TRY
    SELECT * FROM Category WHERE CategoryID = @CategoryID;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetDashboardData]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetDashboardData]

AS
BEGIN
    BEGIN TRY
    SELECT 
        (SELECT COUNT(*) FROM Tickets WHERE Status = 'Open') AS OpenTickets,
        (SELECT COUNT(*) FROM Tickets WHERE Status = 'Closed') AS ClosedTickets,
		(SELECT COUNT(*) FROM Tickets WHERE Status = 'Resolved') AS ResolvedTickets,
        (SELECT COUNT(*) FROM Users) AS TotalUsers; -- Assuming a Users table
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetOpenTickets]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetOpenTickets]

AS
BEGIN
    BEGIN TRY
    SELECT TicketNo
    FROM Tickets
    WHERE Status = 'Open'
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetRoleById]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRoleById]
    @RoleID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT RoleID, RoleName, Status
    FROM RoleMaster
    WHERE RoleID = @RoleID;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSubCategoryById]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSubCategoryById]
    @SubCategoryID INT
AS
BEGIN
    BEGIN TRY
    SELECT 
        sc.SubCategoryID, 
        sc.SubCategoryName, 
        sc.CategoryID, 
        c.CategoryName, 
        sc.DepartmentID, 
        d.DepartmentName, 
        sc.Status
    FROM SubCategory sc
    INNER JOIN Category c ON sc.CategoryID = c.CategoryID
    INNER JOIN Departments d ON sc.DepartmentID = d.Id
    WHERE sc.SubCategoryID = @SubCategoryID;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTATReport]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetTATReport]

AS
BEGIN
    BEGIN TRY

    -- Calculate working hours between two dates
    WITH WorkingHoursCTE AS (
        SELECT 
            t.CreateDate AS CreatedDate,                         
            t.UpdatedDate AS ClosingDate,                       
            t.TicketNo AS TicketNumber,                         
            d.DepartmentName AS Department,                     
            c.CategoryName AS Category,                         
            sc.SubCategoryName AS Subcategory,                  
            t.Status,                                           
            dbo.CalculateWorkingHours(t.CreateDate, t.UpdatedDate) AS TAT -- Calculate TAT in working hours
        FROM Tickets t
        LEFT JOIN Departments d ON t.DepartmentID = d.Id 
        LEFT JOIN Category c ON t.CategoryID = c.CategoryID 
        LEFT JOIN SubCategory sc ON t.SubCategoryID = sc.SubCategoryID
    )
    SELECT 
        CreatedDate,
        ClosingDate,
        TicketNumber,
        Department,
        Category,
        Subcategory,
        Status,
        CASE 
            WHEN TAT > 24 THEN CAST(TAT / 9 AS VARCHAR) + ' days'
            ELSE CAST(TAT AS VARCHAR) + ' hours'
        END AS TAT
    FROM WorkingHoursCTE
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTicketByNo]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTicketByNo]
    @TicketNo NVARCHAR(50)
AS
BEGIN
    BEGIN TRY
    SELECT 
        TicketNo, DepartmentID, CategoryID, SubCategoryID, Priority, Feedback, Status, CreateDate, CreateIP
    FROM Tickets
    WHERE TicketNo = @TicketNo;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTicketByTicketNo]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTicketByTicketNo]
    @TicketNo VARCHAR(50)
AS
BEGIN
    BEGIN TRY

    SELECT 
        t.TicketNo, 
        t.CreateDate, 
        d.DepartmentName, 
        c.CategoryName, 
        sc.SubCategoryName, 
        t.Status, 
        t.Feedback
    FROM Tickets t
    INNER JOIN Departments d ON t.DepartmentID = d.Id
    INNER JOIN category c ON t.CategoryID = c.CategoryID
    INNER JOIN SubCategory sc ON t.SubCategoryID = sc.SubCategoryID
    WHERE t.TicketNo = @TicketNo
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTickets]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTickets]
AS
BEGIN
    BEGIN TRY
    SELECT 
        t.CreateDate,
        t.TicketNo,
        d.DepartmentName,
        c.CategoryName,
        sc.SubCategoryName,
        t.Status
    FROM 
        Tickets t
    INNER JOIN Departments d ON t.DepartmentID = d.Id
    INNER JOIN Category c ON t.CategoryID = c.CategoryID
    INNER JOIN SubCategory sc ON t.SubCategoryID = sc.SubCategoryID
    ORDER BY t.CreateDate DESC;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;



GO
/****** Object:  StoredProcedure [dbo].[GetTicketsByDepartment]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTicketsByDepartment]
AS
BEGIN
    BEGIN TRY
    SELECT 
        d.DepartmentName, 
        COUNT(t.TicketNo) AS TicketCount
    FROM Tickets t
    INNER JOIN Departments d ON t.DepartmentID = d.Id
    GROUP BY d.DepartmentName;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetTicketsStatusByDate]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTicketsStatusByDate]
AS
BEGIN
   BEGIN TRY
    SELECT 
        CAST(t.CreateDate AS DATE) AS TicketDate,
        SUM(CASE WHEN t.Status = 'Open' THEN 1 ELSE 0 END) AS OpenTickets,
        SUM(CASE WHEN t.Status = 'Closed' THEN 1 ELSE 0 END) AS ClosedTickets,
		SUM(CASE WHEN t.Status = 'Resolved' THEN 1 ELSE 0 END) AS ResolvedTickets
    FROM Tickets t
    GROUP BY CAST(t.CreateDate AS DATE)
    ORDER BY TicketDate;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserMasterData]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserMasterData]
AS
BEGIN
    BEGIN TRY
    SELECT 
        u.UserID, 
        CONCAT(um.FirstName, ' ', um.MiddleName, ' ', um.LastName) AS Name,
        d.DepartmentName,
        um.MobileNo,
        um.Email,
        um.Status,
        u.Username
    FROM UserMaster um
    INNER JOIN Users u ON um.UserID = u.UserID
    INNER JOIN Departments d ON um.DepartmentID = d.ID
    ORDER BY u.UserID;  
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserMasterDataById]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserMasterDataById]
    @UserId INT
	AS
BEGIN
    BEGIN TRY
    SELECT 
        UM.UserID,
        UM.FirstName,
        UM.MiddleName,
        UM.LastName,
        UM.Role,
        UM.Password,
        UM.Designation,
        UM.MobileNo,
        UM.Email,
        UM.Status,
		 UM.DepartmentID,
        D.DepartmentName
    FROM UserMaster UM
    INNER JOIN Departments D ON UM.DepartmentID = D.Id
    WHERE UM.UserID = @UserId;
END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] System;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertCategory]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertCategory]
    @CategoryName VARCHAR(100),
    @DepartmentName VARCHAR(100),
    @Status VARCHAR(50),
    @CreatedIP VARCHAR(50)
AS
BEGIN
    BEGIN TRY
        INSERT INTO Category (CategoryName, DepartmentName, Status, CreatedDate, CreatedIP)
        VALUES (@CategoryName, @DepartmentName, @Status, GETDATE(), @CreatedIP);
    END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] system;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertDepartment]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDepartment]
    @DepartmentName VARCHAR(50),
    @Status VARCHAR(20),
    @CreatedIP VARCHAR(50)
AS
BEGIN
    BEGIN TRY
    INSERT INTO Departments (DepartmentName, Status, CreatedDate, CreatedIP)
    VALUES (@DepartmentName, @Status, GETDATE(), @CreatedIP)
 END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] system;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertLogException]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertLogException]
    @Error NVARCHAR(MAX),
    @Source NVARCHAR(255),
    @Message NVARCHAR(MAX),
    @ErrorType NVARCHAR(255),
    @StackType NVARCHAR(MAX),
    @Created_By NVARCHAR(50) = 'System'
AS
BEGIN
    INSERT INTO LogException ([Error], Source, Message, ErrorType, StackType, Created_By)
    VALUES (@Error, @Source, @Message, @ErrorType, @StackType, @Created_By);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertSubCategory]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertSubCategory]
    @SubCategoryName NVARCHAR(100),
    @CategoryID INT,
    @DepartmentID INT,
    @Status NVARCHAR(10),
    @CreatedIP NVARCHAR(50)
AS
BEGIN
    INSERT INTO SubCategory (SubCategoryName, CategoryID, DepartmentID, Status, CreatedIP)
    VALUES (@SubCategoryName, @CategoryID, @DepartmentID, @Status, @CreatedIP);
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @MiddleName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Role NVARCHAR(50),
    @DepartmentID INT,
    @Designation NVARCHAR(50),
    @MobileNo NVARCHAR(15),
    @Email NVARCHAR(50),
    @Status BIT
AS
BEGIN
    INSERT INTO Users (Username, Password, FirstName, MiddleName, LastName, Role, DepartmentID, Designation, MobileNo, Email, Status)
    VALUES (@Username, @Password, @FirstName, @MiddleName, @LastName, @Role, @DepartmentID, @Designation, @MobileNo, @Email, @Status);
END;
GO
/****** Object:  StoredProcedure [dbo].[LogError]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LogError]
    @Created_By NVARCHAR(50) = 'System' -- Default value for Created_By
AS
BEGIN
    INSERT INTO LogException (ErrorDate, Error, Source, Message, ErrorType, StackType, Created_By)
    VALUES (
        GETDATE(), 
        ERROR_MESSAGE(),
        ERROR_PROCEDURE(),
        ERROR_MESSAGE(),
        ERROR_SEVERITY(),
        ERROR_LINE(),
        @Created_By
    );
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategory]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateCategory]
    @CategoryID INT,
    @CategoryName NVARCHAR(100),
    @DepartmentName NVARCHAR(100),
    @Status NVARCHAR(50),
    @UpdatedIP NVARCHAR(50)
AS
BEGIN
    BEGIN TRY
    UPDATE Category
    SET CategoryName = @CategoryName,
        DepartmentName = @DepartmentName,
        Status = @Status,
        UpdatedDate = GETDATE(),
        UpdatedIP = @UpdatedIP
    WHERE CategoryID = @CategoryID
  END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] system;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateDepartment]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDepartment]
    @Id INT,
    @DepartmentName VARCHAR(50),
    @Status VARCHAR(20),
    @UpdatedIP VARCHAR(50)
AS
BEGIN
    BEGIN TRY
    UPDATE Departments
    SET DepartmentName = @DepartmentName,
        Status = @Status,
        UpdatedDate = GETDATE(),
        UpdatedIP = @UpdatedIP
    WHERE Id = @Id
  END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] system;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateRole]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRole]
    @RoleID INT,
    @RoleName NVARCHAR(100),
    @Status NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
    UPDATE RoleMaster
    SET RoleName = @RoleName,
        Status = @Status
    WHERE RoleID = @RoleID;
  END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] system;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateSubCategory]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateSubCategory]
    @SubCategoryID INT,
    @SubCategoryName NVARCHAR(100),
    @CategoryID INT,
    @DepartmentID INT,
    @Status NVARCHAR(10),
    @UpdatedIP NVARCHAR(50) = '127.0.0.1'  -- Default value for IP
AS
BEGIN
    BEGIN TRY
    UPDATE SubCategory
    SET SubCategoryName = @SubCategoryName,
        CategoryID = @CategoryID,
        DepartmentID = @DepartmentID,
        Status = @Status,
        UpdatedDate = GETDATE(),
        UpdatedIP = @UpdatedIP
    WHERE SubCategoryID = @SubCategoryID;
  END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] system;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateTicketStatus]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTicketStatus]
    @TicketNo VARCHAR(50),
    @Status VARCHAR(50),
    @Feedback TEXT

AS
BEGIN
    BEGIN TRY
    UPDATE Tickets
    SET 
        Status = @Status, 
        Feedback = @Feedback,
        UpdatedDate = GETDATE()  -- This will update the UpdatedDate column
    WHERE TicketNo = @TicketNo
 END TRY
    BEGIN CATCH
        EXEC [dbo].[LogError] system;
        THROW;
    END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 1/15/2025 11:12:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateUser]
    @UserID INT,
    @Username NVARCHAR(50),
    @Password NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @MiddleName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Role NVARCHAR(50),
    @DepartmentID INT,
    @Designation NVARCHAR(50),
    @MobileNo NVARCHAR(15),
    @Email NVARCHAR(50),
    @Status BIT
AS
BEGIN
    UPDATE Users
    SET 
        Username = @Username,
        Password = @Password,
        FirstName = @FirstName,
        MiddleName = @MiddleName,
        LastName = @LastName,
        Role = @Role,
        DepartmentID = @DepartmentID,
        Designation = @Designation,
        MobileNo = @MobileNo,
        Email = @Email,
        Status = @Status
    WHERE UserID = @UserID;
END;
GO
USE [master]
GO
ALTER DATABASE [Masters] SET  READ_WRITE 
GO
