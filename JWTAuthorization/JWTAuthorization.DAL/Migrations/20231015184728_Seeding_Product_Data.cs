using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthorization.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Seeding_Product_Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "Description", "CategoryId" },
                values: new object[,]
                {
                    {1,"Watch" , "Smart Watch" , 6},
                    {2,"Shoes" , "Nike Shoes" , 4},
                    {3,"Bag" , "Leather Bag" , 5},
                    {4,"T-Shirt" , "Cotton T-Shirt" , 3},
                    {5,"Mobile" , "Samsung Mobile" , 2},
                    {6,"Lipstick" , "Lipstick" , 1},
                    {7,"Ring" , "Gold Ring" , 7}
                }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Products]");
        }
    }
}
