using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class addnullableIsacceptedonHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_accepted",
                table: "tb_tr_histories",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9647), new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9647) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("5fb9adc0-7d08-45d4-cd66-08db9c7a678f"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9653), new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9654) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9627), new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9638) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9650), new DateTime(2023, 9, 4, 10, 38, 26, 849, DateTimeKind.Local).AddTicks(9651) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "is_accepted",
                table: "tb_tr_histories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("4ec90656-e89c-4871-d9e5-08db8a7d0f37"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9977), new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9977) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("5fb9adc0-7d08-45d4-cd66-08db9c7a678f"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9983), new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9983) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("ae259a90-e2e8-442f-ce18-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9954), new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9971) });

            migrationBuilder.UpdateData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("c0689b0a-5c87-46f1-ce19-08db91a71ab9"),
                columns: new[] { "created_date", "modified_date" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9980), new DateTime(2023, 9, 4, 10, 10, 34, 262, DateTimeKind.Local).AddTicks(9980) });
        }
    }
}
