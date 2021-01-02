using Movies.Data.Interfaces;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Data.Repository
{
    public class MovieRepository : IMovie
    {
        private readonly ApplicationDbContext _appDbContext;

        public MovieRepository(ApplicationDbContext applicationDbContext) {
            _appDbContext = applicationDbContext;
        }

        public IEnumerable<Movie> AllMovies => _appDbContext.Movies;
    }
}
