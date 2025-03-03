using System.ComponentModel;

namespace ecommerce_shop.Models
{
    public class Product
    {
        public int id { get;  set; }

        public string? name { get; set; } = default!;

        public Category category { get; set; }

        public Guid created_by { get; set; }
    }
}
