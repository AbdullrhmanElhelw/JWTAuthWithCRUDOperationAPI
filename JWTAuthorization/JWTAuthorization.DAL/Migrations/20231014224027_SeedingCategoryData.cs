using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthorization.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedingCategoryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "InStock" },
                values: new object[,]
                {
                    {1,"Beauty",12 },
                    {2,"Electornics",21},
                    {3,"Clothes",12 } ,
                    {4,"Shoes",12 },
                    {5,"Bags",12 },
                    {6,"Watches",12 },
                    {7,"Jewelery",12 }
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM  [Categories] ");
        }
    }
}
