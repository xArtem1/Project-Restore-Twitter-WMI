using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikolTwitter.Migrations
{
    /// <inheritdoc />
    public partial class AddProfitSumTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProfitSum",
                table: "BikolSubs",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfitSum",
                table: "BikolSubs");
        }
    }
}
