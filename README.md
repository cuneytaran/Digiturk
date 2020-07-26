# Digiturk
.Net Developer Değerlendirme Projesi 
1- Kullandığım Desenler:
Repository Design: Veritabanı işlemlerini tekrar tekrar yazmak yerine bu işlemleri tekrar kullanılabilirlik amacıyla bu deseni kullandım.

2-.Net Core Web Api kullandım. Standart olarak Microsoft.AspNetCore.Mvc.Core
EntityFrameworkCore.SqlServerver kullandım.
Ekstra Kütüphane olarak: AutoMapper kullandım.
AutoMapper: Veri tablosundan veriyi aldınız ve ilgili modelle maplediniz. İlgili modelin veri tablosundan aldığı bilgiyle maplenebilmesi için veri tablosu kolonlarıyla, modelin propertyleri isim ve tip olarak eşleşmeli. Ancak siz bu veriyi modeldeki gibi direkt olarak programınızda kullanmak istenmez. Burada AutoMapper devreye girer. Model ile kendinizin oluşturduğu dto larınızı eşleştirme yapmanıza yarar.

3-Ekstra zamanım olsaydı. 
	a-Asp.Net Core Üyelik Sistemi(Asp.Net Core Identity)ile login ve register işlemi yaptırırdım. 
	b-Yapılan tüm hareketleri kayıt eden loglama ve logları farklı bir veritabanına kaydetme sistemi yazardım.
	c-Otomatik veritabanı yedekleme senkronizasyonu yapardım.
	d-Login ve verileri farklı veritabanlarında tutardım.


***************************************** Digiturk veritabanı scripti ****** ***********************************


USE [master]
GO
/****** Object:  Database [Digiturk]    Script Date: 25.07.2020 14:28:47 ******/
CREATE DATABASE [Digiturk]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Digiturk', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Digiturk.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Digiturk_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Digiturk_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Digiturk] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Digiturk].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Digiturk] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Digiturk] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Digiturk] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Digiturk] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Digiturk] SET ARITHABORT OFF 
GO
ALTER DATABASE [Digiturk] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Digiturk] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Digiturk] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Digiturk] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Digiturk] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Digiturk] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Digiturk] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Digiturk] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Digiturk] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Digiturk] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Digiturk] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Digiturk] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Digiturk] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Digiturk] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Digiturk] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Digiturk] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Digiturk] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Digiturk] SET RECOVERY FULL 
GO
ALTER DATABASE [Digiturk] SET  MULTI_USER 
GO
ALTER DATABASE [Digiturk] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Digiturk] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Digiturk] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Digiturk] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Digiturk] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Digiturk', N'ON'
GO
ALTER DATABASE [Digiturk] SET QUERY_STORE = OFF
GO
USE [Digiturk]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 25.07.2020 14:28:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ArticleTitle] [nvarchar](max) NULL,
	[ArticleContent] [nvarchar](max) NULL,
	[ArticleDate] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 25.07.2020 14:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NULL,
	[UserId] [int] NULL,
	[Comment] [nvarchar](max) NULL,
	[CommentDate] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 25.07.2020 14:28:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[PasswordHash] [varbinary](max) NULL,
	[PasswordSalt] [varbinary](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[UserMail] [nvarchar](100) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Articles] ON 
GO
INSERT [dbo].[Articles] ([ArticleId], [UserId], [ArticleTitle], [ArticleContent], [ArticleDate], [Active]) VALUES (1, 1, N'MAKALE BAŞLIĞI', N'MAKALE İÇERİĞİ VE AÇIKLAMASI', CAST(N'2020-08-23T00:00:00.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Articles] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 
GO
INSERT [dbo].[Comments] ([CommentId], [ArticleId], [UserId], [Comment], [CommentDate], [Active]) VALUES (4, 1, 1, N'MAKALE İÇİN YORUM', CAST(N'2020-07-28T00:00:00.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [RoleId], [PasswordHash], [PasswordSalt], [UserName], [UserMail], [Active]) VALUES (1, 1, 0x80BE2948004E3E0B8457278A33604D7779E4E2438B2F741F53903AF25E9E5732BBD6681A350295CFFC519BC248BBD44D69CE5E294F025F4CF4E1A244D7932842, 0x4BD32F822CB383E25121446866C201E4707207F5695247D3D41E8048A4CAB12D3E175F14C7F6B1AF72AC0EE391788ACB54300FE761CD52608E5608DC5C89F307EAC1BD66A74DB8D638A1260F8BBDD059F4A76FF4D44CCE8E1DFE70F0117E4F44760D729EE9BFA23CA1E28FF4AD8DD4A798CB1BC0E55DC47A622BDDB25C94A27A, N'Admin', N'cuneyt.aran@gmail.com', 1)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Users]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Articles]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
USE [master]
GO
ALTER DATABASE [Digiturk] SET  READ_WRITE 
GO

***************************************************************************************************************************

