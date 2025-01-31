USE [master]
GO
/****** Object:  Database [FStoreDBAssignment]    Script Date: 30-Oct-24 12:55:29 PM ******/
CREATE DATABASE [FStoreDBAssignment]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FStoreDBAssignment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FStoreDBAssignment.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FStoreDBAssignment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\FStoreDBAssignment_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FStoreDBAssignment] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FStoreDBAssignment].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FStoreDBAssignment] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET ARITHABORT OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FStoreDBAssignment] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FStoreDBAssignment] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FStoreDBAssignment] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FStoreDBAssignment] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FStoreDBAssignment] SET  MULTI_USER 
GO
ALTER DATABASE [FStoreDBAssignment] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FStoreDBAssignment] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FStoreDBAssignment] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FStoreDBAssignment] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FStoreDBAssignment] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FStoreDBAssignment] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FStoreDBAssignment] SET QUERY_STORE = ON
GO
ALTER DATABASE [FStoreDBAssignment] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FStoreDBAssignment]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 30-Oct-24 12:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberId] [int] NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 30-Oct-24 12:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] NOT NULL,
	[MemberId] [int] NULL,
	[OrderDate] [date] NOT NULL,
	[RequiredDate] [date] NULL,
	[ShippedDate] [date] NULL,
	[Freight] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 30-Oct-24 12:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitPrice] [decimal](18, 0) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [float] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 30-Oct-24 12:55:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[Weight] [nchar](10) NOT NULL,
	[UnitPrice] [decimal](18, 0) NOT NULL,
	[UnitslnStock] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (1, N'member01@gmail.com', N'FPT', N'Ha Noi', N'Viet Nam', N'123')
INSERT [dbo].[Member] ([MemberId], [Email], [CompanyName], [City], [Country], [Password]) VALUES (2, N'member02@gmail.com', N'FPT', N'Ha Noi', N'Viet Nam', N'123')
GO
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (1, 1, CAST(N'2024-07-18' AS Date), CAST(N'2024-07-18' AS Date), CAST(N'2024-07-18' AS Date), CAST(1 AS Decimal(18, 0)))
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (3, 2, CAST(N'2024-07-19' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (4, 2, CAST(N'2024-07-19' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (5, 1, CAST(N'2024-07-19' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (6, 1, CAST(N'2024-07-19' AS Date), NULL, NULL, NULL)
INSERT [dbo].[Order] ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight]) VALUES (7, 1, CAST(N'2024-07-20' AS Date), NULL, NULL, NULL)
GO
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (1, 1, CAST(900 AS Decimal(18, 0)), 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (3, 2, CAST(300 AS Decimal(18, 0)), 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (4, 1, CAST(900 AS Decimal(18, 0)), 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (4, 2, CAST(300 AS Decimal(18, 0)), 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (5, 5, CAST(20 AS Decimal(18, 0)), 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (6, 1, CAST(900 AS Decimal(18, 0)), 4, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (6, 10, CAST(100 AS Decimal(18, 0)), 1, 0)
INSERT [dbo].[OrderDetail] ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount]) VALUES (7, 1, CAST(900 AS Decimal(18, 0)), 2, 0)
GO
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (1, 101, N'Laptop', N'2.5 kg    ', CAST(900 AS Decimal(18, 0)), 45)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (2, 102, N'Smartphone', N'150 g     ', CAST(300 AS Decimal(18, 0)), 100)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (3, 103, N'Headphones', N'50 g      ', CAST(50 AS Decimal(18, 0)), 200)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (4, 101, N'Monitor', N'3 kg      ', CAST(249 AS Decimal(18, 0)), 30)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (5, 104, N'Mouse', N'100 g     ', CAST(20 AS Decimal(18, 0)), 150)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (6, 102, N'Tablet', N'500 g     ', CAST(199 AS Decimal(18, 0)), 20)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (7, 105, N'External Hard Drive', N'200 g     ', CAST(80 AS Decimal(18, 0)), 80)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (8, 103, N'Speakers', N'1 kg      ', CAST(129 AS Decimal(18, 0)), 15)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (9, 101, N'Printer', N'10 kg     ', CAST(599 AS Decimal(18, 0)), 10)
INSERT [dbo].[Product] ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitslnStock]) VALUES (10, 102, N'SSD', N'80 g      ', CAST(100 AS Decimal(18, 0)), 49)
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Member] ([MemberId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Orders_Members]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
USE [master]
GO
ALTER DATABASE [FStoreDBAssignment] SET  READ_WRITE 
GO
