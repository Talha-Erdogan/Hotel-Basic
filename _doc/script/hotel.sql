USE [Hotel]
GO
/****** Object:  Table [dbo].[Auth]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auth](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [PK_Auth] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TC] [nvarchar](11) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personnel]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personnel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TC] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Personnel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedDateTime] [datetime] NULL,
	[DeletedBy] [int] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileDetail]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProfileId] [int] NOT NULL,
	[AuthId] [int] NOT NULL,
 CONSTRAINT [PK_ProfileDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfilePersonnel]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfilePersonnel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonnelId] [int] NOT NULL,
	[ProfileId] [int] NOT NULL,
 CONSTRAINT [PK_ProfilePersonnel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[EntryDate] [datetime] NULL,
	[ReleaseDate] [datetime] NULL,
	[TotalDebt] [int] NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 21.08.2020 11:31:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [int] NOT NULL,
	[Floor] [nvarchar](50) NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Auth] ON 

INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (1, N'PAGE_AUTH_LIST', N'Yetki Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (2, N'PAGE_AUTH_ADD', N'Yetki Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (3, N'PAGE_AUTH_EDIT', N'Yetki Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (4, N'PAGE_AUTH_DISPLAY', N'Yetki Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (5, N'PAGE_AUTH_DELETE', N'Yetki Silme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (6, N'PAGE_PROFILE_LIST', N'Profil Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (7, N'PAGE_PROFILE_ADD', N'Profil Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (8, N'PAGE_PROFILE_EDIT', N'Profil Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (9, N'PAGE_PROFILE_DISPLAY', N'Profil Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (10, N'PAGE_PROFILE_DELETE', N'Profil Silme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (11, N'PAGE_PROFILEDETAIL_BATCHEDIT', N'Profil Detaylarını Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (12, N'PAGE_PROFILEPERSONNEL_BATCHEDIT', N'Profil Personelini Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (13, N'PAGE_CUSTOMER_LIST', N'Müşteri Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (14, N'PAGE_CUSTOMER_ADD', N'Müşteri Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (15, N'PAGE_CUSTOMER_EDIT', N'Müşteri Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (16, N'PAGE_CUSTOMER_DISPLAY', N'Müşteri Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (17, N'PAGE_CUSTOMER_DELETE', N'Müşteri Silme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (18, N'PAGE_PERSONNEL_LIST', N'Personel Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (19, N'PAGE_PERSONNEL_ADD', N'Personel Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (20, N'PAGE_PERSONNEL_EDIT', N'Personel Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (21, N'PAGE_PERSONNEL_DISPLAY', N'Personel Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (22, N'PAGE_PERSONNEL_DELETE', N'Personel Silme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (23, N'PAGE_ROOM_LIST', N'Oda Listeleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (24, N'PAGE_ROOM_ADD', N'Oda Ekleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (25, N'PAGE_ROOM_EDIT', N'Oda Düzenleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (26, N'PAGE_ROOM_DISPLAY', N'Oda Görüntüleme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (27, N'PAGE_ROOM_DELETE', N'Oda Silme', 0, NULL, NULL)
INSERT [dbo].[Auth] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (28, N'PAGE_ROOMRESERVATION_OPERATION', N'Oda İişlemleri', 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Auth] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [TC], [Name], [LastName], [Phone]) VALUES (1, N'77788899966', N'Customer', N'Last Name', N'98745632189')
INSERT [dbo].[Customer] ([Id], [TC], [Name], [LastName], [Phone]) VALUES (2, N'33322211144', N'Customer2', N'lastName', N'99966655588')
INSERT [dbo].[Customer] ([Id], [TC], [Name], [LastName], [Phone]) VALUES (3, N'55588899966', N'Customer3', N'Surname', N'44477733322')
INSERT [dbo].[Customer] ([Id], [TC], [Name], [LastName], [Phone]) VALUES (4, N'11199988844', N'Customer4', N'SurName', N'7778885511')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Personnel] ON 

INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password]) VALUES (1, N'11122233344', N'Talha', N'ERDOĞAN', N'1115552244', N'A Sk. No:1 B Cad. C Mah. D / E', N'admin', N'1')
INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password]) VALUES (2, N'99988877766', N'Talha', N'Erdogan (Reseption)', N'9998887766', N'E Sk. No:9 D Cad. C Mah. B / A', N'talha', N'1')
INSERT [dbo].[Personnel] ([Id], [TC], [Name], [LastName], [Phone], [Address], [UserName], [Password]) VALUES (3, N'78945612378', N'Personnel', N'LastName', N'1234567896', N'A Sk. No:1 ', N'personnel', N'1')
SET IDENTITY_INSERT [dbo].[Personnel] OFF
GO
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (1, N'SYSTEMADMIN', N'Sistem Admin Profili', 0, NULL, NULL)
INSERT [dbo].[Profile] ([Id], [Code], [Name], [IsDeleted], [DeletedDateTime], [DeletedBy]) VALUES (2, N'RECEPTIONIST_PROFILE', N'Resepsiyonist Profili', 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Profile] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfileDetail] ON 

INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (1, 1, 11)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (2, 1, 8)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (3, 1, 7)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (4, 1, 9)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (5, 1, 6)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (6, 1, 12)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (7, 1, 10)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (8, 1, 3)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (9, 1, 2)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (10, 1, 4)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (11, 1, 1)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (12, 1, 5)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (14, 1, 25)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (15, 1, 24)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (16, 1, 26)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (17, 1, 23)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (18, 1, 27)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (19, 1, 20)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (20, 1, 19)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (21, 1, 21)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (22, 1, 18)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (23, 1, 22)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (24, 2, 15)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (25, 2, 14)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (26, 2, 16)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (27, 2, 13)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (28, 2, 17)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (29, 1, 28)
INSERT [dbo].[ProfileDetail] ([Id], [ProfileId], [AuthId]) VALUES (30, 2, 28)
SET IDENTITY_INSERT [dbo].[ProfileDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ProfilePersonnel] ON 

INSERT [dbo].[ProfilePersonnel] ([Id], [PersonnelId], [ProfileId]) VALUES (1, 1, 1)
INSERT [dbo].[ProfilePersonnel] ([Id], [PersonnelId], [ProfileId]) VALUES (2, 2, 2)
SET IDENTITY_INSERT [dbo].[ProfilePersonnel] OFF
GO
SET IDENTITY_INSERT [dbo].[Reservation] ON 

INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (1, 1, 1, CAST(N'2020-08-21T09:56:39.600' AS DateTime), CAST(N'2020-08-21T09:56:39.600' AS DateTime), CAST(N'2020-08-21T10:55:07.903' AS DateTime), 100)
INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (2, 6, 3, CAST(N'2020-08-21T09:56:59.513' AS DateTime), CAST(N'2020-08-21T09:56:59.513' AS DateTime), CAST(N'2020-08-21T10:55:12.400' AS DateTime), 600)
INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (3, 2, 2, CAST(N'2020-08-21T10:09:33.703' AS DateTime), CAST(N'2020-08-21T10:09:33.703' AS DateTime), CAST(N'2020-08-21T10:55:10.353' AS DateTime), 200)
INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (4, 6, 1, CAST(N'2020-08-21T10:55:15.113' AS DateTime), CAST(N'2020-08-21T10:55:15.113' AS DateTime), CAST(N'2020-08-21T10:55:24.007' AS DateTime), 600)
INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (5, 2, 3, CAST(N'2020-08-21T10:55:18.290' AS DateTime), CAST(N'2020-08-21T10:55:18.290' AS DateTime), CAST(N'2020-08-21T10:55:25.653' AS DateTime), 200)
INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (6, 1, 4, CAST(N'2020-08-21T10:55:21.493' AS DateTime), CAST(N'2020-08-21T10:55:21.493' AS DateTime), CAST(N'2020-08-21T10:55:26.973' AS DateTime), 100)
INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (7, 6, 2, CAST(N'2020-08-21T10:55:47.050' AS DateTime), CAST(N'2020-08-21T10:55:47.050' AS DateTime), NULL, 0)
INSERT [dbo].[Reservation] ([Id], [RoomId], [CustomerId], [CreatedDateTime], [EntryDate], [ReleaseDate], [TotalDebt]) VALUES (8, 4, 4, CAST(N'2020-08-21T10:55:55.603' AS DateTime), CAST(N'2020-08-21T10:55:55.603' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[Reservation] OFF
GO
SET IDENTITY_INSERT [dbo].[Room] ON 

INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (1, N'Room1', 100, N'1')
INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (2, N'Room2', 200, N'2')
INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (3, N'Room3', 300, N'3')
INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (4, N'Room 4', 400, N'4')
INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (5, N'Room 5', 500, N'5')
INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (6, N'Room 6', 600, N'6')
INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (7, N'Room 7', 700, N'7')
INSERT [dbo].[Room] ([Id], [Name], [Price], [Floor]) VALUES (8, N'Room 8', 800, N'8')
SET IDENTITY_INSERT [dbo].[Room] OFF
GO
