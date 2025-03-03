using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace ecommerce_shop.Models
{
    public class BaseModel
    {
        public bool Deleted { get; set; } = false;
        public DateTime Created_At { get; set; } = DateTime.UtcNow;
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public Guid UpdatedBy { get; set; }
    }
}
