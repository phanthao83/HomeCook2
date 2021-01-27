using Microsoft.EntityFrameworkCore.Migrations;

namespace HC.DataAccess.Migrations
{
    public partial class SP_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp_SelectActiveProduct = @"
                IF OBJECT_ID('SelectActiveProduct','P') IS NOT NULL DROP PROC SelectActiveProduct;
                GO
                CREATE PROCEDURE [dbo].[SelectActiveProduct] 
                @CategoryId int
                AS
                BEGIN
                Select TOP 4 Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                Category.Id as CategoryId, Category.Name as CategoryName, 
                  Unit.Id as UnitId , Unit.Name as UnitName, FileName as FileName ,  COALESCE( Sum( OrderDetail.Quantity),0) as PQuantity
                from  Product 
                inner join Category on Product.CategoryId = Category.Id and Category.Id = @CategoryId
                inner join Unit on Product.UnitId = Unit.Id
                left join OrderDetail on OrderDetail.ProductId = Product.Id
                left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                where Product.Status = 'A' 
                Group by Product.ID ,  Product.Name , Product.Price , Category.Id, Category.Name, 
                  Unit.Id , Unit.Name, FileName
                END    ";
            migrationBuilder.Sql(sp_SelectActiveProduct);


            var sp_SelectNewProduct = @"
                IF OBJECT_ID('SelectNewProduct','P') IS NOT NULL DROP PROC SelectNewProduct;
                GO
                CREATE PROCEDURE [dbo].[SelectNewProduct] 
                AS
                BEGIN
                Select TOP 4 Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                Category.Id as CategoryId, Category.Name as CategoryName, 
                  Unit.Id as UnitId , Unit.Name as UnitName, FileName ,  0 as PQuantity
                from  Product 
                inner join Category on Product.CategoryId = Category.Id
                inner join Unit on Product.UnitId = Unit.Id
                left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                where Product.Status = 'A' 
                Order by Product.CreateDate DESC

                END
            
                ";
            migrationBuilder.Sql(sp_SelectNewProduct);

            var sp_SelectTop4NewProduct = @"
                    IF OBJECT_ID('SelectTop4NewProduct','P') IS NOT NULL DROP PROC SelectTop4NewProduct;
                    GO

                    CREATE PROCEDURE [dbo].[SelectTop4NewProduct] 
                    AS
                    BEGIN
                    Select TOP 4 Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                    Category.Id as CategoryId, Category.Name as CategoryName, 
                      Unit.Id as UnitId , Unit.Name as UnitName,  FileName ,  0 as PQuantity
                    from  Product 
                    inner join Category on Product.CategoryId = Category.Id
                    inner join Unit on Product.UnitId = Unit.Id
                    left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                    where Product.Status = 'A' 
                    Order by Product.CreateDate DESC

                    END";
             migrationBuilder.Sql(sp_SelectTop4NewProduct);


            var sp_SelectTopProduct = @"
                IF OBJECT_ID('SelectTopProduct','P') IS NOT NULL DROP PROC SelectTopProduct;
                GO

                CREATE PROCEDURE [dbo].[SelectTopProduct] 
                AS
                BEGIN
                Select Product.ID as ID , Product.Name as Name , Product.Price as Price , 
                Category.Id as CategoryId, Category.Name as CategoryName, 
                  Unit.Id as UnitId , Unit.Name as UnitName, FileName  as FileName ,  COALESCE( Sum( OrderDetail.Quantity),0) as PQuantity
                from  Product 
                inner join Category on Product.CategoryId = Category.Id
                inner join Unit on Product.UnitId = Unit.Id
                left join OrderDetail on OrderDetail.ProductId = Product.Id
                left join ProductImage on ProductImage.ProductId = Product.ID and ProductImage.IsDefault = 1 
                where Product.Status = 'A'
                Group by Product.ID ,  Product.Name , Product.Price , Category.Id, Category.Name, 
                  Unit.Id , Unit.Name, FileName
                END";
            migrationBuilder.Sql(sp_SelectTopProduct);


            var sp_SelectTopSeller = @"
                IF OBJECT_ID('SelectTopSeller','P') IS NOT NULL DROP PROC SelectTopSeller;
                GO

                CREATE PROCEDURE [dbo].[SelectTopSeller] 
                AS
                BEGIN
                SELECT Top 10 [AspNetUsers].ID , PhoneNumber
                      ,[AvartarUrl], [City] , [AspNetUsers].[Name] ,[State],
	                   count(Product.Id) as ProductQuanity
	  
                FROM [AspNetUsers]  left join Product  on Product.UserId = [AspNetUsers].Id  and Product.Status = 'A'
                where [AspNetUsers].Id  in (Select AspNetUserRoles.UserId
                                 from AspNetRoles, AspNetUserRoles
				                 where AspNetUserRoles.RoleId = AspNetRoles.Id and Name ='S')
                group by [AspNetUsers].ID , PhoneNumber
                      ,[AvartarUrl], [City] , [AspNetUsers].Name ,[State]
                END
                ";
            migrationBuilder.Sql(sp_SelectTopSeller);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropSelectActiveProduct = "IF OBJECT_ID('SelectActiveProduct','P') IS NOT NULL DROP PROC SelectActiveProduct;";
            migrationBuilder.Sql(dropSelectActiveProduct);
           
            //SelectNewProduct
            var dropSelectNewProduct = "IF OBJECT_ID('SelectNewProduct','P') IS NOT NULL DROP PROC SelectNewProduct;";
            migrationBuilder.Sql(dropSelectNewProduct);

            //SelectTop4NewProduct
            var dropSelectTop4NewProduct = "IF OBJECT_ID('SelectTop4NewProduct','P') IS NOT NULL DROP PROC SelectTop4NewProduct;";
            migrationBuilder.Sql(dropSelectTop4NewProduct);

            //SelectTopProduct
            var dropSelectTopProduct = "IF OBJECT_ID('SelectTopProduct','P') IS NOT NULL DROP PROC SelectTopProduct;";
            migrationBuilder.Sql(dropSelectTopProduct);

            //SelectTopSeller
            var dropSelectTopSeller = "IF OBJECT_ID('SelectTopSeller','P') IS NOT NULL DROP PROC SelectTopSeller;";
            migrationBuilder.Sql(dropSelectTopSeller);



        }
    }
}
