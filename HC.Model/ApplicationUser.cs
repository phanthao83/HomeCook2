/* Author : Thi Xuan Thao, Phan
 *Linkedin  : https://www.linkedin.com/in/phan-thao-bb782bb5/
 */
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HC.Model
{
    public static class UserType
    {

        public const string AdminRole = "A";
        public const string SupplierRole = "S";
        public const string CustomerRole = "C";
    }


    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        [Display (Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address 1")]
        [MaxLength(50)]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        [MaxLength(50)]
        public string Address2 { get; set;  }

        [Display(Name = "City")]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "State")]
        [MaxLength(2)]
        public string State { get; set; }

        [Display(Name = "Country")]
        [MaxLength(50)]
        public string Country { get; set; }



        [Display(Name = "Zip Code")]
        [MaxLength(5)]
        public string PostCode { get; set; }

        [Display(Name = "Avartar")]
        [MaxLength(100)] 
        public string AvartarUrl { get; set; }

        
       
    }
}
