using MextFullStack.Domain;
using MextFullStack.Domain.Entities;
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
            var categories = FakeDatabase.Categories;

            List<CategoryGetAllDto> categoryDtos = categories
                .Select(x => CategoryGetAllDto.FromCategory(x))
                .ToList();

            return Ok(categoryDtos);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var category = FakeDatabase
                .Categories
                .FirstOrDefault(p => p.Id == id);

            var result = CategoryGetAllDto.FromCategory(category);

            if (category == null)
                return NotFound("Aradaginiz urun sistemde bulunamadi.");

            return Ok(result);
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
        
        [HttpPut]
        public IActionResult Update(CategoryUpdateDto categoryUpdateDto)
        {
            //Model validation
            if (categoryUpdateDto.Id == Guid.Empty)
                return BadRequest("Gecersiz kategori id'si.");

            if (categoryUpdateDto.Description.Length <= 10)
                return BadRequest("Kategori aciklamasi 10 karakterden buyuk olmalidir.");

            if (FakeDatabase.Categories.Any(c => c.Name.ToLowerInvariant() == categoryUpdateDto.Name.ToLowerInvariant()))
                return BadRequest("Bu isimde bir kategori zaten mevcut.");
             
            //Finding the category by id
            var categoryToUpdateIndex = FakeDatabase.Categories.FindIndex(c => c.Id == categoryUpdateDto.Id);

            //Control is exist
            if (categoryToUpdateIndex == -1)
                return NotFound("Kategori bulunamadi.");

            //Update category
            FakeDatabase.Categories[categoryToUpdateIndex].Name = categoryUpdateDto.Name;
            FakeDatabase.Categories[categoryToUpdateIndex].Description = categoryUpdateDto.Description;
            FakeDatabase.Categories[categoryToUpdateIndex].ModifiedOn = DateTime.Now;

            //Return id
            return Ok(categoryUpdateDto.Id);
        }
    }
}
