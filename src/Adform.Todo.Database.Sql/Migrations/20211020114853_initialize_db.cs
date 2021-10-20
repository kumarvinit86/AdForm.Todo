using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adform.Todo.Database.Sql.Migrations
{
    public partial class initialize_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 10, 20, 17, 18, 51, 258, DateTimeKind.Local).AddTicks(4988), new DateTime(2021, 10, 20, 17, 18, 51, 259, DateTimeKind.Local).AddTicks(8346) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 10, 20, 17, 14, 37, 741, DateTimeKind.Local).AddTicks(7650), new DateTime(2021, 10, 20, 17, 14, 37, 742, DateTimeKind.Local).AddTicks(7340) });
        }
    }
}
