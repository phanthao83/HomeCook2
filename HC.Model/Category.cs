/* Author : Thi Xuan Thao, Phan
 *Linkedin  : https://www.linkedin.com/in/phan-thao-bb782bb5/
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HC.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set;  }

        [Required]
        [Display(Name = "Category Name")]
        [MaxLength (30)]
        public string Name { get; set; }

        [Display (Name = "Category Description")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Display (Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
