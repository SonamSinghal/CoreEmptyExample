using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEmptyExample.Model
{
    public class BookModel
    {
 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime BookUpdatedOn { get; set; }
        public DateTime QuantityUpdatedOn { get; set; }
    }
}
