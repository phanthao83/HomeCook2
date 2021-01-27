using Microsoft.EntityFrameworkCore.Migrations;

namespace HC.DataAccess.Migrations
{
    public partial class SP_SelectAllSupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp_SelectAllSeller = @"
                CREATE PROCEDURE [dbo].[SelectAllSeller] 
                AS
                BEGIN
                SELECT [AspNetUsers].ID , PhoneNumber
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
            migrationBuilder.Sql(sp_SelectAllSeller);


            var sp_SelectAllCustomer = @"
                CREATE PROCEDURE [dbo].[SelectAllCustomer] 
                AS
                BEGIN
                   SELECT [AspNetUsers].ID , PhoneNumber
	                    ,[AvartarUrl], [City] , [AspNetUsers].[Name] ,[State],
	                    count([Order].Id) as ProductQuanity
	  
	                    FROM [AspNetUsers]  left join [Order]  on [Order].BuyerId = [AspNetUsers].Id 
				  
	                    where [AspNetUsers].Id  in (Select AspNetUserRoles.UserId
	                    from AspNetRoles, AspNetUserRoles
	                    where AspNetUserRoles.RoleId = AspNetRoles.Id and Name ='S')
	                    group by [AspNetUsers].ID , PhoneNumber
	                    ,[AvartarUrl], [City] , [AspNetUsers].Name ,[State]
                END
                ";
            migrationBuilder.Sql(sp_SelectAllCustomer);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
