using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2596), new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2596) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("5fb9adc0-7d08-45d4-cd66-08db9c7a678f"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2606), new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2607) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2577), new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2591) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2603), new DateTime(2023, 8, 17, 15, 21, 57, 962, DateTimeKind.Local).AddTicks(2604) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(858), new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(859) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("5fb9adc0-7d08-45d4-cd66-08db9c7a678f"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(866), new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(866) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(840), new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(853) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(862), new DateTime(2023, 8, 17, 13, 43, 10, 753, DateTimeKind.Local).AddTicks(863) });
        }
    }
}
