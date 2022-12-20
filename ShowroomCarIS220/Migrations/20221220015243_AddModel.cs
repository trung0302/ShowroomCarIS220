using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShowroomCarIS220.Migrations
{
    /// <inheritdoc />
    public partial class AddModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    macar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thuonghieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dongco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    socho = table.Column<int>(type: "int", nullable: false),
                    kichthuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nguongoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    vantoctoida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dungtich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tieuhaonhienlieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    congsuatcucdai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mausac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gia = table.Column<long>(type: "bigint", nullable: false),
                    hinhanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mota = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    namsanxuat = table.Column<int>(type: "int", nullable: false),
                    soluong = table.Column<int>(type: "int", nullable: false),
                    advice = table.Column<bool>(type: "bit", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.id);
                });

<<<<<<< HEAD:ShowroomCarIS220/Migrations/20221218165424_AddModel.cs
           

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    manv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diachi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaysinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    chucvu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gioitinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sodienthoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cccd = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    confirmpassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                });

=======
>>>>>>> 66e0f1ba5dab27cf0f3f821591b9708209f16703:ShowroomCarIS220/Migrations/20221220015243_AddModel.cs
            migrationBuilder.CreateTable(
                name: "Form",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Form", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    mahd = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    makh = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    manv = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tenkh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngayhd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tinhtrang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    trigia = table.Column<long>(type: "bigint", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => new { x.mahd, x.makh, x.manv });
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mauser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gioitinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ngaysinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    diachi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chucvu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cccd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    verifyToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "CTHD",
                columns: table => new
                {
                    mahd = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    macar = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    soluong = table.Column<int>(type: "int", nullable: false),
                    gia = table.Column<long>(type: "bigint", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoaDonmahd = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HoaDonmakh = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HoaDonmanv = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTHD", x => new { x.macar, x.mahd });
                    table.ForeignKey(
                        name: "FK_CTHD_HoaDon_HoaDonmahd_HoaDonmakh_HoaDonmanv",
                        columns: x => new { x.HoaDonmahd, x.HoaDonmakh, x.HoaDonmanv },
                        principalTable: "HoaDon",
                        principalColumns: new[] { "mahd", "makh", "manv" });
                });

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.id);
                    table.ForeignKey(
                        name: "FK_Token_User_userId",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTHD_HoaDonmahd_HoaDonmakh_HoaDonmanv",
                table: "CTHD",
                columns: new[] { "HoaDonmahd", "HoaDonmakh", "HoaDonmanv" });

            migrationBuilder.CreateIndex(
                name: "IX_Token_userId",
                table: "Token",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_User_email",
                table: "User",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "CTHD");

            migrationBuilder.DropTable(
                name: "Form");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
