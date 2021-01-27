using HC.DataAccess.Data.Repository;
using HC.Model;
using HC.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HC.DataAccess.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private  UnitOfWork _unitOfWork; 

        public DBInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = new UnitOfWork(_db); 
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
         
            if (_db.Roles.Any(r => r.Name == UserType.AdminRole)) return;

            //Create Role 
            _roleManager.CreateAsync(new IdentityRole(UserType.AdminRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(UserType.SupplierRole)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(UserType.CustomerRole)).GetAwaiter().GetResult();
            _unitOfWork = new UnitOfWork(_db);

            //Create Admin user
            int n = 5;
            CreateUsers(1,5, 5 );
            CreateCategoryandUnit();
            CreateProduct(4);
            CreateProduct(10, true); 


        }

        

        private void CreateProduct(int numberProduct, bool isPending= false) {

            IEnumerable < Category > listCategory= _unitOfWork.Category.GetAll();
            IEnumerable<AppUserView> BestSuppliers = _unitOfWork.SP.ReturnList<AppUserView>("SelectTopSeller");

            for (int i = 1; i <= numberProduct; i++)
            {
                foreach (Category cat in listCategory)
                {
                    IEnumerable<Unit> unitList = _unitOfWork.Unit.GetAll(filter: u=> u.CategoryId == cat.Id); 
                    Random random = new Random();
                    int index = random.Next(0, BestSuppliers.Count()-1);
                    AppUserView selectedUser = BestSuppliers.ToArray()[index];

                    string productName = "Great" + cat.Name + "No " + i.ToString(); 

                    Product product = new Product() {
                        Name = productName, 
                        Description = productName + " is one of the most famous food in Vietnam.",
                        Price = 10 * i / 0.5 + (15 * cat.Id * 0.3), 
                        UnitId = unitList.ToArray()[0].Id, 
                        CategoryId = cat.Id, 
                        CreateDate = DateTime.Now, 
                        AvgRating = i*2 , 
                        Status = isPending ? ProductStatus.Pending : ProductStatus.Active, 
                        UserId = selectedUser.Id

                    };

                    _unitOfWork.Product.Add(product);

                    PricingHistory pricing = new PricingHistory()
                    {
                        Product = product,
                        OPrice = 0,
                        NPrice = product.Price,
                        UpdateDate = product.CreateDate,
                        UserId = product.UserId
                    };
                    _unitOfWork.PricingHistory.Add(pricing);

                    _unitOfWork.Save();
                }
            
            }
           


        }

        private void CreateCategoryandUnit()
        {
            Category cateogry1 = new Category()
            {
                Name = "Dessert",
                Description = "Including sweet dessert, cake, boba tea",
                DisplayOrder = 1
            };
            _unitOfWork.Category.Add(cateogry1);

            Unit unit1 = new Unit()
            {
                Name = "Small cup",
                Description = "Cup about 20fl oz",
                Category = cateogry1
               
            };
            _unitOfWork.Unit.Add(unit1);
            Unit unit2 = new Unit()
            {
                Name = "Medium Cup",
                Description = "Cup about 30fl oz",
                Category = cateogry1

            };
            _unitOfWork.Unit.Add(unit2);



            Category cateogry2 = new Category()
            {
                Name = "Main Dishes",
                Description = "Including soup , noodle, fried things ...",
                DisplayOrder = 2
            };
            _unitOfWork.Category.Add(cateogry2);
            Unit unit3 = new Unit()
            {
                Name = "Small Dish",
                Description = "Cup about 10 inc",
                Category = cateogry2

            };
            _unitOfWork.Unit.Add(unit3);
            Unit unit4 = new Unit()
            {
                Name = "Small Bowl",
                Description = "Cup about 50oz",
                Category = cateogry2

            };
            _unitOfWork.Unit.Add(unit4);



            Category cateogry3 = new Category()
            {
                Name = "Services",
                Description = "Including services such as cleanpup , cooking, babysister",
                DisplayOrder = 3
            };
            _unitOfWork.Category.Add(cateogry3);
            Unit unit5 = new Unit()
            {
                Name = "Hour",
                Description = "60min without break",
                Category = cateogry3

            };
            _unitOfWork.Unit.Add(unit5);
            Unit unit6 = new Unit()
            {
                Name = "Day",
                Description = "From 8AM to 5PM ",
                Category = cateogry3

            };
            _unitOfWork.Unit.Add(unit6);

            _unitOfWork.Save();
        }




        //
        private void CreateUsers(int numberAdmin, int numberSupplier, int numberCustomer)
        {
            for (int i = 1; i <= numberAdmin; i++ )
            {
                ApplicationUser userInfo = new ApplicationUser()
                {
                    UserName = "Admin" + i.ToString() + "@gmail.com",
                    Email = "Admin" + i.ToString() + "@gmail.com",
                    EmailConfirmed = false,
                    Name = "Home Cook Admin " + i.ToString()
                };
                CreateUser(userInfo);
            }

            for (int i = 1; i <= numberSupplier; i++)
            {
                ApplicationUser userInfo = new ApplicationUser()
                {
                    UserName = "Supplier" + i.ToString() + "@gmail.com",
                    Email = "Supplier" + i.ToString() + "@gmail.com",
                    EmailConfirmed = false,
                    Name = "Thao Phan " + i.ToString()
                };
                CreateUser(userInfo, false, true, false);
            }

            for (int i = 1; i <= numberCustomer; i++)
            {
                ApplicationUser userInfo = new ApplicationUser()
                {
                    UserName = "Customer" + i.ToString() + "@gmail.com",
                    Email = "Customer" + i.ToString() + "@gmail.com",
                    EmailConfirmed = false,
                    Name = "Customer " + i.ToString()
                };
                CreateUser(userInfo);
            }


        }

        private void CreateUser(ApplicationUser userInfo, bool isAdmin = true, bool isSupplier =false, bool isCustomer = true) 
        {

            _ = _userManager.CreateAsync(userInfo, "P@ssword123").GetAwaiter().GetResult();

            ApplicationUser user = _db.AppUser.Where(u => u.Email == userInfo.Email).FirstOrDefault();

            if (isAdmin)    _userManager.AddToRoleAsync(user, UserType.AdminRole).GetAwaiter().GetResult();
            if (isSupplier) _userManager.AddToRoleAsync(user, UserType.SupplierRole).GetAwaiter().GetResult();
            if (isCustomer) _userManager.AddToRoleAsync(user, UserType.CustomerRole).GetAwaiter().GetResult();

        }

        
    }
}
