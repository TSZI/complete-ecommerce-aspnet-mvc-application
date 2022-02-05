using eTickets.Data.Cart;
using eTickets.Data.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using eTickets.Data.Static;

namespace eTickets.Controllers
{

	[Authorize]
	public class OrdersController : Controller
	{

		private readonly IMoviesService _moviesService;
		private readonly ShoppingCart _shoppingCart;
		private readonly IOrdersService _ordersService;

		public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
		{
			_moviesService = moviesService;
			_shoppingCart = shoppingCart;
			_ordersService = ordersService;
		}

		public async Task<IActionResult> Index()
		{
			string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			string userRole = User.FindFirst(ClaimTypes.Role).Value;

			var orders = await _ordersService.GetOrderByUserIdAndRoleAsync(userId, userRole);
			return View(orders);
		}

		public IActionResult ShoppingCart()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			var response = new VM_ShoppingCart()
			{
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};

			return View(response);
		}

		public async Task<IActionResult> AddItemToShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);

			if(item != null)
			{
				_shoppingCart.AddItemToCart(item);
			}

			return RedirectToAction(nameof(ShoppingCart));
		}

		public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
		{
			var item = await _moviesService.GetMovieByIdAsync(id);

			if (item != null)
			{
				_shoppingCart.RemoveItemFromCart(item);
			}

			return RedirectToAction(nameof(ShoppingCart));
		}

		public async Task<IActionResult> CompleteOrder()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			string userEmailAddress = User.FindFirst(ClaimTypes.Email).Value;

			await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
			await _shoppingCart.ClearShoppingCartAsync();

			return View("OrderCompleted");
		}
	}
}
