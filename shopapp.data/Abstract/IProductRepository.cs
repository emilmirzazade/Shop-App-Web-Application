using shopapp.entityy;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace shopapp.data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByCategory(string name, int page, int pageSize);
        Product GetProductDetails(string url);
        Product GetByIdWithCategories(int id);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetHomePageProducts();
        int getCountByCategory(string category);
        void Update(Product entity, int[] categoryIds);
    }
}
