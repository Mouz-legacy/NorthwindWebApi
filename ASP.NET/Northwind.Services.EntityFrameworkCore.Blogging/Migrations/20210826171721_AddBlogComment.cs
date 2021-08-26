using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Services.EntityFrameworkCore.Blogging.Migrations
{
    public partial class AddBlogComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogArticleProduct",
                table: "BlogArticleProduct");

            migrationBuilder.RenameTable(
                name: "BlogArticleProduct",
                newName: "ArticleProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleProducts",
                table: "ArticleProducts",
                column: "blog_article_product_id");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    blog_comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    article_id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    text = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.blog_comment_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleProducts",
                table: "ArticleProducts");

            migrationBuilder.RenameTable(
                name: "ArticleProducts",
                newName: "BlogArticleProduct");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogArticleProduct",
                table: "BlogArticleProduct",
                column: "blog_article_product_id");
        }
    }
}
