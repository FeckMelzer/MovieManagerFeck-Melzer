using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManager.Core.Entities
{
    public class Category : EntityObject
    {
        [Required]
        public String CategoryName { get; set; }
        
        [InverseProperty("Category")]
        public ICollection<Movie> Movies { get; set; }
    }
}
