using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace prjMvcCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePandaController : ControllerBase
    {
        // GET: api/<GamePandaController>  網址加上api/GamePanda 不用controller
        [HttpGet]
        public IEnumerable<TProduct> Get()
        {
            //從資料庫撈資料
           DbDemoContext db = new DbDemoContext();
            var datas = from c in db.TProducts
                        select c;
            foreach (var data in datas)
            {
                data.FCost = 0;
                if (data.FQty > 50)
                    data.FQty = 50;
            }
            //直接return資料,.net framework會自動轉成JSON格式
            return datas;
        }

        // GET api/<GamePandaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GamePandaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GamePandaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GamePandaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
