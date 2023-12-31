﻿using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductList ListProducts(int page)
		{
			var skip = 0;
			if (page > 1)
				skip = (page - 1) * 10;
			return new ProductList() { HasNext = false, TotalCount = 10, Products = _ctx.Products.Skip(skip).Take(10).ToList() };
		}

	}
}
