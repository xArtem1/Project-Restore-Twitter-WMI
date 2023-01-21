using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikolTwitter.Migrations
{
    /// <inheritdoc />
    public partial class AddScreenName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByScreenName",
                table: "BikolSubTweets",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByScreenName",
                table: "BikolSubTweets");
        }
    }
}
