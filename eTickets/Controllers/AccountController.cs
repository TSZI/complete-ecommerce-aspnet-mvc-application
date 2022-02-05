using eTickets.Data;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.ViewModels;
using eTickets.Data.Static;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
	public class AccountController : Controller
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ApplicationDbContext _context;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_context = context;
		}

		public async Task<IActionResult> Users()
		{
			var users = await _context.Users.ToListAsync();
			return View(users);
		}


		[HttpGet]
		public IActionResult Login()
		{
			return View(new VM_Login());
		}

		[HttpPost]
		public async Task<IActionResult> Login(VM_Login vmLogin)
		{
			if (!ModelState.IsValid) { return View(vmLogin); }

			var user = await _userManager.FindByEmailAsync(vmLogin.EmailAddress);

			if (user != null)
			{
				var passwordCheck = await _userManager.CheckPasswordAsync(user, vmLogin.Password);
				if (passwordCheck)
				{
					var result = await _signInManager.PasswordSignInAsync(user, vmLogin.Password, false, false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Movies");
					}
				}
				else
				{
					TempData["Error"] = "Wrong credentials. Please, try again!";
					return View(vmLogin);
				}
			}

			TempData["Error"] = "Wrong credentials. Please, try again!";
			return View(vmLogin);

		}

		[HttpGet]
		public IActionResult Register() => View(new VM_Register());

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(VM_Register vmRegister)
		{
			if (!ModelState.IsValid) { return View(vmRegister); }

			var user = await _userManager.FindByEmailAsync(vmRegister.EmailAddress);

			if (user != null)
			{
				TempData["Error"] = "This email address is already in user";
				return View(vmRegister);
			}

			var newUser = new ApplicationUser()
			{
				FullName = vmRegister.FullName,
				Email = vmRegister.EmailAddress,
				UserName = vmRegister.EmailAddress
			};

			var newUserResponse = await _userManager.CreateAsync(newUser, vmRegister.Password);

			if (newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(newUser, UserRoles.User);
			}

			return View("RegisterCompleted");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Movies");
		}

		public IActionResult AccessDenied(string ReturnUrl)
		{
			return View();
		}

	}
}
