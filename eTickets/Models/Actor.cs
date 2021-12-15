﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Models
{
	public class Actor
	{
		[Key]
		public int Id { get; set; }

		[Display(Name = "Profile Picture")]
		[Required(ErrorMessage = "Profile Picture is required")]
		public string ProfilePictureURL { get; set; }

		[Display(Name = "Full Name")]
		[Required(ErrorMessage = "Full Name is required")]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between {2} and {1} chars.")]
		public string FullName { get; set; }

		[Display(Name = "Biography")]
		[Required(ErrorMessage = "Biography is required")]
		public string Bio { get; set; }

		// Relationships
		public List<Actor_Movie> Actors_Movies { get; set; }
	}
}