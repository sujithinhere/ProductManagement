using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public partial class Product : IEntity
    {
        [Required(ErrorMessage = "Product Code is mandatory")]
        [StringLength(255, ErrorMessage = "Product Code can't be longer than 255 characters")]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Product Name is mandatory")]
        [StringLength(255, ErrorMessage = "Product Name can't be longer than 255 characters")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Url is mandatory")]
        public string ProductUrl { get; set; }
    }
}
