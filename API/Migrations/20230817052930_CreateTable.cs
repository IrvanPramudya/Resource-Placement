using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4615), new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4616) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("5fb9adc0-7d08-45d4-cd66-08db9c7a678f"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4621), new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4621) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4604), new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4613) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4618), new DateTime(2023, 8, 17, 12, 29, 30, 664, DateTimeKind.Local).AddTicks(4618) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2294), new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2294) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("5fb9adc0-7d08-45d4-cd66-08db9c7a678f"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2300), new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2300) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2279), new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2289) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2296), new DateTime(2023, 8, 17, 11, 53, 10, 617, DateTimeKind.Local).AddTicks(2297) });
        }
    }
}
