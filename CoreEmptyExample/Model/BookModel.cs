using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreEmptyExample.Model
{
    public class BookModel
    {
        
        [Key]
        public Guid Id { get; set; }
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
        
        [NotMapped]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
    }
}
