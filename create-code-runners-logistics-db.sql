/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.2027)
    Source Database Engine Edition : Microsoft SQL Server Enterprise Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [code-runners-logistics]    Script Date: 10/6/2020 10:27:28 PM ******/
CREATE DATABASE [code-runners-logistics]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'code-runners-logistics', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\code-runners-logistics.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'code-runners-logistics_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\code-runners-logistics_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [code-runners-logistics] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [code-runners-logistics].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [code-runners-logistics] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [code-runners-logistics] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [code-runners-logistics] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [code-runners-logistics] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [code-runners-logistics] SET ARITHABORT OFF 
GO
ALTER DATABASE [code-runners-logistics] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [code-runners-logistics] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [code-runners-logistics] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [code-runners-logistics] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [code-runners-logistics] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [code-runners-logistics] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [code-runners-logistics] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [code-runners-logistics] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [code-runners-logistics] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [code-runners-logistics] SET  DISABLE_BROKER 
GO
ALTER DATABASE [code-runners-logistics] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [code-runners-logistics] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [code-runners-logistics] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [code-runners-logistics] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [code-runners-logistics] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [code-runners-logistics] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [code-runners-logistics] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [code-runners-logistics] SET RECOVERY FULL 
GO
ALTER DATABASE [code-runners-logistics] SET  MULTI_USER 
GO
ALTER DATABASE [code-runners-logistics] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [code-runners-logistics] SET DB_CHAINING OFF 
GO
ALTER DATABASE [code-runners-logistics] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [code-runners-logistics] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [code-runners-logistics] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'code-runners-logistics', N'ON'
GO
ALTER DATABASE [code-runners-logistics] SET QUERY_STORE = OFF
GO
USE [code-runners-logistics]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [code-runners-logistics]
GO
/****** Object:  Table [dbo].[Edges]    Script Date: 10/6/2020 10:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Edges](
	[From_Id] [int] NOT NULL,
	[To_Id] [int] NOT NULL,
 CONSTRAINT [PK_Edges] PRIMARY KEY CLUSTERED 
(
	[From_Id] ASC,
	[To_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LastGraphIdentity]    Script Date: 10/6/2020 10:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LastGraphIdentity](
	[Identity] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LogisticCenter]    Script Date: 10/6/2020 10:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogisticCenter](
	[Id] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nodes]    Script Date: 10/6/2020 10:27:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Settlements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Edges] ([From_Id], [To_Id]) VALUES (25, 29)
INSERT [dbo].[Edges] ([From_Id], [To_Id]) VALUES (27, 25)
INSERT [dbo].[Edges] ([From_Id], [To_Id]) VALUES (27, 26)
INSERT [dbo].[Edges] ([From_Id], [To_Id]) VALUES (28, 27)
INSERT [dbo].[Edges] ([From_Id], [To_Id]) VALUES (28, 29)
INSERT [dbo].[LastGraphIdentity] ([Identity]) VALUES (N'0010100100110100010110010')
INSERT [dbo].[LogisticCenter] ([Id]) VALUES (28)
SET IDENTITY_INSERT [dbo].[Nodes] ON 

INSERT [dbo].[Nodes] ([Id], [Name]) VALUES (25, N'Sofia')
INSERT [dbo].[Nodes] ([Id], [Name]) VALUES (26, N'Varna')
INSERT [dbo].[Nodes] ([Id], [Name]) VALUES (27, N'Plovdiv')
INSERT [dbo].[Nodes] ([Id], [Name]) VALUES (28, N'Stara Zagora')
INSERT [dbo].[Nodes] ([Id], [Name]) VALUES (29, N'Burgas')
SET IDENTITY_INSERT [dbo].[Nodes] OFF
ALTER TABLE [dbo].[Edges]  WITH CHECK ADD  CONSTRAINT [FK_Edges_Settlements] FOREIGN KEY([From_Id])
REFERENCES [dbo].[Nodes] ([Id])
GO
ALTER TABLE [dbo].[Edges] CHECK CONSTRAINT [FK_Edges_Settlements]
GO
ALTER TABLE [dbo].[Edges]  WITH CHECK ADD  CONSTRAINT [FK_Edges_Settlements1] FOREIGN KEY([To_Id])
REFERENCES [dbo].[Nodes] ([Id])
GO
ALTER TABLE [dbo].[Edges] CHECK CONSTRAINT [FK_Edges_Settlements1]
GO
USE [master]
GO
ALTER DATABASE [code-runners-logistics] SET  READ_WRITE 
GO
