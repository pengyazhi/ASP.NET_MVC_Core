using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using projMauiDemo.Resources.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace prjMvcCoreDemo.Controllers
{
    public class AController : Controller
    {
        public string demoJson2Obj()
        {
            string json = demoObj2Json();
            TCustomer x = JsonSerializer.Deserialize<TCustomer>(json);
            return x.FName + "<br/>" + x.FPhone;

        }
        public string demoObj2Json()
        {
            TCustomer x = new TCustomer()
            {
                FId = 1,
                FName = "Amy",
                FAddress = "Taipe",
                FEmail = "amy@gmail.com",
                FPhone = "091123456",
                FPassword = "1234"
            };
            string json = JsonSerializer.Serialize(x);
            return json;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string lotto()
        {
            //Models加的是「共用方法」
            //直接加入現有的class並直接new一塊新的記憶體呼叫裡面的方法
            return (new CLottoGen()).getNumbers();
        }
        public IActionResult showById()
        {
            return View();
        }
        public ActionResult showCountBySession() 
        {
            int count = 0;
            //if (Session["COUNT"] != null)舊版
            if(HttpContext.Session.Keys.Contains("COUNT"))
                //因為GetInt32傳回的型別是int?所以要轉成int
                count = (int)HttpContext.Session.GetInt32("COUNT");
            count++;
            //Session["COUNT"] = count; 舊版
            HttpContext.Session.SetInt32("COUNT", count);
            ViewBag.COUNT = count;
            return View();
        }
    }
}
