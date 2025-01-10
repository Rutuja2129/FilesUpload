using CodeTentaclesTaks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeTentaclesTaks.Controllers
{
    public class ProductsAPIController : ApiController
    {
        [Route("api/ProductAPI/GetAllProductsList")]
        public IHttpActionResult GetAllProductsList()
        {
            CodeTentaclesTaskEntities context = new CodeTentaclesTaskEntities();
            var products = context.tblProducts.ToList();
            return Ok(products);
        }
    }
}
