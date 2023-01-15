using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikolTwitter.Migrations
{
    /// <inheritdoc />
    public partial class AddBikolPostsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BikolPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TwitterId = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Profit = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    BikolSubId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikolPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BikolPosts_BikolSubs_BikolSubId",
                        column: x => x.BikolSubId,
                        principalTable: "BikolSubs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BikolPosts_BikolSubId",
                table: "BikolPosts",
                column: "BikolSubId");

            migrationBuilder.CreateIndex(
                name: "IX_BikolPosts_TwitterId",
                table: "BikolPosts",
                column: "TwitterId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikolPosts");
        }
    }
}
