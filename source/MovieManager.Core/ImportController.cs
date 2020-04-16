using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Utils;

namespace MovieManager.Core
{
    public class ImportController
    {
        const string Filename = "Movies.csv";

        /// <summary>
        /// Liefert die Movies mit den dazugehörigen Kategorien
        /// </summary>
        public static IEnumerable<Movie> ReadFromCsv() 
        {
            
            string[] lines = File.ReadAllLines(MyFile.GetFullNameInApplicationTree(Filename), Encoding.UTF8);

            IDictionary<string, Category> categories = new Dictionary<string, Category>();
            List<Movie> movies = new List<Movie>();

            for(int i = 1;  i<lines.Length; i++)
            {
                string[] splitted = lines[i].Split(";");

                string title = splitted[0];
                int year = Convert.ToInt32(splitted[1]);
                string category = splitted[2];
                int duration = Convert.ToInt32(splitted[3]);

                Category newCat;

                Movie movie = new Movie
                {
                    Title = title,
                    Duration = duration,
                    Year = year
                };

                Category cat;
                if(categories.TryGetValue(category, out cat))
                {
                 
                    movie.Category = cat;
                }
                else
                {
                    newCat = new Category
                    {
                        CategoryName = category,
                    };
                    categories.Add(category, newCat);
                    
                    movie.Category = newCat;
                }
                movies.Add(movie);
            }
            return movies.ToArray();
        }

    }
}
