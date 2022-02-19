using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class Product
    {
        
        [Key]
        public int ProductId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Product Name")]
        public string ProductName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Rate")]
        public decimal Rate { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Description")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Category Name")]
        public string CategoryName { get; set; }

    }
}