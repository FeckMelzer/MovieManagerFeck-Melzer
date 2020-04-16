using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Core.DataTransferObjects
{
    internal class CategoryDTO1
    {
        public Category category { get; set; }
        public int MoviesCount { get; set; }
        public string HoursAndMinutesComplete { get; set; }

    }
}
