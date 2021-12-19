using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public class CinemasService: EntityBaseRepository<Cinema>, ICinemasService
	{
		public CinemasService(ApplicationDbContext context) : base(context)
		{
		}
	}
}
