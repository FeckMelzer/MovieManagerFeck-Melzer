using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Core.DataTransferObjects
{
    class CategoryDTO2
    {
        public Category category { get; set; }
        public string averageMovieDuration { get; set; }
    }
}
