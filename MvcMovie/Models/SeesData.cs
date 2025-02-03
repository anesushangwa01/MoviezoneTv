using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }
            context.Movie.AddRange(
                new Movie
                {
                    ImageUrl = "https://cdn.pixabay.com/photo/2020/09/11/00/06/spiderman-5561671_1280.jpg",
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                       Rating = "R",
                    Price = 7.99M
                },
                new Movie
                {
                     ImageUrl = "https://cdn.pixabay.com/photo/2020/09/11/00/06/spiderman-5561671_1280.jpg",
                    Title = "Ghostbusters ",
                   
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                       Rating = "R",
                    Price = 8.99M
                },
                new Movie
                {
                     ImageUrl = "https://cdn.pixabay.com/photo/2020/09/11/00/06/spiderman-5561671_1280.jpg",
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                       Rating = "R",
                    Price = 9.99M
                },
                new Movie
                {
                     ImageUrl = "https://cdn.pixabay.com/photo/2020/09/11/00/06/spiderman-5561671_1280.jpg",
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Rating = "R",
                    Price = 3.99M
                }
            );
            context.SaveChanges();
        }
    }
}