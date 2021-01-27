using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HC.Model
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Unit Name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Display(Name = "Unit Description")]
        [MaxLength(100)]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
