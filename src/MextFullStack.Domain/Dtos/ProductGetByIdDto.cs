﻿using MextFullStack.Domain.Entities;

namespace MextFullStack.Domain.Dtos
{
    public class ProductGetByIdDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool CategoryIsActive { get; set; }


        public static ProductGetByIdDto FromProduct(Product product, Category category)
        {
            return new ProductGetByIdDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = category.Name,
                CategoryIsActive = category.IsActive
            };
        }
    }
}
