USE [master]
GO

/****** Object:  Database [USAbleLifeOrderManagement]    Script Date: 10/4/2017 12:38:56 PM ******/
CREATE DATABASE [USAbleLifeOrderManagement]
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [USAbleLifeOrderManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET ARITHABORT OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET  DISABLE_BROKER 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET RECOVERY FULL 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET  MULTI_USER 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET DB_CHAINING OFF 
GO

ALTER DATABASE [USAbleLifeOrderManagement] SET  READ_WRITE 
GO



USE [master]
GO

/****** Object:  Login [USAbleLifeAdmin]    Script Date: 10/4/2017 12:34:00 PM ******/
CREATE LOGIN [USAbleLifeAdmin] WITH PASSWORD=N'*^TgbHJ765rdfG==', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

--ALTER LOGIN [USAbleLifeAdmin] DISABLE
--GO

USE [USAbleLifeOrderManagement]
GO

/****** Object:  User [USAbleLifeAdmin]    Script Date: 10/4/2017 12:34:00 PM ******/
CREATE USER [USAbleLifeAdmin] FOR LOGIN [USAbleLifeAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
--ALTER ROLE [db_owner] ADD MEMBER [USAbleLifeAdmin]
EXEC sp_addrolemember  'db_owner','USAbleLifeAdmin'
GO
EXEC sp_addrolemember  'db_datareader','USAbleLifeAdmin'
GO
EXEC sp_addrolemember  'db_datawriter','USAbleLifeAdmin'
GO

/****** Object:  Table [dbo].[Discount]    Script Date: 10/4/2017 12:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Type] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employee]    Script Date: 10/4/2017 12:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MealOrder]    Script Date: 10/4/2017 12:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealOrder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DiscountId] [bigint] NULL,
	[Created] [datetime] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_MealOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MealOrderItem]    Script Date: 10/4/2017 12:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealOrderItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MealOrderId] [bigint] NOT NULL,
	[MenuItemId] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_MealOrderitem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MealOrderTax]    Script Date: 10/4/2017 12:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MealOrderTax](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MealOrderId] [bigint] NOT NULL,
	[TaxId] [bigint] NOT NULL,
 CONSTRAINT [PK_MealOrderTax] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 10/4/2017 12:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tax]    Script Date: 10/4/2017 12:34:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tax](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Percentage] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Tax] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([Id], [Name], [Amount], [Type], [IsDeleted]) VALUES (1, N'Senior', CAST(14.00 AS Decimal(18, 2)), 1, 0)
INSERT [dbo].[Discount] ([Id], [Name], [Amount], [Type], [IsDeleted]) VALUES (2, N'Military', CAST(4.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Discount] ([Id], [Name], [Amount], [Type], [IsDeleted]) VALUES (3, N'Child test', CAST(2.00 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Discount] ([Id], [Name], [Amount], [Type], [IsDeleted]) VALUES (4, N'Employee', CAST(14.00 AS Decimal(18, 2)), 1, 0)
INSERT [dbo].[Discount] ([Id], [Name], [Amount], [Type], [IsDeleted]) VALUES (5, N'Test', CAST(3.55 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Discount] ([Id], [Name], [Amount], [Type], [IsDeleted]) VALUES (6, N'Senior Multi', CAST(2.99 AS Decimal(18, 2)), 0, 0)
INSERT [dbo].[Discount] ([Id], [Name], [Amount], [Type], [IsDeleted]) VALUES (7, N'Night Owl', CAST(2.00 AS Decimal(18, 2)), 0, 0)
SET IDENTITY_INSERT [dbo].[Discount] OFF
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Username], [FirstName], [LastName], [Password], [IsDeleted]) VALUES (1, N'1234', N'Jane', N'Doe', N'ff353ba787d56a6c8532ac6bfc4ee3ce', NULL)
INSERT [dbo].[Employee] ([Id], [Username], [FirstName], [LastName], [Password], [IsDeleted]) VALUES (2, N'3456', N'John', N'Smith', N'54ee198f01f340fdabd07a003be104df', 0)
SET IDENTITY_INSERT [dbo].[Employee] OFF
SET IDENTITY_INSERT [dbo].[MealOrder] ON 

INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (1, NULL, CAST(N'2017-09-25T12:09:00.813' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (2, NULL, CAST(N'2017-09-25T13:38:42.240' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (3, NULL, CAST(N'2017-09-25T13:38:42.270' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (4, NULL, CAST(N'2017-09-25T13:38:42.270' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (5, NULL, CAST(N'2017-09-25T13:38:42.270' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (8, NULL, CAST(N'2017-09-26T00:09:21.210' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (9, NULL, CAST(N'2017-09-26T00:14:24.837' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (10, NULL, CAST(N'2017-09-26T00:17:30.240' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (11, NULL, CAST(N'2017-09-26T00:18:38.563' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (12, NULL, CAST(N'2017-09-26T00:20:04.710' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (13, NULL, CAST(N'2017-09-26T00:22:54.957' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (14, NULL, CAST(N'2017-09-26T00:23:28.370' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (15, NULL, CAST(N'2017-09-26T00:25:17.793' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (16, 1, CAST(N'2017-09-28T21:59:46.207' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (17, 1, CAST(N'2017-09-29T21:41:53.433' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (18, 1, CAST(N'2017-09-29T22:02:43.140' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (19, 1, CAST(N'2017-09-29T22:03:10.393' AS DateTime), 1)
INSERT [dbo].[MealOrder] ([Id], [DiscountId], [Created], [EmployeeId]) VALUES (20, 1, CAST(N'2017-09-30T01:21:37.427' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[MealOrder] OFF
SET IDENTITY_INSERT [dbo].[MealOrderItem] ON 

INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (1, 9, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (2, 9, 3, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (3, 9, 6, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (4, 10, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (5, 10, 3, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (6, 10, 6, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (7, 10, 13, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (8, 10, 10, 2)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (9, 11, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (10, 12, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (11, 12, 4, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (12, 12, 7, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (13, 12, 11, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (14, 13, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (15, 13, 4, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (16, 13, 6, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (17, 13, 9, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (18, 14, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (19, 14, 4, 2)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (20, 14, 6, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (21, 14, 9, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (22, 14, 2, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (23, 15, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (24, 15, 4, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (25, 15, 9, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (26, 16, 1, 2)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (27, 17, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (28, 18, 1, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (29, 19, 2, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (30, 20, 6, 4)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (31, 20, 9, 5)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (32, 20, 11, 8)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (33, 20, 13, 7)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (34, 20, 4, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (35, 20, 10, 1)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (36, 20, 3, 2)
INSERT [dbo].[MealOrderItem] ([Id], [MealOrderId], [MenuItemId], [Quantity]) VALUES (37, 20, 5, 1)
SET IDENTITY_INSERT [dbo].[MealOrderItem] OFF
SET IDENTITY_INSERT [dbo].[MealOrderTax] ON 

INSERT [dbo].[MealOrderTax] ([Id], [MealOrderId], [TaxId]) VALUES (1, 18, 3)
INSERT [dbo].[MealOrderTax] ([Id], [MealOrderId], [TaxId]) VALUES (2, 19, 3)
INSERT [dbo].[MealOrderTax] ([Id], [MealOrderId], [TaxId]) VALUES (3, 20, 1)
INSERT [dbo].[MealOrderTax] ([Id], [MealOrderId], [TaxId]) VALUES (4, 20, 2)
INSERT [dbo].[MealOrderTax] ([Id], [MealOrderId], [TaxId]) VALUES (5, 20, 3)
INSERT [dbo].[MealOrderTax] ([Id], [MealOrderId], [TaxId]) VALUES (6, 20, 4)
INSERT [dbo].[MealOrderTax] ([Id], [MealOrderId], [TaxId]) VALUES (7, 20, 5)
SET IDENTITY_INSERT [dbo].[MealOrderTax] OFF
SET IDENTITY_INSERT [dbo].[MenuItem] ON 

INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (1, N'City', CAST(9.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (2, N'Candy', CAST(1.50 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (3, N'Pretzel', CAST(1.88 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (4, N'Popcorn', CAST(2.99 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (5, N'Hamburger', CAST(2.49 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (6, N'Chesseburger', CAST(2.99 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (7, N'French Fries', CAST(1.59 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (8, N'Kids Meal', CAST(3.99 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (9, N'Apple Pie', CAST(2.99 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (10, N'Cinnamon Bun', CAST(2.25 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (11, N'Coffee', CAST(0.99 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (12, N'Hot Chocolate', CAST(1.55 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (13, N'Fruit Cup', CAST(2.89 AS Decimal(18, 2)), NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Price], [IsDeleted]) VALUES (14, N'Milkshake', CAST(3.99 AS Decimal(18, 2)), NULL)
SET IDENTITY_INSERT [dbo].[MenuItem] OFF
SET IDENTITY_INSERT [dbo].[Tax] ON 

INSERT [dbo].[Tax] ([Id], [Name], [Percentage], [IsDeleted]) VALUES (1, N'City', 8, NULL)
INSERT [dbo].[Tax] ([Id], [Name], [Percentage], [IsDeleted]) VALUES (2, N'State', 3, NULL)
INSERT [dbo].[Tax] ([Id], [Name], [Percentage], [IsDeleted]) VALUES (3, N'County', 1, NULL)
INSERT [dbo].[Tax] ([Id], [Name], [Percentage], [IsDeleted]) VALUES (4, N'Federal', 2, NULL)
INSERT [dbo].[Tax] ([Id], [Name], [Percentage], [IsDeleted]) VALUES (5, N'Fast Food', 20, NULL)
INSERT [dbo].[Tax] ([Id], [Name], [Percentage], [IsDeleted]) VALUES (7, N'test', 1, 1)
INSERT [dbo].[Tax] ([Id], [Name], [Percentage], [IsDeleted]) VALUES (9, N'United', 12, 0)
SET IDENTITY_INSERT [dbo].[Tax] OFF
ALTER TABLE [dbo].[Discount] ADD  CONSTRAINT [DF_Discount_Type]  DEFAULT ((1)) FOR [Type]
GO
ALTER TABLE [dbo].[Discount] ADD  CONSTRAINT [DF_Discount_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MealOrderItem] ADD  CONSTRAINT [DF_MealOrderItem_Quantity]  DEFAULT ((1)) FOR [Quantity]
GO
ALTER TABLE [dbo].[MenuItem] ADD  CONSTRAINT [DF_MenuItem_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tax] ADD  CONSTRAINT [DF_Tax_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
