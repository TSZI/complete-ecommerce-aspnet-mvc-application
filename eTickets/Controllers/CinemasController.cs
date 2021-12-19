using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eTickets.Data.Services;

namespace eTickets.Controllers
{
	public class CinemasController : Controller
	{
		private readonly ICinemasService _service;

		public CinemasController(ICinemasService service)
		{
			_service = service;
		}

		public async Task<IActionResult> Index()
		{
			var cinemas = await _service.GetAllAsync();
			return View(cinemas);
		}
	}
}
