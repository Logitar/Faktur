using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logitar.Faktur.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class CreateStoreTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BannerId = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressStreet = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AddressLocality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressRegion = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    AddressPostalCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AddressCountry = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    AddressFormatted = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PhoneCountryCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhoneExtension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PhoneE164Formatted = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    AggregateId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_Stores_Banners_BannerId",
                        column: x => x.BannerId,
                        principalTable: "Banners",
                        principalColumn: "BannerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AddressFormatted",
                table: "Stores",
                column: "AddressFormatted");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AggregateId",
                table: "Stores",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stores_BannerId",
                table: "Stores",
                column: "BannerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CreatedBy",
                table: "Stores",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_CreatedOn",
                table: "Stores",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_DisplayName",
                table: "Stores",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_Number",
                table: "Stores",
                column: "Number");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_PhoneE164Formatted",
                table: "Stores",
                column: "PhoneE164Formatted");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UpdatedBy",
                table: "Stores",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UpdatedOn",
                table: "Stores",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_Version",
                table: "Stores",
                column: "Version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
