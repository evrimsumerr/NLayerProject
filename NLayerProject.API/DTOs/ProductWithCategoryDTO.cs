using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.DTOs
{
    public class ProductWithCategoryDTO : ProductDTO
    {
        public CategoryDTO Category { get; set; }
    }
}
