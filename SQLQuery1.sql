use QLBANSACH

select v.Vaitro,t.TenTG
from VIETSACH v ,TACGIA t
where v.Masach=1 and v.MaTG=t.MaTG
select * from SACH
select * from TACGIA

insert [dbo].[SACH] ([Masach], [Tensach], [Donvitinh], [Dongia], [Mota], [Hinhminhhoa], [MaCD], [MaNXB], [Ngaycapnhat], [Soluongban], [solanxem], [moi])
values(25, N'My book', N'Cuốn', 17800.0000, N'Chào mừng', N'TH001.jpg', 5, 5, CAST(N'2004-05-15T00:00:00' AS SmallDateTime), 8, 0, NULL)
INSERT [dbo].[TACGIA]([MaTG], [TenTG], [DiachiTG], [DienthoaiTG])
VALUES (17, N'Phạm', N'197 Trần Hưng Đạo', N'98877668')
INSERT [dbo].[TACGIA]([MaTG], [TenTG], [DiachiTG], [DienthoaiTG])
VALUES (18, N'Phạm An', N'197', N'98877668')
INSERT [dbo].[VIETSACH] ([MaTG], [Masach], [Vaitro])
VALUES (17, 25, N'Tác giả 1')
INSERT [dbo].[VIETSACH] ([MaTG], [Masach], [Vaitro])
VALUES (18, 25, N'Tác giả 2')

create procedure sp_ThemSach @Masach int,@tensach nvarchar(100),@Dongia money,@Mota ntext,@maTG1 int,@maTG2 int
as
if not exists (select * from TACGIA where MaTG=@maTG1 or MaTG=@maTG2)
	raiserror ('tac gia nay khong ton tai',16,1)
	else if exists (select * from SACH where Masach=@Masach)
	raiserror ('Trung khoa chinh, sach nay da ton tai',16,1)
	else if exists (select * from VIETSACH where @maTG1=@maTG2)
	raiserror ('2 tac gia ko dc trung',16,1)
	else
	begin
		
	end