using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;

namespace eTickets.Controllers
{
	public class MoviesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public MoviesController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var movies = await _context.Movies.Include(c => c.Cinema).OrderBy(m => m.Name).ToListAsync();
			return View(movies);
		}
	}
}
