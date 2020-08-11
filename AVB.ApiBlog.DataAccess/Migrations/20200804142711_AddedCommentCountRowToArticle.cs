using Microsoft.EntityFrameworkCore.Migrations;

namespace AVB.ApiBlog.DataAccess.Migrations
{
    public partial class AddedCommentCountRowToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentCount",
                table: "Articles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentCount",
                table: "Articles");
        }
    }
}
