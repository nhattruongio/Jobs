namespace Jobs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Jobs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ten = c.String(maxLength: 30),
                        TaiKhoan = c.String(maxLength: 30),
                        Matkhau = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CONGVIEC",
                c => new
                    {
                        MaCV = c.Int(nullable: false, identity: true),
                        SoLuong = c.String(maxLength: 10),
                        YeuCau = c.String(maxLength: 30),
                        DiaChi = c.String(maxLength: 300),
                        Luong = c.String(maxLength: 10),
                        MaKH = c.Int(),
                        MoTa = c.String(maxLength: 300),
                        DaNhan = c.Int(),
                        ChucVu = c.String(maxLength: 20),
                        Anh = c.String(),
                        NgayCapNhat = c.DateTime(),
                        TinhTrang = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.MaCV)
                .ForeignKey("dbo.KHACHHANG", t => t.MaKH)
                .Index(t => t.MaKH);
            
            CreateTable(
                "dbo.HOSO",
                c => new
                    {
                        MaHS = c.Int(nullable: false, identity: true),
                        MaKH = c.Int(),
                        MaCV = c.Int(),
                        TinhTrangXetTuyen = c.String(maxLength: 30),
                        NgayDang = c.DateTime(),
                    })
                .PrimaryKey(t => t.MaHS)
                .ForeignKey("dbo.CONGVIEC", t => t.MaCV)
                .ForeignKey("dbo.KHACHHANG", t => t.MaKH)
                .Index(t => t.MaKH)
                .Index(t => t.MaCV);
            
            CreateTable(
                "dbo.KHACHHANG",
                c => new
                    {
                        MaKH = c.Int(nullable: false, identity: true),
                        TenKH = c.String(maxLength: 30),
                        TaiKhoanKH = c.String(maxLength: 30),
                        MatKhauKH = c.String(maxLength: 300),
                        DienThoaiKH = c.String(maxLength: 10),
                        Email = c.String(nullable: false),
                        DiaChi = c.String(maxLength: 90),
                        GioiTinh = c.String(maxLength: 6),
                        LoaiTK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaKH);
            
            CreateTable(
                "dbo.LOAITK",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoaiTK = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HOSO", "MaKH", "dbo.KHACHHANG");
            DropForeignKey("dbo.CONGVIEC", "MaKH", "dbo.KHACHHANG");
            DropForeignKey("dbo.HOSO", "MaCV", "dbo.CONGVIEC");
            DropIndex("dbo.HOSO", new[] { "MaCV" });
            DropIndex("dbo.HOSO", new[] { "MaKH" });
            DropIndex("dbo.CONGVIEC", new[] { "MaKH" });
            DropTable("dbo.LOAITK");
            DropTable("dbo.KHACHHANG");
            DropTable("dbo.HOSO");
            DropTable("dbo.CONGVIEC");
            DropTable("dbo.ADMIN");
        }
    }
}
