USE [master]
GO
/****** Object:  Database [PharmaCityDb]    Script Date: 17/11/2022 17:17:35 ******/
CREATE DATABASE [PharmaCityDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PharmaCityDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PharmaCityDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PharmaCityDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PharmaCityDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PharmaCityDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PharmaCityDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PharmaCityDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PharmaCityDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PharmaCityDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PharmaCityDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PharmaCityDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [PharmaCityDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PharmaCityDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PharmaCityDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PharmaCityDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PharmaCityDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PharmaCityDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PharmaCityDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PharmaCityDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PharmaCityDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PharmaCityDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PharmaCityDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PharmaCityDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PharmaCityDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PharmaCityDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PharmaCityDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PharmaCityDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PharmaCityDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PharmaCityDb] SET RECOVERY FULL 
GO
ALTER DATABASE [PharmaCityDb] SET  MULTI_USER 
GO
ALTER DATABASE [PharmaCityDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PharmaCityDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PharmaCityDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PharmaCityDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PharmaCityDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PharmaCityDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PharmaCityDb', N'ON'
GO
ALTER DATABASE [PharmaCityDb] SET QUERY_STORE = OFF
GO
USE [PharmaCityDb]
GO
/****** Object:  Table [dbo].[Invitations]    Script Date: 17/11/2022 17:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Role] [int] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[PharmacyId] [int] NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_Invitations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Medicines]    Script Date: 17/11/2022 17:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Medicines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Symptoms] [nvarchar](max) NULL,
	[Presentation] [nvarchar](max) NULL,
	[Quantity] [int] NOT NULL,
	[Unit] [nvarchar](max) NULL,
	[Price] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[Receipt] [nvarchar](max) NULL,
	[PharmacyName] [nvarchar](max) NULL,
	[PharmacyId] [int] NULL,
 CONSTRAINT [PK_Medicines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Petitions]    Script Date: 17/11/2022 17:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Petitions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MedicineCode] [nvarchar](max) NULL,
	[Quantity] [int] NOT NULL,
	[PharmacyId] [int] NULL,
	[State] [int] NOT NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[PurchaseId] [int] NULL,
	[StockRequestId] [int] NULL,
 CONSTRAINT [PK_Petitions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pharmacies]    Script Date: 17/11/2022 17:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pharmacies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Direction] [nvarchar](max) NULL,
 CONSTRAINT [PK_Pharmacies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchases]    Script Date: 17/11/2022 17:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchases](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NULL,
	[PharmacyId] [int] NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_Purchases] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StockRequests]    Script Date: 17/11/2022 17:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[PharmacyId] [int] NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_StockRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17/11/2022 17:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Direction] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
	[RegisterDate] [datetime2](7) NOT NULL,
	[Role] [int] NOT NULL,
	[PharmacyId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Invitations_PharmacyId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_Invitations_PharmacyId] ON [dbo].[Invitations]
(
	[PharmacyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Medicines_PharmacyId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_Medicines_PharmacyId] ON [dbo].[Medicines]
(
	[PharmacyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Petitions_PharmacyId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_Petitions_PharmacyId] ON [dbo].[Petitions]
(
	[PharmacyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Petitions_PurchaseId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_Petitions_PurchaseId] ON [dbo].[Petitions]
(
	[PurchaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Petitions_StockRequestId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_Petitions_StockRequestId] ON [dbo].[Petitions]
(
	[StockRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Purchases_PharmacyId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_Purchases_PharmacyId] ON [dbo].[Purchases]
(
	[PharmacyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StockRequests_EmployeeId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_StockRequests_EmployeeId] ON [dbo].[StockRequests]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_StockRequests_PharmacyId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_StockRequests_PharmacyId] ON [dbo].[StockRequests]
(
	[PharmacyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_PharmacyId]    Script Date: 17/11/2022 17:17:35 ******/
CREATE NONCLUSTERED INDEX [IX_Users_PharmacyId] ON [dbo].[Users]
(
	[PharmacyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Invitations]  WITH CHECK ADD  CONSTRAINT [FK_Invitations_Pharmacies_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[Pharmacies] ([Id])
GO
ALTER TABLE [dbo].[Invitations] CHECK CONSTRAINT [FK_Invitations_Pharmacies_PharmacyId]
GO
ALTER TABLE [dbo].[Medicines]  WITH CHECK ADD  CONSTRAINT [FK_Medicines_Pharmacies_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[Pharmacies] ([Id])
GO
ALTER TABLE [dbo].[Medicines] CHECK CONSTRAINT [FK_Medicines_Pharmacies_PharmacyId]
GO
ALTER TABLE [dbo].[Petitions]  WITH CHECK ADD  CONSTRAINT [FK_Petitions_Pharmacies_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[Pharmacies] ([Id])
GO
ALTER TABLE [dbo].[Petitions] CHECK CONSTRAINT [FK_Petitions_Pharmacies_PharmacyId]
GO
ALTER TABLE [dbo].[Petitions]  WITH CHECK ADD  CONSTRAINT [FK_Petitions_Purchases_PurchaseId] FOREIGN KEY([PurchaseId])
REFERENCES [dbo].[Purchases] ([Id])
GO
ALTER TABLE [dbo].[Petitions] CHECK CONSTRAINT [FK_Petitions_Purchases_PurchaseId]
GO
ALTER TABLE [dbo].[Petitions]  WITH CHECK ADD  CONSTRAINT [FK_Petitions_StockRequests_StockRequestId] FOREIGN KEY([StockRequestId])
REFERENCES [dbo].[StockRequests] ([Id])
GO
ALTER TABLE [dbo].[Petitions] CHECK CONSTRAINT [FK_Petitions_StockRequests_StockRequestId]
GO
ALTER TABLE [dbo].[Purchases]  WITH CHECK ADD  CONSTRAINT [FK_Purchases_Pharmacies_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[Pharmacies] ([Id])
GO
ALTER TABLE [dbo].[Purchases] CHECK CONSTRAINT [FK_Purchases_Pharmacies_PharmacyId]
GO
ALTER TABLE [dbo].[StockRequests]  WITH CHECK ADD  CONSTRAINT [FK_StockRequests_Pharmacies_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[Pharmacies] ([Id])
GO
ALTER TABLE [dbo].[StockRequests] CHECK CONSTRAINT [FK_StockRequests_Pharmacies_PharmacyId]
GO
ALTER TABLE [dbo].[StockRequests]  WITH CHECK ADD  CONSTRAINT [FK_StockRequests_Users_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[StockRequests] CHECK CONSTRAINT [FK_StockRequests_Users_EmployeeId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Pharmacies_PharmacyId] FOREIGN KEY([PharmacyId])
REFERENCES [dbo].[Pharmacies] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Pharmacies_PharmacyId]
GO
USE [master]
GO
ALTER DATABASE [PharmaCityDb] SET  READ_WRITE 
GO
