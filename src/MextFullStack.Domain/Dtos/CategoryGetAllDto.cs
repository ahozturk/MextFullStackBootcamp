using MextFullStack.Domain.Entities;

namespace MextFullStack.Domain;

public class CategoryGetAllDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public static CategoryGetAllDto FromCategory(Category category)
    {
        return new CategoryGetAllDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        };
    }
}
