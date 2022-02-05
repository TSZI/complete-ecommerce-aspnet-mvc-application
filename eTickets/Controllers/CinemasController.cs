using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using eTickets.Data.Static;

namespace eTickets.Controllers
{
	[Authorize(Roles = UserRoles.Admin)]
	public class CinemasController : Controller
	{
		private readonly ICinemasService _service;

		public CinemasController(ICinemasService service)
		{
			_service = service;
		}

		[AllowAnonymous]
		public async Task<IActionResult> Index()
		{
			var cinemas = await _service.GetAllAsync();
			return View(cinemas);
		}

		// GET: Cinemas/Create
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Logo, Name, Description")] Cinema cinema)
		{
			if (!ModelState.IsValid) return View(cinema);
			await _service.AddAsync(cinema);
			return RedirectToAction(nameof(Index));
		}

		// GET: Cinemas/Details/1
		[AllowAnonymous]
		public async Task<IActionResult> Details(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		// GET: Cinemas/Edit/1
		public async Task<IActionResult> Edit(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id, Logo, Name, Description")] Cinema cinema)
		{
			if (!ModelState.IsValid) return View(cinema);
			await _service.UpdateAsync(id, cinema);
			return RedirectToAction(nameof(Index));
		}

		// GET: Cinemas/Delete/1
		public async Task<IActionResult> Delete(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			return View(cinemaDetails);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirm(int id)
		{
			var cinemaDetails = await _service.GetByIdAsync(id);
			if (cinemaDetails == null) return View("NotFound");
			await _service.DeleteAsync(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
