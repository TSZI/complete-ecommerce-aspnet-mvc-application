using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eTickets.Data.Cart;

namespace eTickets.Data.ViewModels
{
	public class VM_ShoppingCart
	{
		public ShoppingCart ShoppingCart { get; set; }

		public double ShoppingCartTotal { get; set; }
	}
}
