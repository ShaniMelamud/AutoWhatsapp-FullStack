USE [master]
GO
/****** Object:  Database [AutoWhatsapp]    Script Date: 10/11/2020 15:43:52 ******/
CREATE DATABASE [AutoWhatsapp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AutoWhatsapp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AutoWhatsapp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AutoWhatsapp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\AutoWhatsapp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [AutoWhatsapp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutoWhatsapp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AutoWhatsapp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET ARITHABORT OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AutoWhatsapp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AutoWhatsapp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AutoWhatsapp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AutoWhatsapp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AutoWhatsapp] SET  MULTI_USER 
GO
ALTER DATABASE [AutoWhatsapp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AutoWhatsapp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AutoWhatsapp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AutoWhatsapp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AutoWhatsapp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AutoWhatsapp] SET QUERY_STORE = OFF
GO
USE [AutoWhatsapp]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[AnswerContent] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Answers] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AutoMessages]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AutoMessages](
	[AutoMessageID] [int] IDENTITY(1,1) NOT NULL,
	[MessageID] [int] NOT NULL,
	[ScheduleID] [int] NOT NULL,
 CONSTRAINT [PK_AutoMessages] PRIMARY KEY CLUSTERED 
(
	[AutoMessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Businesses]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Businesses](
	[BusinessID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessName] [nvarchar](50) NULL,
	[BusinessType] [nvarchar](50) NULL,
	[BusinessPhone] [nvarchar](20) NOT NULL,
	[BusinessEmail] [nvarchar](50) NULL,
	[CustomerName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Businesses] PRIMARY KEY CLUSTERED 
(
	[BusinessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatBots]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatBots](
	[ChatBotID] [int] IDENTITY(1,1) NOT NULL,
	[MessageQuestionID] [int] NOT NULL,
	[MessageAnswerID] [int] NOT NULL,
 CONSTRAINT [PK_ChatBots] PRIMARY KEY CLUSTERED 
(
	[ChatBotID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ContactID] [int] IDENTITY(1,1) NOT NULL,
	[BusinessID] [int] NOT NULL,
	[ContactName] [nvarchar](50) NOT NULL,
	[ContactPhone] [nvarchar](50) NOT NULL,
	[ContactEmail] [nvarchar](50) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailingLists]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailingLists](
	[MailingListID] [int] IDENTITY(1,1) NOT NULL,
	[MailingListName] [nvarchar](20) NULL,
	[BusinessID] [int] NULL,
 CONSTRAINT [PK_MailingLists] PRIMARY KEY CLUSTERED 
(
	[MailingListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailingListsContacts]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailingListsContacts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[MailingListID] [int] NULL,
	[ContactID] [int] NULL,
 CONSTRAINT [PK_MailingListsContacts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailingListsMessages]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailingListsMessages](
	[MailingListMessageID] [int] IDENTITY(1,1) NOT NULL,
	[MailingListID] [int] NULL,
	[MessageID] [int] NULL,
 CONSTRAINT [PK_MailingListsMessages] PRIMARY KEY CLUSTERED 
(
	[MailingListMessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[MessageContent] [nvarchar](200) NOT NULL,
	[BusinessID] [int] NOT NULL,
	[FilePath] [nvarchar](300) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[AnswerID] [int] NOT NULL,
	[BusinessID] [int] NOT NULL,
	[QuestionContent] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 10/11/2020 15:43:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[ScheduleID] [int] IDENTITY(1,1) NOT NULL,
	[FromTime] [date] NOT NULL,
	[ToTime] [date] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[ScheduleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Businesses] ON 

INSERT [dbo].[Businesses] ([BusinessID], [BusinessName], [BusinessType], [BusinessPhone], [BusinessEmail], [CustomerName], [Password], [Username], [Role]) VALUES (1, N'Cellcom', N'Communication', N'0521231231', N'cellcom@cellcom.co.il', N'Yosi Cohen', N'1234', N'Yosi', N'business')
INSERT [dbo].[Businesses] ([BusinessID], [BusinessName], [BusinessType], [BusinessPhone], [BusinessEmail], [CustomerName], [Password], [Username], [Role]) VALUES (3, N'Golan-Telecom', N'Communication', N'0585858585', N'golan@golan-telecom.co.il', N'Moshe Levi', N'1234', N'Moshe', N'business')
INSERT [dbo].[Businesses] ([BusinessID], [BusinessName], [BusinessType], [BusinessPhone], [BusinessEmail], [CustomerName], [Password], [Username], [Role]) VALUES (4, NULL, NULL, N'+972584848464', N'david1ariel@gmail.com', N'David Ariel', N'1234', N'david1234', N'user')
INSERT [dbo].[Businesses] ([BusinessID], [BusinessName], [BusinessType], [BusinessPhone], [BusinessEmail], [CustomerName], [Password], [Username], [Role]) VALUES (9, N'Tomedia', N'Web', N'+972545799650', N'danielb18@gmail.com', N'Daniel Burd', N'1234', N'Dani', N'user')
SET IDENTITY_INSERT [dbo].[Businesses] OFF
GO
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (1, 1, N'Avi', N'01234567', N'1@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (2, 1, N'Itzik', N'543225643', N'2@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (3, 1, N'Jack', N'4532662', N'3@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (4, 1, N'Moishe', N'5432562', N'42@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (5, 1, N'Aron', N'234254', N'52@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (6, 1, N'Yosi', N'23534254363', N'62@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (7, 1, N'dave', N'453425235', N'72@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (8, 1, N'Rubi', N'2524523456', N'82@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (9, 1, N'Simon', N'45346543634', N'92@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (10, 1, N'Levi', N'252525', N'102@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (11, 1, N'Yuda', N'25243524', N'112@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (13, 1, N'Issi', N'25243652365', N'122@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (14, 9, N'Daniel Burd', N'+972545799650', N'dnielb18@gmail.com')
INSERT [dbo].[Contacts] ([ContactID], [BusinessID], [ContactName], [ContactPhone], [ContactEmail]) VALUES (15, 9, N'David Ariel', N'+972584848464', N'david1ariel@gmail.com')
SET IDENTITY_INSERT [dbo].[Contacts] OFF
GO
SET IDENTITY_INSERT [dbo].[MailingLists] ON 

INSERT [dbo].[MailingLists] ([MailingListID], [MailingListName], [BusinessID]) VALUES (1, N'Clients', NULL)
INSERT [dbo].[MailingLists] ([MailingListID], [MailingListName], [BusinessID]) VALUES (2, N'Employees', NULL)
INSERT [dbo].[MailingLists] ([MailingListID], [MailingListName], [BusinessID]) VALUES (3, N'Employees', NULL)
INSERT [dbo].[MailingLists] ([MailingListID], [MailingListName], [BusinessID]) VALUES (4, N'Salesmen', NULL)
INSERT [dbo].[MailingLists] ([MailingListID], [MailingListName], [BusinessID]) VALUES (5, N'Managers', NULL)
INSERT [dbo].[MailingLists] ([MailingListID], [MailingListName], [BusinessID]) VALUES (6, N'Ex-Clients', NULL)
INSERT [dbo].[MailingLists] ([MailingListID], [MailingListName], [BusinessID]) VALUES (7, N'Tomedia Team', 9)
SET IDENTITY_INSERT [dbo].[MailingLists] OFF
GO
SET IDENTITY_INSERT [dbo].[MailingListsContacts] ON 

INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (3, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (4, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (5, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (6, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (7, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (8, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (9, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (10, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (11, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (12, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (13, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (14, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (15, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (16, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (17, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (18, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (19, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (20, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (21, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (22, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (23, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (24, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (25, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (26, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (27, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (28, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (29, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (30, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (31, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (32, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (33, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (34, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (35, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (36, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (37, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (38, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (39, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (40, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (41, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (42, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (43, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (44, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (45, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (46, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (47, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (48, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (49, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (50, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (51, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (52, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (53, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (54, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (55, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (56, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (57, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (58, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (59, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (60, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (61, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (62, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (63, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (64, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (65, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (66, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (67, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (68, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (69, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (70, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (71, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (72, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (73, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (74, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (75, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (76, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (77, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (78, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (79, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (80, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (81, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (82, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (83, 1, 1)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (84, 1, 2)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (85, 1, 3)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (86, 1, 4)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (87, 1, 5)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (88, 1, 6)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (89, 1, 7)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (90, 1, 8)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (91, 1, 9)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (92, 1, 10)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (93, 7, 14)
INSERT [dbo].[MailingListsContacts] ([id], [MailingListID], [ContactID]) VALUES (94, 7, 15)
SET IDENTITY_INSERT [dbo].[MailingListsContacts] OFF
GO
SET IDENTITY_INSERT [dbo].[MailingListsMessages] ON 

INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (2, 1, 1)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (3, 2, 2)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (4, 3, 3)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (5, 4, 4)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (6, 5, 5)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (7, 6, 6)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (8, 1, 12)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (9, 2, 11)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (10, 3, 10)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (11, 4, 9)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (12, 5, 8)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (13, 6, 7)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (14, 3, 13)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (15, 4, 14)
INSERT [dbo].[MailingListsMessages] ([MailingListMessageID], [MailingListID], [MessageID]) VALUES (16, 5, 15)
SET IDENTITY_INSERT [dbo].[MailingListsMessages] OFF
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (1, N'Testing 1', 1, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (2, N'Testing 2', 3, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (3, N'Testing 3', 4, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (4, N'Testing 4', 3, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (5, N'Testing 5', 1, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (6, N'Testing 6', 4, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (7, N'Teting 7', 1, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (8, N'Testing 8', 1, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (9, N'Testing 9', 4, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (10, N'Testing 10', 3, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (11, N'Testing 11', 4, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (12, N'Testing 12', 1, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (13, N'Testing 13', 4, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (14, N'Testing 14', 3, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (15, N'Testing 15', 1, NULL)
INSERT [dbo].[Messages] ([MessageID], [MessageContent], [BusinessID], [FilePath]) VALUES (1002, N'Testing Quartz scheduler', 9, NULL)
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
ALTER TABLE [dbo].[AutoMessages]  WITH CHECK ADD  CONSTRAINT [FK_AutoMessages_Messages] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Messages] ([MessageID])
GO
ALTER TABLE [dbo].[AutoMessages] CHECK CONSTRAINT [FK_AutoMessages_Messages]
GO
ALTER TABLE [dbo].[AutoMessages]  WITH CHECK ADD  CONSTRAINT [FK_AutoMessages_Schedules] FOREIGN KEY([ScheduleID])
REFERENCES [dbo].[Schedules] ([ScheduleID])
GO
ALTER TABLE [dbo].[AutoMessages] CHECK CONSTRAINT [FK_AutoMessages_Schedules]
GO
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Businesses] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Businesses] ([BusinessID])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Businesses]
GO
ALTER TABLE [dbo].[MailingListsContacts]  WITH CHECK ADD  CONSTRAINT [FK_MailingListsContacts_Contacts] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contacts] ([ContactID])
GO
ALTER TABLE [dbo].[MailingListsContacts] CHECK CONSTRAINT [FK_MailingListsContacts_Contacts]
GO
ALTER TABLE [dbo].[MailingListsContacts]  WITH CHECK ADD  CONSTRAINT [FK_MailingListsContacts_MailingLists] FOREIGN KEY([MailingListID])
REFERENCES [dbo].[MailingLists] ([MailingListID])
GO
ALTER TABLE [dbo].[MailingListsContacts] CHECK CONSTRAINT [FK_MailingListsContacts_MailingLists]
GO
ALTER TABLE [dbo].[MailingListsMessages]  WITH CHECK ADD  CONSTRAINT [FK_MailingListsMessages_MailingLists] FOREIGN KEY([MailingListID])
REFERENCES [dbo].[MailingLists] ([MailingListID])
GO
ALTER TABLE [dbo].[MailingListsMessages] CHECK CONSTRAINT [FK_MailingListsMessages_MailingLists]
GO
ALTER TABLE [dbo].[MailingListsMessages]  WITH CHECK ADD  CONSTRAINT [FK_MailingListsMessages_Messages] FOREIGN KEY([MessageID])
REFERENCES [dbo].[Messages] ([MessageID])
GO
ALTER TABLE [dbo].[MailingListsMessages] CHECK CONSTRAINT [FK_MailingListsMessages_Messages]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Businesses] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Businesses] ([BusinessID])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Businesses]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Answers] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[Answers] ([AnswerID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Answers]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_Businesses] FOREIGN KEY([BusinessID])
REFERENCES [dbo].[Businesses] ([BusinessID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_Businesses]
GO
USE [master]
GO
ALTER DATABASE [AutoWhatsapp] SET  READ_WRITE 
GO
