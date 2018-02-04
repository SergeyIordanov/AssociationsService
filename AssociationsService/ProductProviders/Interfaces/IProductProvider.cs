using System.Collections.Generic;
using System.Threading.Tasks;
using AssociationsService.Entities.Product;

namespace AssociationsService.ProductProviders.Interfaces
{
    public interface IProductProvider
    {
        Task<IEnumerable<Product>> GetProductsAsync(string searchQuery);
    }
}