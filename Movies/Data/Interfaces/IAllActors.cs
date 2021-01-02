using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Data.Interfaces
{
    public interface IAllActors
    {
        IEnumerable<Actor> AllActors { get; }

        Actor GetActorsById(int actorId);
    }
}
