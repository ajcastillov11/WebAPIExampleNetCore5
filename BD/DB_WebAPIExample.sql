CREATE DATABASE [dbServerAPI]
GO

USE [dbServerAPI]
GO
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_Category_CategoryId]
GO
ALTER TABLE [dbo].[Peliculas] DROP CONSTRAINT [FK_Peliculas_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[UserClaim] DROP CONSTRAINT [DF__UserClaim__Claim__3D5E1FD2]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF__User__Password__3F466844]
GO
ALTER TABLE [dbo].[User] DROP CONSTRAINT [DF__User__UserName__3E52440B]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 23/08/2021 10:28:33 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserClaim]') AND type in (N'U'))
DROP TABLE [dbo].[UserClaim]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/08/2021 10:28:33 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 23/08/2021 10:28:33 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[Peliculas]    Script Date: 23/08/2021 10:28:33 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Peliculas]') AND type in (N'U'))
DROP TABLE [dbo].[Peliculas]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 23/08/2021 10:28:33 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Category]') AND type in (N'U'))
DROP TABLE [dbo].[Category]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 23/08/2021 10:28:33 a. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categorias]') AND type in (N'U'))
DROP TABLE [dbo].[Categorias]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 23/08/2021 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](255) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 23/08/2021 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peliculas]    Script Date: 23/08/2021 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peliculas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NombrePelicula] [nvarchar](255) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[Director] [nvarchar](255) NULL,
 CONSTRAINT [PK_Peliculas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 23/08/2021 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NULL,
	[IntroductionDate] [datetime2](7) NOT NULL,
	[Price] [real] NOT NULL,
	[Url] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/08/2021 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 23/08/2021 10:28:33 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[ClaimId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [nvarchar](255) NOT NULL,
	[ClaimValue] [bit] NOT NULL,
 CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 
GO
INSERT [dbo].[Categorias] ([Id], [Nombre], [Estado]) VALUES (1, N'Accion', 1)
GO
INSERT [dbo].[Categorias] ([Id], [Nombre], [Estado]) VALUES (2, N'Drama', 1)
GO
INSERT [dbo].[Categorias] ([Id], [Nombre], [Estado]) VALUES (3, N'Comedia', 1)
GO
INSERT [dbo].[Categorias] ([Id], [Nombre], [Estado]) VALUES (4, N'Ficcion', 1)
GO
INSERT [dbo].[Categorias] ([Id], [Nombre], [Estado]) VALUES (5, N'Comedia Romantica', 1)
GO
INSERT [dbo].[Categorias] ([Id], [Nombre], [Estado]) VALUES (6, N'Romance', 1)
GO
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Information')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Services')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Training')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Peliculas] ON 
GO
INSERT [dbo].[Peliculas] ([Id], [NombrePelicula], [CategoriaId], [Director]) VALUES (1, N'La Marca', 4, NULL)
GO
INSERT [dbo].[Peliculas] ([Id], [NombrePelicula], [CategoriaId], [Director]) VALUES (2, N'Chivolocracia', 1, N'Uribe - AUC')
GO
INSERT [dbo].[Peliculas] ([Id], [NombrePelicula], [CategoriaId], [Director]) VALUES (3, N'El General en su Laberinto', 4, N'Policeman')
GO
SET IDENTITY_INSERT [dbo].[Peliculas] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (1, N'Jabon Para Perros -- Update', CAST(N'2021-08-21T04:45:31.2430000' AS DateTime2), 450, N'www.google.com', 2)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([Id], [UserName], [Password]) VALUES (1, N'ajcv11', N'123456')
GO
INSERT [dbo].[User] ([Id], [UserName], [Password]) VALUES (2, N'juanv', N'54321')
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserClaim] ON 
GO
INSERT [dbo].[UserClaim] ([ClaimId], [UserId], [ClaimType], [ClaimValue]) VALUES (1, 1, N'CanAddProduct', 1)
GO
INSERT [dbo].[UserClaim] ([ClaimId], [UserId], [ClaimType], [ClaimValue]) VALUES (2, 1, N'CanAccessProducts', 0)
GO
INSERT [dbo].[UserClaim] ([ClaimId], [UserId], [ClaimType], [ClaimValue]) VALUES (3, 1, N'CanSaveProduct', 1)
GO
INSERT [dbo].[UserClaim] ([ClaimId], [UserId], [ClaimType], [ClaimValue]) VALUES (4, 1, N'CanDeleteProduct', 1)
GO
INSERT [dbo].[UserClaim] ([ClaimId], [UserId], [ClaimType], [ClaimValue]) VALUES (5, 2, N'CanAccessProducts', 0)
GO
SET IDENTITY_INSERT [dbo].[UserClaim] OFF
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (N'') FOR [UserName]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT (N'') FOR [Password]
GO
ALTER TABLE [dbo].[UserClaim] ADD  DEFAULT (N'') FOR [ClaimType]
GO
ALTER TABLE [dbo].[Peliculas]  WITH CHECK ADD  CONSTRAINT [FK_Peliculas_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Peliculas] CHECK CONSTRAINT [FK_Peliculas_Categorias_CategoriaId]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category_CategoryId]
GO
