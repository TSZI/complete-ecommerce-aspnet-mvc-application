using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;

namespace eTickets.Data.Services
{
	public class ActorsService : EntityBaseRepository<Actor>, IActorsService
	{
		// See Generic Base for the following implementation
		
		//private readonly ApplicationDbContext _context;

		public ActorsService(ApplicationDbContext context) : base(context) { }

		//public async Task AddAsync(Actor actor)
		//{
		//	await _context.Actors.AddAsync(actor);
		//	await _context.SaveChangesAsync();
		//}

		//public async Task DeleteAsync(int id)
		//{
		//	var result = await _context.Actors.FirstOrDefaultAsync(n => n.Id == id);
		//	_context.Actors.Remove(result);
		//	await _context.SaveChangesAsync();
		//}

		//public async Task<Actor> UpdateAsync(int id, Actor newActor)
		//{
		//	_context.Update(newActor);
		//	await _context.SaveChangesAsync();
		//	return newActor;
		//}
	}
}
