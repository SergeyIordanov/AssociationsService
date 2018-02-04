using System.Collections.Generic;
using AssociationsService.Entities.Product;

namespace AssociationsService.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> SearchProducts(string query);
    }
}