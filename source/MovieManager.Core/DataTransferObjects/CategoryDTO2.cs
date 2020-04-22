using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Core.DataTransferObjects
{
    public class CategoryDTO2
    {
        public Category category { get; set; }
        public double averageMovieDuration { get; set; }
    }
}
