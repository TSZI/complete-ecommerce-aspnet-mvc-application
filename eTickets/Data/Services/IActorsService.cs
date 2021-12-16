using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;

namespace eTickets.Data.Services
{
	public interface IActorsService : IEntityBaseRepository<Actor>
	{
		// See Generic Base for the following implementation

		//Task<IEnumerable<Actor>> GetAllAsync();

		//Task<Actor> GetByIdAsync(int id);

		//Task AddAsync(Actor actor);

		//Task <Actor> UpdateAsync(int id, Actor newActor);

		//Task DeleteAsync(int id);
	}
}
