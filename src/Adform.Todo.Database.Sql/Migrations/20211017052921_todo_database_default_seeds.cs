using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Adform.Todo.Database.Sql.Migrations
{
    public partial class todo_database_default_seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Labels",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "None" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 1, "default", "MZi3g4TK09g=" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 2, "admin", "AKDS+52ehoM=" });

            migrationBuilder.InsertData(
                table: "ToDoLists",
                columns: new[] { "Id", "CreatedDate", "LabelId", "Name", "UpdatedDate", "UserId" },
                values: new object[] { 1, new DateTime(2021, 10, 17, 10, 59, 21, 351, DateTimeKind.Local).AddTicks(5227), 1, "None", new DateTime(2021, 10, 17, 10, 59, 21, 352, DateTimeKind.Local).AddTicks(2267), 1 });

            migrationBuilder.InsertData(
                table: "ToDoItems",
                columns: new[] { "Id", "CreatedDate", "IsComplete", "LabelId", "Name", "ToDoListId", "UpdatedDate", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "None", 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToDoItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Labels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
