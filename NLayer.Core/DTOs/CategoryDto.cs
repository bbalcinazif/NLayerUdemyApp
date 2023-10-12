using NLayer.Core.Models;

namespace NLayer.Core.DTOs
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
