﻿using eTickets.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
	public class ProducersController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProducersController(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var producers = await _context.Producers.ToListAsync();
			return View(producers);
		}
	}
}