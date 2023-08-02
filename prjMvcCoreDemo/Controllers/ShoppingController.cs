using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;
using System.Text.Json;

namespace prjMvcCoreDemo.Controllers
{
    public class ShoppingController : SupperController
    {
        DbDemoContext db = new DbDemoContext();
        public IActionResult List()
        {
            var datas = from p in db.TProducts select p;
            return View(datas);
        }
        public IActionResult AddToCart(int? id)
        {
            //先用id找要加入購物車的產品
            if (id == null)
                return RedirectToAction("List");
            ViewBag.FID = id;
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(CAddToCartViewModel vm)
        {
            //先用id找要加入購物車的產品
            TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == vm.txtFId);
            if (prod != null)
            {
                string json = "";
                List<CShoppingCartItem> cart = null;
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST))
                {
                    //key
                    json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST);
                    //value
                    cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);
                }
                else
                {
                    cart = new List<CShoppingCartItem>();
                }
                CShoppingCartItem item = new CShoppingCartItem();
                item.price = (decimal)prod.FPrice;
                item.productId = prod.FId;
                item.count = vm.txtCount;
                item.product = prod;
                cart.Add(item);
                json= JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST, json);
            }
            return RedirectToAction("List");
        }


        public IActionResult CartView()
        { 
            //if (cart == null)
            //        return RedirectToAction("List");舊版
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS_LIST)) //如果購物車沒有東西,回到商品列表
                return RedirectToAction("List");
            //key
            string json = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS_LIST); //如果購物車有東西,把購物車裡的商品顯示在畫面上
            //value
            //List<CShoppingCartItem> cart = Session[CDictionary.SK_PURCHASED_PRODUCTS_LIST] as List<CShoppingCartItem>;舊版
            List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(json);

            return View(cart);
        }
    }
}
