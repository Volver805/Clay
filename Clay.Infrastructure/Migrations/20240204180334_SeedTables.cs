using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clay.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var passwordHasher = new PasswordHasher<Object>();

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Username", "HashPassword", "Name" },
                values: new object[] { 1, "Ahmed", passwordHasher.HashPassword(null, "moonlightsword"), "Ahmed" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Username", "HashPassword", "Name" },
                values: new object[] { 2, "Nikita", passwordHasher.HashPassword(null, "billiondollarcode"), "Nikita" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Username", "HashPassword", "Name" },
                values: new object[] { 3, "Nadiia", passwordHasher.HashPassword(null, "sett5000"), "Nadiia" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Employee" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "Name" },
                values: new object[] { 2, "Admin" });

            migrationBuilder.InsertData(
                table: "Locks",
                columns: new[] { "ID", "SerialNumber", "Label", "IsLocked", "ShouldLockAfter" },
                values: new object[] { 1, "CL0001", "Entrance Door", false, null });

            migrationBuilder.InsertData(
                table: "Locks",
                columns: new[] { "ID", "SerialNumber", "Label", "IsLocked", "ShouldLockAfter" },
                values: new object[] { 2, "CL0002", "Storage Room", true, 300 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "ID", "UserId", "RoleId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "ID", "UserId", "RoleId" },
                values: new object[] { 2, 2, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "ID", "UserId", "RoleId" },
                values: new object[] { 3, 3, 1 });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "ID", "UserId", "RoleId" },
                values: new object[] { 4, 3, 2 });

            migrationBuilder.InsertData(
                table: "LockRoles",
                columns: new[] { "ID", "LockId", "RoleId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "LockRoles",
                columns: new[] { "ID", "LockId", "RoleId" },
                values: new object[] { 2, 2, 2 });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "LockRoles",
                keyColumns: new[] { "LockId", "RoleId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "LockRoles",
                keyColumns: new[] { "LockId", "RoleId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locks",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locks",
                keyColumn: "ID",
                keyValue: 2);

        }
    }
}
