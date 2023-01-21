using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikolTwitter.Migrations
{
    /// <inheritdoc />
    public partial class AddBikolSubTweetsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BikolSubTweets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: true),
                    FullText = table.Column<string>(type: "TEXT", nullable: true),
                    Prefix = table.Column<string>(type: "TEXT", nullable: true),
                    Suffix = table.Column<string>(type: "TEXT", nullable: true),
                    FavoriteCount = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikolSubTweets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikolSubTweets");
        }
    }
}
