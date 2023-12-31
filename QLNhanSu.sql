USE [master]
GO
/****** Object:  Database [QLNhanSu]    Script Date: 01/10/2023 3:16:59 SA ******/
CREATE DATABASE [QLNhanSu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLNhanSu', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLNhanSu.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLNhanSu_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\QLNhanSu_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLNhanSu] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLNhanSu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLNhanSu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLNhanSu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLNhanSu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLNhanSu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLNhanSu] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLNhanSu] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLNhanSu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLNhanSu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLNhanSu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLNhanSu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLNhanSu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLNhanSu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLNhanSu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLNhanSu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLNhanSu] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLNhanSu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLNhanSu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLNhanSu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLNhanSu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLNhanSu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLNhanSu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLNhanSu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLNhanSu] SET RECOVERY FULL 
GO
ALTER DATABASE [QLNhanSu] SET  MULTI_USER 
GO
ALTER DATABASE [QLNhanSu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLNhanSu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLNhanSu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLNhanSu] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLNhanSu] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLNhanSu', N'ON'
GO
USE [QLNhanSu]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 01/10/2023 3:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [char](6) NOT NULL,
	[TenNV] [nchar](20) NULL,
	[NgaySinh] [datetime] NULL,
	[MaPB] [char](2) NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PhongBan]    Script Date: 01/10/2023 3:17:00 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PhongBan](
	[MaPB] [char](2) NOT NULL,
	[TenPB] [nchar](30) NULL,
 CONSTRAINT [PK_PhongBan] PRIMARY KEY CLUSTERED 
(
	[MaPB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [MaPB]) VALUES (N'NV0001', N'Trần Văn Nam        ', CAST(N'1980-08-15 00:00:00.000' AS DateTime), N'1 ')
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [MaPB]) VALUES (N'NV0002', N'Nguyễn Thị Yến      ', CAST(N'1981-05-25 00:00:00.000' AS DateTime), N'2 ')
INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [NgaySinh], [MaPB]) VALUES (N'NV0003', N'Lý Kim Tuyền        ', CAST(N'1979-08-10 00:00:00.000' AS DateTime), N'1 ')
INSERT [dbo].[PhongBan] ([MaPB], [TenPB]) VALUES (N'1 ', N'Kinh Doanh                    ')
INSERT [dbo].[PhongBan] ([MaPB], [TenPB]) VALUES (N'2 ', N'Kế Toán                       ')
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_PhongBan] FOREIGN KEY([MaPB])
REFERENCES [dbo].[PhongBan] ([MaPB])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_PhongBan]
GO
USE [master]
GO
ALTER DATABASE [QLNhanSu] SET  READ_WRITE 
GO
