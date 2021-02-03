/* Author : Thi Xuan Thao, Phan
 *Linkedin  : https://www.linkedin.com/in/phan-thao-bb782bb5/
 */
using HC.DataAccess.Data.Repository.IRepository;
using HC.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace HC.DataAccess.Data.Repository
{
    public class ProductReviewRepository : Respository<ProductReview>, IProductReviewRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(ProductReview review)
        {
            var selectedReview = _db.ProductReview.FirstOrDefault(s => s.Id == review.Id);
            selectedReview.ProductId = review.ProductId;
            selectedReview.Rate = review.Rate;
            selectedReview.UserId = review.UserId;

            selectedReview.Comment = review.Comment;

           // _db.SaveChanges();
        }
    }
}
