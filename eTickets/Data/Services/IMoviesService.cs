using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;

namespace eTickets.Data.Services
{
	public interface IMoviesService : IEntityBaseRepository<Movie>
	{
		// Signature
		Task<Movie> GetMovieByIdAsync(int id);
		Task<VM_NewMovieDropDown> GetNewMovieDropDownsValues();
		Task AddNewMovieAsync(VM_NewMovie data);
		Task UpdateMovieAsync(VM_NewMovie data);
	}
}
