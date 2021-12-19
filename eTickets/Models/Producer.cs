using eTickets.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
	public class Producer : IEntityBase
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Profile picture is required")]
		[Display(Name = "Profile Picture")]
		public string ProfilePictureURL { get; set; }

		[Required(ErrorMessage = "Full name is required")]
		[Display(Name = "Full Name")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} chars")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Biography is required")]
		[Display(Name = "Biography")]
		public string Bio { get; set; }

		// Relationships
		public List<Movie> Movies { get; set; }
	}
}
