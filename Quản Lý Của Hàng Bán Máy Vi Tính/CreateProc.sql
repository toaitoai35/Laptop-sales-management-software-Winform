Use  QLBH_ViTinhDB

GO

----------------------------------------Nhan Hieu-------------
--GetListNhanHieu---
go
create proc [GetListNhanHieu]
as
begin
SET NOCOUNT ON;
select * 
from tblNhanHieu
end
--Them NhanHieu--
go
create proc  [InsertNhanHieu](
@MaNhanHieu nvarchar(50),
@TenNhanHieu nvarchar(50)
)
as
begin
Insert into tblNhanHieu(MaNhanHieu,TenNhanHieu)
values (@MaNhanHieu, @TenNhanHieu)
end
--Sửa NhanHieu--
go
create proc [UpdateNhanHieu](
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
-- Xóa NhanHieu--
go
create proc [DeleteNhanHieu](
@MaNhanHieu varchar(50)
)
as
begin 
delete from tblNhanHieu
where MaNhanHieu = @MaNhanHieu

end
----------------------------------------Khách Hàng------
--GetListKhachHang---
go	
create proc [GetListKhachHang]
as
begin
SET NOCOUNT ON;
select * 
from tblKhachHang
end
--Them KhachHang--
go
create proc  [InsertKhachHang](
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
values (@MaKhach, @TenKhach, @DiaChi, @DienThoai, @GioiTinh, @NgaySinh)
end
--Sửa khachHang--
go
create proc [UpdateKhachHang](
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
-- Xóa KhachHang--
go
create proc [DeleteKhachHang](
@MaKhach varchar(10)
)
as
begin 
delete from tblKhachHang
where MaKhach = @MaKhach

end
-- Tìm kiếm KhachHang--
go 
create proc [FindKhachHang](@Filter varchar(50))
as
begin
select * from tblKhachHang
where TenKhach like  '%'+@Filter+'%'

end
------------------------------------------Nhân Viên------
--GetListNhanVien---
go	
create proc [GetListNhanVien]
as
begin
SET NOCOUNT ON;
select * 
from tblNhanVien
end
--Them NhanVien--
go
create proc  [InsertNhanVien](
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
values (@MaNhanVien, @TenNhanVien, @DiaChi, @DienThoai, @GioiTinh, @NgaySinh)
end
--Sửa NhanVien--
go
create proc [UpdateNhanVien](
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
-- Xóa NhanVien--
go
create proc [DeleteNhanVien](
@MaNhanVien varchar(10)
)
as
begin 
delete from tblNhanVien
where MaNhanVien = @MaNhanVien

end
-- Tìm kiếm NhanVien--
go 
create proc [FindNhanVien](@Filter varchar(50))
as
begin
select * from tblNhanVien
where TenNhanVien like  '%'+@Filter+'%'

end
-----------------------------------------Hàng hóa-----------
--GetListHang---
go	
create proc [GetListHang]
as
begin
SET NOCOUNT ON;
select * 
from tblHang
end
--Them Hang--
go
create proc  [InsertHang](
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
values (@MaHang, @TenHang, @MaNhanHieu, @SoLuong, @DonGiaNhap, @DonGiaBan, @Anh, @GhiChu)
end
--Sửa Hang--
go
create proc [UpdateHang](
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
-- Xóa Hang--
go
create proc [DeleteHang](
@MaHang varchar(10)
)
as
begin 
delete from tblHang
where MaHang = @MaHang

end
-- Tìm kiếm NhanVien--
go 
create proc [FindHang](@Filter varchar(50))
as
begin
select * from tblHang
where TenHang like  '%'+@Filter+'%'

end
-----------------------------------------Hoa Don-----------

--them Hoa Don---
go
create proc  [InsertHoaDon](
@MaHoaDon nvarchar(30),
@MaNhanVien nvarchar(10),
@NgayBan datetime,
@MaKhach nvarchar(10),
@TongTien float
)
as
begin
Insert into tblHoadon(MaHoaDon,MaNhanVien, NgayBan, MaKhach, TongTien)
values (@MaHoaDon, @MaNhanVien, @NgayBan, @MaKhach, @TongTien)
end
-- Xóa HoaDon --
go
create proc [DeleteHoaDon](
@MaHoaDon varchar(30)
)
as
begin 
delete from tblHoadon
where MaHoaDon = @MaHoaDon

end
-----------------------------------HoaDonChiTiet-------------------------

--them HDChiTiet---
go
create proc  [InsertHDChiTiet](
@MaHoaDon nvarchar(30),
@MaHang nvarchar(50),
@SoLuong float,
@DonGiaBan float,
@GiamGia float,
@ThanhTien float
)
as
begin
Insert into tblHDChiTiet
values (@MaHoaDon, @MaHang, @SoLuong, @DonGiaBan, @GiamGia, @ThanhTien)
end
-- Xóa HDChiTiet --
go
create proc [DeleteHDChiTiet](
@MaHoaDon varchar(30)
)
as
begin 
delete from tblHDChiTiet
where MaHoaDon = @MaHoaDon
end
-- Tìm kiếm HoaDon--
go 
create proc [FindMaHoaDon](@Filter varchar(50))
as
begin
SELECT * FROM tblHoadon 
where MaHoaDon like  '%'+@Filter+'%'
end
go


--- Get Field Values HoaDon ---

create proc [GetNgayBanHD] @MaHoaDon nvarchar(30)
as
SELECT NgayBan FROM tblHoadon WHERE MaHoaDon = @MaHoaDon
go

create proc [GetMaNhanVienHD] @MaHoaDon nvarchar(30)
as
SELECT MaNhanVien FROM tblHoadon WHERE MaHoaDon = @MaHoaDon
go

create proc [GetMaKhachHD] @MaHoaDon nvarchar(30)
as
SELECT MaKhach FROM tblHoadon WHERE MaHoaDon = @MaHoaDon
go

create proc [GetTongTienHD] @MaHoaDon nvarchar(30)
as
SELECT TongTien FROM tblHoadon WHERE MaHoaDon = @MaHoaDon
go

create proc [GetMaHoaDonHD] @MaHoaDon nvarchar(30)
as
SELECT MaHoaDon FROM tblHoadon WHERE MaHoaDon = @MaHoaDon
go


--- ComboBox HoaDon ---

create proc [GetMaKhachToCbxHD]
as
SELECT MaKhach, TenKhach FROM tblKhachHang
go

create proc [GetMaNhanVienToCbxHD]
as
SELECT MaNhanVien, TenNhanVien FROM tblNhanVien
go

create proc [GetMaHangToCbxHD]
as
SELECT MaHang, TenHang FROM tblHang
go

create proc [GetMaHoaDonToCbxHD]
as
SELECT MaHoaDon FROM tblHoaDon
go

-- Lấy Mã Hàng từ Hóa đơn chi tiết --

create proc [GetMaHangHD2Param] @MaHang nvarchar(50), @MaHoaDon nvarchar(30)
as
SELECT MaHang FROM tblHDChiTiet WHERE MaHang = @MaHang AND MaHoaDon = @MaHoaDon
go

-- Insert Hoa don --
create proc [InsertHoaDon] 
@MaHoaDon nvarchar(30),
@MaNhanVien nvarchar(10),
@NgayBan datetime,
@MaKhach nvarchar(10),
@TongTien float
as
INSERT INTO tblHoadon(MaHoaDon, NgayBan, MaNhanVien, MaKhach, TongTien) VALUES (@MaHoaDon, @NgayBan, @MaNhanVien, @MaKhach, @TongTien)
go


-- Insert Hoa don Chi tiet --
create proc [InsertHDChiTiet] 
@MaHoaDon nvarchar(30),
@MaHang nvarchar(50),
@SoLuong float,
@DonGiaBan float,
@GiamGia float,
@ThanhTien float
as
INSERT INTO tblHoadon VALUES (@MaHoaDon, @MaHang, @SoLuong, @DonGiaBan, @GiamGia, @ThanhTien)
go


-- Get So Luong = Ma hang --
create proc [GetSoLuongBaseOnMaHangHD] @MaHang nvarchar(50)
as
SELECT SoLuong FROM tblHang WHERE MaHang = @MaHang
go

-- Update So Luong -- 
create proc [UpdateSoLuong] @MaHang nvarchar(50), @SoLuong float
as
UPDATE tblHang SET SoLuong = @SoLuong WHERE MaHang = @MaHang
go

-- Update Tong Tien --
create proc [UpdateTongTien] @TongTien float, @MaHoaDon nvarchar(30)
as
UPDATE tblHoadon SET TongTien = @TongTien WHERE MaHoaDon = @MaHoaDon
go

-- Get TenNhanVien -- 
create proc [GetTenNhanVien] @MaNhanVien nvarchar(10)
as
Select TenNhanVien from tblNhanVien where MaNhanVien = @MaNhanVien
go

-- Get Data from KhachHang --
create proc [GetTenKhach] @MaKhach nvarchar(10)
as
select TenKhach from tblKhachHang where MaKhach = @MaKhach
go

create proc [GetDiaChi] @MaKhach nvarchar(10)
as
select TenKhach from tblKhachHang where MaKhach = @MaKhach
go

create proc [GetDienThoai] @MaKhach nvarchar(10)
as
select TenKhach from tblKhachHang where MaKhach = @MaKhach
go

-- Get data from tblHDChiTiet --
create proc [GetMaHangAndSoLuong] @MaHoaDon nvarchar(30)
as
SELECT MaHang, SoLuong FROM tblHDChiTiet WHERE MaHoaDon = @MaHoaDon
go


-- Get data from tblHoaDon --
create proc [GetSoLuong] @MaHoaDon nvarchar(30)
as
select Soluong from tblHoaDon where MaHoaDon = @MaHoaDon
go

-- Delete from tblHDChiTiet -- 

create proc [DeleteHoaDonChiTiet] (@MaHoaDon nvarchar(30), @MaHang nvarchar(50))
as
DELETE FROM tblHDChiTiet
WHERE MaHoaDon = @MaHoaDon
	AND MaHang = @MaHang
go

-- Get SoLuong from tblHang --
create proc [GetSLHang] @MaHang nvarchar (50)
as
SELECT SoLuong FROM tblHang WHERE MaHang = @MaHang
go

create proc [GetDonGiaBan] @MaHang nvarchar(50)
as
select DonGiaban from tblHang where MaHang = @MaHang
go

create proc [GetTenHang] @MaHang nvarchar(50)
as
select TenHang from tblHang where MaHang = @MaHang
go

-- Get Thong Tin Hoa Don --
create proc [GetThongTinHD] @MaHoaDon nvarchar(30)
as
SELECT a.MaHoaDon, a.NgayBan, a.TongTien, b.TenKhach, b.DiaChi, b.DienThoai, c.TenNhanVien
FROM tblHoadon AS a, tblKhachHang AS b, tblNhanVien AS c
WHERE a.MaHoaDon = @MaHoaDon
	AND a.MaKhach = b.MaKhach
	AND a.MaNhanVien = c.MaNhanVien;
go


-- Get Thong Tin Hang --
create proc [GetThongTinHang] @MaHoaDon nvarchar(30)
as
SELECT b.TenHang, a.SoLuong, b.DonGiaBan, a.GiamGia, a.ThanhTien
FROM tblHDChiTiet AS a , tblHang AS b
WHERE a.MaHoaDon = @MaHoaDon
	AND a.MaHang = b.MaHang
go


-- Get ten nhan hieu --
create proc [GetTenNhanHieu] @Param nvarchar(50)
as
SELECT TenNhanHieu FROM tblNhanHieu WHERE MaNhanHieu = @Param
go

-- get dir Anh --
create proc [GetAnhHang] @Param nvarchar(50)
as
SELECT Anh FROM tblHang WHERE MaHang = @Param
go

-- get ghi chu --
create proc [GetGhiChu] @Param nvarchar(50)
as
SELECT Ghichu FROM tblHang WHERE MaHang = @Param
go