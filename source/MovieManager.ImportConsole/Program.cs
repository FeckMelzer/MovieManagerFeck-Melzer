using ConsoleTables;
using MovieManager.Core;
using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using MovieManager.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.ImportConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            InitData();
            AnalyzeData();

            Console.WriteLine();
            Console.Write("Beenden mit Eingabetaste ...");
            Console.ReadLine();
        }

        private static void InitData()
        {
            Console.WriteLine("***************************");
            Console.WriteLine("          Import");
            Console.WriteLine("***************************");

            Console.WriteLine("Import der Movies und Categories in die Datenbank");
            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Console.WriteLine("Datenbank löschen");
                //TODO: Datenbank löschen
                unitOfWork.DeleteDatabase();
                Console.WriteLine("Datenbank migrieren");
                //TODO: Migrationen anstoßen
                unitOfWork.MigrateDatabase();

                Console.WriteLine("Movies/Categories werden eingelesen");

                var movies = ImportController.ReadFromCsv().ToArray();
                if (movies.Length == 0)
                {
                    Console.WriteLine("!!! Es wurden keine Movies eingelesen");
                    return;
                }

                List<Category> categories = new List<Category>();
                //TODO: Kategorien ermitteln
                foreach (var movie in movies)
                {
                    if (!categories.Contains(movie.Category))
                    {
                        categories.Add(movie.Category);
                    }
                }

                Console.WriteLine($"  Es wurden {movies.Count()} Movies in {categories.Count()} Kategorien eingelesen!");

                //TODO: Movies und Kategorien in die Datenbank schreiben
                unitOfWork.MovieRepository.AddMovies(movies);
                unitOfWork.CategoryRepository.AddCategories(categories);
                unitOfWork.Save();

                Console.WriteLine();
            }
        }

        private static void AnalyzeData()
        {
            using (IUnitOfWork uow = new UnitOfWork())
            {
                Console.WriteLine("***************************");
                Console.WriteLine("        Statistik");
                Console.WriteLine("***************************");


                // Längster Film: Bei mehreren gleichlangen Filmen, soll jener angezeigt werden, dessen Titel im Alphabet am weitesten vorne steht.
                // Die Dauer des längsten Films soll in Stunden und Minuten angezeigt werden!
                //TODO
                Movie longestFilm = uow.MovieRepository.getLongestFilm();


                Console.WriteLine($"Längster Film: {longestFilm.Title}, {GetDurationAsString(longestFilm.Duration, false)}");


                // Top Kategorie:
                //   - Jene Kategorie mit den meisten Filmen.
                //TODO
                Category mostFilms = uow.CategoryRepository.getCategoryWithMostFilms();
                Console.WriteLine($"Kategorie mit den meisten Filmen: '{mostFilms.CategoryName}'; Filme: {mostFilms.Movies.Count}");


                // Jahr der Kategorie "Action":
                //  - In welchem Jahr wurden die meisten Action-Filme veröffentlicht?
                //TODO
                var yearOfMostActionFilms = uow.MovieRepository.getYearWithMostActionFilms();
                Console.WriteLine($"Jahr der Action-Filme: {yearOfMostActionFilms.ToString()}");

                // Kategorie Auswertung (Teil 1):
                //   - Eine Liste in der je Kategorie die Anzahl der Filme und deren Gesamtdauer dargestellt wird.
                //   - Sortiert nach dem Namen der Kategorie (aufsteigend).
                //   - Die Gesamtdauer soll in Stunden und Minuten angezeigt werden!
                //TODO
                Console.WriteLine();
                Console.WriteLine("Kategorie Auswertung");

                var CategoryStatistics1 = uow.CategoryRepository.CategoryStatistics1();
                var Table1 = new ConsoleTable("Kategorie", "Anzahl", "Gesamtdauer");

                foreach (var item in CategoryStatistics1)
                {
                    Table1.AddRow(item.Category.CategoryName, item.MoviesCount, GetDurationAsString(item.CompleteMinutes, false));
                }

                Table1.Write();
                Console.WriteLine();
                // Kategorie Auswertung (Teil 2):
                //   - Alle Kategorien und die durchschnittliche Dauer der Filme der Kategorie
                //   - Absteigend sortiert nach der durchschnittlichen Dauer der Filme.
                //     Bei gleicher Dauer dann nach dem Namen der Kategorie aufsteigend sortieren.
                //   - Die Gesamtdauer soll in Stunden, Minuten und Sekunden angezeigt werden!
                //TODO
                var CategoryStatistics2 = uow.CategoryRepository.CategoryStatistics2();
                var Table2 = new ConsoleTable("Kategorie", "durchschn. Gesamtdauer");

                foreach(var item in CategoryStatistics2)
                {
                    Table2.AddRow(item.category.CategoryName, GetDurationAsString(item.averageMovieDuration, true));
                }
                Table2.Write();

            }
        }

        private static string GetDurationAsString(double minutes, bool withSeconds)
        {
            double durationMinutes = (minutes % 60);
            string durationHours = ((minutes - durationMinutes) / 60).ToString();

            if (durationHours.Length == 1) durationHours = $"0{durationHours}";
            
            
            if(!withSeconds)return $"{durationHours} h {durationMinutes} min";

            return $"{durationHours} h {Math.Truncate(durationMinutes)} min {Math.Truncate((durationMinutes - Math.Truncate(durationMinutes)) * 60)} sec";


        }
       
    }
}
