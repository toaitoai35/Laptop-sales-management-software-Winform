USE [master]
GO
/****** Object:  Database [QLBH_ViTinhDB]    Script Date: 4/5/2020 2:49:51 PM ******/
CREATE DATABASE [QLBH_ViTinhDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLBH_ViTinhDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\QLBH_ViTinhDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLBH_ViTinhDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER01\MSSQL\DATA\QLBH_ViTinhDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [QLBH_ViTinhDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBH_ViTinhDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBH_ViTinhDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET RECOVERY FULL 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET  MULTI_USER 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLBH_ViTinhDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLBH_ViTinhDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QLBH_ViTinhDB', N'ON'
GO
ALTER DATABASE [QLBH_ViTinhDB] SET QUERY_STORE = OFF
GO
USE [QLBH_ViTinhDB]
GO
/****** Object:  Table [dbo].[tblHang]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHang](
	[MaHang] [nvarchar](50) NOT NULL,
	[TenHang] [nvarchar](50) NULL,
	[MaNhanHieu] [nvarchar](50) NULL,
	[SoLuong] [float] NULL,
	[DonGiaNhap] [float] NULL,
	[DonGiaBan] [float] NULL,
	[Anh] [nvarchar](200) NULL,
	[GhiChu] [nvarchar](500) NULL,
 CONSTRAINT [PK_tblHang] PRIMARY KEY CLUSTERED 
(
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHDChiTiet]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHDChiTiet](
	[MaHoaDon] [nvarchar](30) NOT NULL,
	[MaHang] [nvarchar](50) NOT NULL,
	[SoLuong] [float] NULL,
	[DonGiaBan] [float] NULL,
	[GiamGia] [float] NULL,
	[ThanhTien] [float] NULL,
 CONSTRAINT [PK_tblHDChiTiet] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC,
	[MaHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblHoadon]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblHoadon](
	[MaHoaDon] [nvarchar](30) NOT NULL,
	[MaNhanVien] [nvarchar](10) NULL,
	[NgayBan] [datetime] NULL,
	[MaKhach] [nvarchar](10) NULL,
	[TongTien] [float] NULL,
 CONSTRAINT [PK_tblHoadon] PRIMARY KEY CLUSTERED 
(
	[MaHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblKhachHang]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblKhachHang](
	[MaKhach] [nvarchar](10) NOT NULL,
	[TenKhach] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[DienThoai] [nvarchar](15) NULL,
	[GioiTinh] [nvarchar](50) NULL,
	[NgaySinh] [datetime] NULL,
 CONSTRAINT [PK_tblKhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKhach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNhanHieu]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNhanHieu](
	[MaNhanHieu] [nvarchar](50) NOT NULL,
	[TenNhanHieu] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblNhanHieu] PRIMARY KEY CLUSTERED 
(
	[MaNhanHieu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNhanVien]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNhanVien](
	[MaNhanVien] [nvarchar](10) NOT NULL,
	[TenNhanVien] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[DienThoai] [nvarchar](15) NULL,
	[GioiTinh] [nvarchar](15) NULL,
	[NgaySinh] [datetime] NULL,
 CONSTRAINT [PK_tblNhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNhanVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'aa', N'đ', N'Asus', 222, 12, 1111, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\macbookair-1.jpg', N'')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'dsd', N'dsdw', N'Asus', 44, 33, 33, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\Dell-Ins-3567.jpg', N'edee')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'H01', N'Asus Vivobook X507MA-BR317T', N'Asus', 18, 6000000, 6990000, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\Asus-X507UA-BR167T-1.png', N'CPU :	Intel, Celeron
RAM :	4 GB, DDR4
Ổ cứng :	SSD, 256 GB
Màn hình :	15.6 inchs, 1366 x 768 Pixels
Card màn hình :	Intel UHD Graphics, Tích hợp
Cổng kết nối :	LAN : Không, WIFI : 802.11 b/g/n
Hệ điều hành :	Windows 10 Home SL 64
Trọng lượng :	1.73 kg
Kích thước :	365 x 266 x 21.9 mm (W x D x H)
Xuất xứ :	Trung Quốc
Năm sản xuất :	2019')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'H02', N'Dell Inspiron N3567S', N'Dell', 25, 9999000, 10990000, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\Dell-Ins-3567.jpg', N'CPU :	Intel, Core i3
RAM :	4 GB, DDR4
Ổ cứng :	HDD, 1 TB
Màn hình :	15.6 inchs, 1366 x 768 Pixels
Card màn hình :	Intel HD Graphics 620, Tích hợp
Cổng kết nối :	LAN : 10/100 Mbps Ethernet controller, WIFI : 802.11ac
Hệ điều hành :	Ubuntu
Trọng lượng :	2.30 Kg
Kích thước :	380 x 260 x 23.65 mm')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'H03', N'Macbook Air 13 128GB MQD32SA/A (2017)', N'Apple', 15, 18990999, 21990000, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\macbookair-1.jpg', N'CPU :	Intel, Core i5
RAM :	8 GB, LPDDR3
Ổ cứng :	SSD, 128 GB
Màn hình :	13.3 inch, 1440 x 900 pixels
Card màn hình :	Intel HD Graphics 6000
Cổng kết nối :	LAN : 802.11ac Wi-Fi wireless networking, WIFI : IEEE 802.11a/b/g/n compatible
Hệ điều hành :	Mac Os
Trọng lượng :	1.35 Kg')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'H04', N'HP Pavilion 14-ce1014TU', N'HP', 51, 11000000, 12490000, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\LAPTOP HP-1.jpg', N'CPU :	Intel, Core i3
RAM :	4 GB, DDR4
Ổ cứng :	HDD 5400rpm, 500 GB
Màn hình :	14.0 inchs, 1920 x 1080 Pixels
Card màn hình :	Intel® HD Graphics 600, Tích hợp
Cổng kết nối :	LAN : 10/100/1000 Mbps, WIFI : 802.11 b/g/n
Hệ điều hành :	Windows 10 Home SL 64
Trọng lượng :	1,53kg
Kích thước :	Dài 324 mm - Rộng 227.6 mm - Dày 19.9 mm
Xuất xứ :	Trung Quốc
Năm sản xuất :	2019')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'q', N'q', N'Dell', 3, 3, 3, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\macbookair-1.jpg', N'33')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'qqq', N'ww2', N'Apple', 21, 22, 222, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\New folder\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\LAPTOP HP-1.jpg', N'đ')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N's', N's', N'Asus', 2, 33, 36, N'E:\IT\Sortware\CN5\PRN292(.NET and C#)\project c#\Quản Lý Của Hàng Bán Máy Vi Tính\Hình ảnh\macbookair-1.jpg', N'a')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'sss', N'qqq', N'Asus', 222, 22, 2, N'C:\Users\Admin\OneDrive\Máy tính\IMG_0662.JPG', N'222')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'ssw', N'qq', N'Dell', 22, 22, 33, N'C:\Users\Admin\OneDrive\Máy tính\a0861625887_10.jpg', N'đe')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'xasx', N'xsxxx', N'Apple', 2, 2, 2, N'C:\Users\Admin\OneDrive\Máy tính\2019_July2.jpg', N'ss')
INSERT [dbo].[tblHang] ([MaHang], [TenHang], [MaNhanHieu], [SoLuong], [DonGiaNhap], [DonGiaBan], [Anh], [GhiChu]) VALUES (N'zx', N'sw', N'Dell', 2, 2, 2, N'C:\Users\Admin\OneDrive\Máy tính\IMG_0662.JPG', N'xsx')
INSERT [dbo].[tblHDChiTiet] ([MaHoaDon], [MaHang], [SoLuong], [DonGiaBan], [GiamGia], [ThanhTien]) VALUES (N'HD01', N'H01', 1, 10990000, 0, 10990000)
INSERT [dbo].[tblHDChiTiet] ([MaHoaDon], [MaHang], [SoLuong], [DonGiaBan], [GiamGia], [ThanhTien]) VALUES (N'HDB452020_012302', N'H01', 2, 6990000, 0, 13980000)
INSERT [dbo].[tblHDChiTiet] ([MaHoaDon], [MaHang], [SoLuong], [DonGiaBan], [GiamGia], [ThanhTien]) VALUES (N'HDB452020_012302', N'H03', 2, 21990000, 3, 42660600)
INSERT [dbo].[tblHDChiTiet] ([MaHoaDon], [MaHang], [SoLuong], [DonGiaBan], [GiamGia], [ThanhTien]) VALUES (N'HDB452020_013810', N'H03', 3, 21990000, 0, 65970000)
INSERT [dbo].[tblHDChiTiet] ([MaHoaDon], [MaHang], [SoLuong], [DonGiaBan], [GiamGia], [ThanhTien]) VALUES (N'HDB452020_013810', N'H04', 4, 12490000, 0, 49960000)
INSERT [dbo].[tblHDChiTiet] ([MaHoaDon], [MaHang], [SoLuong], [DonGiaBan], [GiamGia], [ThanhTien]) VALUES (N'HDB452020_023428', N'H02', 2, 10990000, 0, 21980000)
INSERT [dbo].[tblHDChiTiet] ([MaHoaDon], [MaHang], [SoLuong], [DonGiaBan], [GiamGia], [ThanhTien]) VALUES (N'HDB452020_023428', N'H04', 4, 12490000, 6, 46962400)
INSERT [dbo].[tblHoadon] ([MaHoaDon], [MaNhanVien], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HD01', N'NV02', CAST(N'2020-02-21T00:00:00.000' AS DateTime), N'KH01', 10990000)
INSERT [dbo].[tblHoadon] ([MaHoaDon], [MaNhanVien], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB452020_012302', N'NV02', CAST(N'2020-04-05T00:00:00.000' AS DateTime), N'KH03', 56640600)
INSERT [dbo].[tblHoadon] ([MaHoaDon], [MaNhanVien], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB452020_013810', N'NV02', CAST(N'2020-04-05T00:00:00.000' AS DateTime), N'KH03', 115930000)
INSERT [dbo].[tblHoadon] ([MaHoaDon], [MaNhanVien], [NgayBan], [MaKhach], [TongTien]) VALUES (N'HDB452020_023428', N'NV03', CAST(N'2020-04-05T00:00:00.000' AS DateTime), N'KH03', 68942400)
INSERT [dbo].[tblKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'KH01', N'Bành Thị Thu', N' 103/1Bình Trị Ðông A, Quận Bình Tân, TPHCM', N'(097) 970-9479', N'Nam', CAST(N'1990-05-21T00:00:00.000' AS DateTime))
INSERT [dbo].[tblKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'KH02', N'Phúa', N'ss', N'( 03) 443-0007', N'Nu', CAST(N'1995-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tblKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'KH03', N'Hồng Hạnh', N'hòa hoa thám', N'(444) 444-444', N'Nam', CAST(N'2020-04-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tblKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'KH04', N'Võ Hữu Thành', N'Nam Kì Khởi Nghĩa', N'(363) 656-6266', N'Nu', CAST(N'1989-04-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tblKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'KH05', N'búa', N'aaa', N'(987) 215-0505', N'Nữ', CAST(N'1999-11-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tblKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'qqq', N'hế lô', N'đ', N'(777) 777-7777', N'Nu', CAST(N'2020-04-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tblKhachHang] ([MaKhach], [TenKhach], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'w', N'hé lo quan', N'777777', N'(777) 777-7777', N'Nu', CAST(N'2020-04-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'Apple', N'Apple')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'Asus', N'Asus')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'dd', N'dds')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'Dell', N'Dell')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'HP', N'HP')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'ss', N's')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'sss', N'ssws')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'vv', N'vv')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'xx', N'xx')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'za', N'aa')
INSERT [dbo].[tblNhanHieu] ([MaNhanHieu], [TenNhanHieu]) VALUES (N'zz', N'zz')
INSERT [dbo].[tblNhanVien] ([MaNhanVien], [TenNhanVien], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'aww', N'a', N'7', N'(777) 777-7777', N'Nữ', CAST(N'2020-04-15T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNhanVien] ([MaNhanVien], [TenNhanVien], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'NV01', N'Lê Hoài Phúc', N'598 Điện Biên Phủ', N'0344300078', N'Nam', CAST(N'1999-03-20T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNhanVien] ([MaNhanVien], [TenNhanVien], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'NV02', N'Võ Hữu Lộc', N'12 Bạch Đằng, Q Bình Thạnh', N'0123456789', N'Nam', CAST(N'1997-12-05T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNhanVien] ([MaNhanVien], [TenNhanVien], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'NV03', N'Phạm Thu Thủy', N'122 CMT8, Quận 10', N'4579465625', N'Nữ', CAST(N'1995-06-12T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNhanVien] ([MaNhanVien], [TenNhanVien], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N's', N's', N'rrt', N'(777) 777-7777', N'Nữ', CAST(N'2020-04-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tblNhanVien] ([MaNhanVien], [TenNhanVien], [DiaChi], [DienThoai], [GioiTinh], [NgaySinh]) VALUES (N'sq', N'sq', N'77777777777', N'(777) 777-7777', N'Nam', CAST(N'2020-04-04T00:00:00.000' AS DateTime))
ALTER TABLE [dbo].[tblHang]  WITH CHECK ADD  CONSTRAINT [FK_tblHang_tblNhanHieu1] FOREIGN KEY([MaNhanHieu])
REFERENCES [dbo].[tblNhanHieu] ([MaNhanHieu])
GO
ALTER TABLE [dbo].[tblHang] CHECK CONSTRAINT [FK_tblHang_tblNhanHieu1]
GO
ALTER TABLE [dbo].[tblHDChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_tblHDChiTiet_tblHang] FOREIGN KEY([MaHang])
REFERENCES [dbo].[tblHang] ([MaHang])
GO
ALTER TABLE [dbo].[tblHDChiTiet] CHECK CONSTRAINT [FK_tblHDChiTiet_tblHang]
GO
ALTER TABLE [dbo].[tblHDChiTiet]  WITH CHECK ADD  CONSTRAINT [FK_tblHDChiTiet_tblHoadon] FOREIGN KEY([MaHoaDon])
REFERENCES [dbo].[tblHoadon] ([MaHoaDon])
GO
ALTER TABLE [dbo].[tblHDChiTiet] CHECK CONSTRAINT [FK_tblHDChiTiet_tblHoadon]
GO
ALTER TABLE [dbo].[tblHoadon]  WITH CHECK ADD  CONSTRAINT [FK_tblHoadon_tblKhachHang] FOREIGN KEY([MaKhach])
REFERENCES [dbo].[tblKhachHang] ([MaKhach])
GO
ALTER TABLE [dbo].[tblHoadon] CHECK CONSTRAINT [FK_tblHoadon_tblKhachHang]
GO
ALTER TABLE [dbo].[tblHoadon]  WITH CHECK ADD  CONSTRAINT [FK_tblHoadon_tblNhanVien] FOREIGN KEY([MaNhanVien])
REFERENCES [dbo].[tblNhanVien] ([MaNhanVien])
GO
ALTER TABLE [dbo].[tblHoadon] CHECK CONSTRAINT [FK_tblHoadon_tblNhanVien]
GO
/****** Object:  StoredProcedure [dbo].[DeleteHang]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteHang](
@MaHang varchar(10)
)
as
begin 
delete from tblHang
where MaHang = @MaHang

end
GO
/****** Object:  StoredProcedure [dbo].[DeleteHDChiTiet]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteHDChiTiet](
@MaHoaDon varchar(30)
)
as
begin 
delete from tblHDChiTiet
where MaHoaDon = @MaHoaDon

end
GO
/****** Object:  StoredProcedure [dbo].[DeleteHoaDon]    Script Date: 4/5/2020 2:49:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteHoaDon](
@MaHoaDon varchar(30)
)
as
begin 
delete from tblHoadon
where MaHoaDon = @MaHoaDon

end
GO
/****** Object:  StoredProcedure [dbo].[DeleteKhachHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteKhachHang](
@MaKhach varchar(10)
)
as
begin 
delete from tblKhachHang
where MaKhach = @MaKhach

end
GO
/****** Object:  StoredProcedure [dbo].[DeleteNhanHieu]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteNhanHieu](
@MaNhanHieu varchar(50)
)
as
begin 
delete from tblNhanHieu
where MaNhanHieu = @MaNhanHieu

end
GO
/****** Object:  StoredProcedure [dbo].[DeleteNhanVien]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteNhanVien](
@MaNhanVien varchar(10)
)
as
begin 
delete from tblNhanVien
where MaNhanVien = @MaNhanVien

end
GO
/****** Object:  StoredProcedure [dbo].[FindHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[FindHang](@Filter varchar(50))
as
begin
select * from tblHang
where TenHang like  '%'+@Filter+'%'

end
GO
/****** Object:  StoredProcedure [dbo].[FindKhachHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[FindKhachHang](@Filter varchar(50))
as
begin
select * from tblKhachHang
where TenKhach like  '%'+@Filter+'%'

end
GO
/****** Object:  StoredProcedure [dbo].[FindNhanVien]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[FindNhanVien](@Filter varchar(50))
as
begin
select * from tblNhanVien
where TenNhanVien like  '%'+@Filter+'%'

end
GO
/****** Object:  StoredProcedure [dbo].[GetListHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetListHang]
as
begin
SET NOCOUNT ON;
select * 
from tblHang
end
GO
/****** Object:  StoredProcedure [dbo].[GetListKhachHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetListKhachHang]
as
begin
SET NOCOUNT ON;
select * 
from tblKhachHang
end
GO
/****** Object:  StoredProcedure [dbo].[GetListNhanHieu]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--------Nhan Hieu-------------
--GetListNhanHieu---
create proc [dbo].[GetListNhanHieu]
as
begin
SET NOCOUNT ON;
select * 
from tblNhanHieu
end
GO
/****** Object:  StoredProcedure [dbo].[GetListNhanVien]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetListNhanVien]
as
begin
SET NOCOUNT ON;
select * 
from tblNhanVien
end
GO
/****** Object:  StoredProcedure [dbo].[InsertHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[InsertHang](
@MaHang nvarchar(50),
@TenHang nvarchar(50),
@MaNhanHieu nvarchar(50),
@SoLuong float,
@DonGiaNhap float,
@DonGiaBan float,
@Anh nvarchar(200),
@GhiChu nvarchar(500)
)
as
begin
Insert into tblHang(MaHang,TenHang, MaNhanHieu, SoLuong, DonGiaNhap, DonGiaBan, Anh, GhiChu)
values (@MaHang,@TenHang, @MaNhanHieu, @SoLuong, @DonGiaNhap, @DonGiaBan, @Anh, @GhiChu)
end
GO
/****** Object:  StoredProcedure [dbo].[InsertHDChiTiet]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[InsertHDChiTiet](
@MaHoaDon nvarchar(30),
@MaHang nvarchar(50),
@SoLuong float,
@DonGiaBan float,
@GiamGia float,
@ThanhTien float
)
as
begin
Insert into tblHDChiTiet(MaHoaDon,MaHang, SoLuong, DonGiaBan, GiamGia,ThanhTien)
values (@MaHoaDon,@MaHang, @SoLuong, @DonGiaBan, @GiamGia,@ThanhTien)
end
GO
/****** Object:  StoredProcedure [dbo].[InsertHoaDon]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[InsertHoaDon](
@MaHoaDon nvarchar(30),
@MaNhanVien nvarchar(10),
@NgayBan datetime,
@MaKhach nvarchar(10),
@TongTien float
)
as
begin
Insert into tblHoadon(MaHoaDon,MaNhanVien, NgayBan, MaKhach, TongTien)
values (@MaHoaDon,@MaNhanVien, @NgayBan, @MaKhach, @TongTien)
end
GO
/****** Object:  StoredProcedure [dbo].[InsertKhachHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[InsertKhachHang](
@MaKhach nvarchar(10),
@TenKhach nvarchar(50),
@DiaChi nvarchar(50),
@DienThoai nvarchar(15),
@GioiTinh nvarchar(50),
@NgaySinh datetime
)
as
begin
Insert into tblKhachHang(MaKhach,TenKhach, DiaChi, DienThoai, GioiTinh, NgaySinh)
values (@MaKhach,@TenKhach, @DiaChi, @DienThoai, @GioiTinh, @NgaySinh)
end
GO
/****** Object:  StoredProcedure [dbo].[InsertNhanHieu]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[InsertNhanHieu](
@MaNhanHieu nvarchar(50),
@TenNhanHieu nvarchar(50)
)
as
begin
Insert into tblNhanHieu(MaNhanHieu,TenNhanHieu)
values (@MaNhanHieu,@TenNhanHieu)
end
GO
/****** Object:  StoredProcedure [dbo].[InsertNhanVien]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc  [dbo].[InsertNhanVien](
@MaNhanVien nvarchar(10),
@TenNhanVien nvarchar(50),
@DiaChi nvarchar(50),
@DienThoai nvarchar(15),
@GioiTinh nvarchar(50),
@NgaySinh datetime
)
as
begin
Insert into tblNhanVien(MaNhanVien,TenNhanVien, DiaChi, DienThoai, GioiTinh, NgaySinh)
values (@MaNhanVien,@TenNhanVien, @DiaChi, @DienThoai, @GioiTinh, @NgaySinh)
end
GO
/****** Object:  StoredProcedure [dbo].[UpdateHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateHang](
@MaHang nvarchar(10),
@TenHang nvarchar(50),
@MaNhanHieu nvarchar(50),
@SoLuong float,
@DonGiaNhap float,
@DonGiaBan float,
@Anh nvarchar(200),
@GhiChu nvarchar(500)
)
as
begin 
Update tblHang
set
						MaHang = @MaHang,
						TenHang = @TenHang,
						MaNhanHieu = @MaNhanHieu,
						SoLuong = @SoLuong,
						DonGiaNhap = @DonGiaNhap,
						DonGiaBan = @DonGiaBan,
						Anh = @Anh,
						GhiChu = @GhiChu
WHERE MaHang= @MaHang

end
GO
/****** Object:  StoredProcedure [dbo].[UpdateKhachHang]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateKhachHang](
@MaKhach nvarchar(10),
@TenKhach nvarchar(50),
@DiaChi nvarchar(50),
@DienThoai nvarchar(15),
@GioiTinh nvarchar(50),
@NgaySinh datetime
)
as
begin 
Update tblKhachHang
set
						MaKhach = @MaKhach,
						TenKhach = @TenKhach,
						DiaChi = @DiaChi,
						DienThoai = @DienThoai,
						GioiTinh = @GioiTinh,
						NgaySinh = @NgaySinh
WHERE MaKhach= @MaKhach

end
GO
/****** Object:  StoredProcedure [dbo].[UpdateNhanHieu]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateNhanHieu](
@MaNhanHieu nvarchar(50),
@TenNhanHieu nvarchar(50)
)
as
begin 
Update tblNhanHieu
set
						MaNhanHieu = @MaNhanHieu,
						TenNhanHieu = @TenNhanHieu
WHERE MaNhanHieu= @MaNhanHieu

end
GO
/****** Object:  StoredProcedure [dbo].[UpdateNhanVien]    Script Date: 4/5/2020 2:49:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateNhanVien](
@MaNhanVien nvarchar(10),
@TenNhanVien nvarchar(50),
@DiaChi nvarchar(50),
@DienThoai nvarchar(15),
@GioiTinh nvarchar(50),
@NgaySinh datetime
)
as
begin 
Update tblNhanVien
set
						MaNhanVien = @MaNhanVien,
						TenNhanVien = @TenNhanVien,
						DiaChi = @DiaChi,
						DienThoai = @DienThoai,
						GioiTinh = @GioiTinh,
						NgaySinh = @NgaySinh
WHERE MaNhanVien= @MaNhanVien

end
GO
USE [master]
GO
ALTER DATABASE [QLBH_ViTinhDB] SET  READ_WRITE 
GO
