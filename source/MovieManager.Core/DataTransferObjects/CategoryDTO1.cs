using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Core.DataTransferObjects
{
    public class CategoryDTO1
    {
        public Category Category { get; set; }
        public int MoviesCount { get; set; }
        public int CompleteMinutes { get; set; }

    }
}
