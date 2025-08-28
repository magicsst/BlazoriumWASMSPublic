using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazoriumWASMS.Shared.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        [Range(0.01, 1000)]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? SpecialTag { get; set; }

        public int CategoryId { get; set; }

        // Συνήθως δεν χρειάζεται να γράψω το [foreignKey] γιατι το καταλαβαίνει μόνο του,
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new Category();

        public string? ImageUrl { get; set; }
    }
}
