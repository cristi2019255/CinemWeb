using Movies.Data.Interfaces;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Data.Repository
{
    public class ActorRepository : IAllActors
    {
        private readonly ApplicationDbContext _appDbContext;

        public ActorRepository(ApplicationDbContext applicationDbContext)
        {
            _appDbContext = applicationDbContext;
        }

        public IEnumerable<Actor> AllActors {
            get => _appDbContext.Actors; }

        public Actor GetActorsById(int actorId)
        {
            return _appDbContext.Actors.FirstOrDefault(item => item.id == actorId);
        }
    }
}
