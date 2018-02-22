using System.Collections.Generic;
using MyWebApp.Models;

namespace MyWepApi.DataAccess
{
    public interface IAdventureWorksRepository
    {
        List<ProductViewModel>  GetPaginatedProducts(int page, int pageSize);
    }
}