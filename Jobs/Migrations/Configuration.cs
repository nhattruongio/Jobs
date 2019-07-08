namespace Jobs.Migrations
{
    using global::Jobs.Common;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<global::Jobs.Models.JobDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(global::Jobs.Models.JobDatabaseContext context)
        {
            context.ADMINs.AddOrUpdate(
                new Models.ADMIN() { Ten = "Nguyễn Trọng Tín", TaiKhoan = "tisnhehe", Matkhau = StringHash.crypto("12345678") }
                );

            context.LOAITKs.AddOrUpdate(
                new Models.LOAITK() { LoaiTK = "Người xin việc" },
                new Models.LOAITK() { LoaiTK = "Nhà tuyển dụng" }
                );
        }
    }
}
