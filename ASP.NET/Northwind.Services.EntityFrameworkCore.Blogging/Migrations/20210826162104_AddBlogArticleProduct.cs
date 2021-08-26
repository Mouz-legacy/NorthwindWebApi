using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Services.EntityFrameworkCore.Blogging.Migrations
{
    public partial class AddBlogArticleProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogArticleProduct",
                columns: table => new
                {
                    blog_article_product_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    article_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogArticleProduct", x => x.blog_article_product_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogArticleProduct");
        }
    }
}
