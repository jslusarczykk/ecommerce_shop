using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace EShop.Domain.Models
{
    public class Category : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
