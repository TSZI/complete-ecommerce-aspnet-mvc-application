using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Display(Name = "Full name")]		
		public string FullName { get; set; }
	}
}
