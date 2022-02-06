using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using eTickets.Data.Static;

namespace eTickets.Controllers
{

	[Authorize(Roles = UserRoles.Admin)]
	public class MoviesController : Controller
	{
		private readonly IMoviesService _service;
		private readonly ApplicationDbContext _context;

		public MoviesController(IMoviesService service, ApplicationDbContext context)
		{
			_service = service;
			_context = context;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var movies = await _service.GetAllAsync(n => n.Cinema);
			return View(movies);
		}

		[AllowAnonymous]
		public async Task<IActionResult> Filter(string searchString)
		{
			var movies = await _service.GetAllAsync(n => n.Cinema);

			if (!string.IsNullOrEmpty(searchString))
			{
				var results = movies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) 
								|| n.Description.ToLower().Contains(searchString.ToLower())).ToList();
				return View("Index", results);
			}

			return View("Index", movies);
		}

		// GET: Movies/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var movieDetail = await _service.GetMovieByIdAsync(id);
			return View(movieDetail);
		}

		// GET: Movies/Create
		public async Task<IActionResult> Create()
		{
			var actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync();
			var cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync();
			var producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync();

			ViewBag.Actors = new SelectList(actors, "Id", "FullName");
			ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
			ViewBag.Producers = new SelectList(producers, "Id", "FullName");

			return View();
		}
		//public async Task<IActionResult> Create(int junk)
		//{
		//	var data = await _service.GetNewMovieDropDownsValues();
		//	ViewBag.Cinemas = new SelectList(data.Cinemas, "Id", "Name");
		//	ViewBag.Producers = new SelectList(data.Producers, "Id", "FullName");
		//	ViewBag.Actors = new SelectList(data.Actors, "Id", "FullName");

		//	return View();
		//}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(VM_NewMovie movie)
		{
			if (!ModelState.IsValid)
			{
				var actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync();
				var cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync();
				var producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync();

				ViewBag.Actors = new SelectList(actors, "Id", "FullName");
				ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(producers, "Id", "FullName");

				return View(movie);
			}

			await _service.AddNewMovieAsync(movie);

			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		// GET: Movies/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var movieDetails = await _service.GetMovieByIdAsync(id);
			if (movieDetails == null) { return View("NotFound"); }

			var response = new VM_NewMovie()
			{
				Id = movieDetails.Id,
				Name = movieDetails.Name,
				Description = movieDetails.Description,
				Price = movieDetails.Price,
				StartDate = movieDetails.StartDate,
				EndDate = movieDetails.EndDate,
				ImageURL = movieDetails.ImageURL,
				MovieCategory = movieDetails.MovieCategory,
				CinemaId = movieDetails.CinemaId,
				ProducerId = movieDetails.ProducerId,
				ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
			};

			var actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync();
			var cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync();
			var producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync();

			ViewBag.Actors = new SelectList(actors, "Id", "FullName");
			ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
			ViewBag.Producers = new SelectList(producers, "Id", "FullName");

			return View(response);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		// GET: Movies/Edit
		public async Task<IActionResult> Edit(int id, VM_NewMovie movie)
		{
			if (id != movie.Id) { return View("NotFound"); }

			if (!ModelState.IsValid)
			{
				var actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync();
				var cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync();
				var producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync();

				ViewBag.Actors = new SelectList(actors, "Id", "FullName");
				ViewBag.Cinemas = new SelectList(cinemas, "Id", "Name");
				ViewBag.Producers = new SelectList(producers, "Id", "FullName");

				return View(movie);
			}

			await _service.UpdateMovieAsync(movie);

			return RedirectToAction(nameof(Index));
		}

	}
}
