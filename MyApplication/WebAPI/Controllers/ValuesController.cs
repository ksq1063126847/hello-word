using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        public string GetTest(int id)
        {
            return "GetTest";
        }

        [HttpGet]
        public JsonResult<Product> MyTest(int id = 0)
        {
            Product p = new Product();
            p.ID = 1;
            p.Name = "one";
            return Json(p);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    [Serializable]
    public class Product
    {
        public int ID;
        public string Name;
    }
}
