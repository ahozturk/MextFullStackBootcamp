﻿using MextFullStack.Domain.Entities;
using MextFullStack.WebApi.Data;
using MextFullStack.WebApi.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace MextFullStack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            return Ok(FakeDatabase.Categories);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var category = FakeDatabase
                .Categories
                .FirstOrDefault(p => p.Id == id);

            if (category == null)
                return NotFound("Aradaginiz urun sistemde bulunamadi.");

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryRequestModel createCategoryRequestModel)
        {
            var category = new Category
            {
                Name = createCategoryRequestModel.Name,
                Description = createCategoryRequestModel.Description,
                IsActive = true,
                Id = Guid.NewGuid(),
            };

            if (category.Name.Length <= 2)
                return BadRequest("Kategori ismi en az 2 karakter olmalidir.");

            if (category.Description.Length <= 10)
                return BadRequest("Kategori aciklamasi 10 karakterden buyuk olmalidir.");


            if (FakeDatabase.Categories.Any(c => c.Name.ToLowerInvariant() == category.Name.ToLowerInvariant()))
                return BadRequest("Bu isimde bir kategori zaten mevcut.");

            FakeDatabase.Categories.Add(category);

            return Ok(category.Id);
        }

    }
}