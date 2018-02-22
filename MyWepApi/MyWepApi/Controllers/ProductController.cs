using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyWepApi.DataAccess;
using MyWebApp.Models;



namespace MyWepApi.Controllers
{
    public class ProductController : ApiController
    {
        private IAdventureWorksRepository repo;

        public ProductController()
        {
            repo = new AdventureWorksRepository();
        }

        public IHttpActionResult Index()
        {
            return Ok();
        }

        public IHttpActionResult GetPaginatedProducts(int page, int pageSize)
        {
            var result = repo.GetPaginatedProducts(page, pageSize);
            return Ok(result);
        }
    }
}
