using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class ProductController : SupperController
    {
        private IWebHostEnvironment _envior = null;
        public ProductController(IWebHostEnvironment p)
        {
            _envior = p;
        }
        DbDemoContext db = new DbDemoContext();
        public IActionResult List(CKeywordViewModel vm)
        {
            IEnumerable<TProduct> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = from t in db.TProducts select t;
            }
            else
            {
                datas = db.TProducts.Where(p => p.FName.ToUpper().Contains(vm.txtKeyword.ToUpper()));
            }
            
            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TProduct prod)
        {
            db.TProducts.Add(prod);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == id);
                if (prod != null)
                {
                    db.TProducts.Remove(prod);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            TProduct prod = db.TProducts.FirstOrDefault(p => p.FId == id);
            if (prod == null)
            {
                return RedirectToAction("List");
            }
            CProductWrap prodWrap = new CProductWrap();
            //把整個TProduct丟給CProductWrap的product
            prodWrap.product = prod;
            return View(prodWrap);
        }

        [HttpPost]
        public IActionResult Edit(CProductWrap pIn)
        {
            TProduct pDb = db.TProducts.FirstOrDefault(p => p.FId == pIn.FId);
            if (pDb != null)
            {
                if (pIn.photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    pDb.FImagePath = photoName;
                    //pIn.photo.SaveAs(Server.MapPath("../../Images/" + photoName)); 舊版的
                    //新版用CopyTo跟FileStream,存到的串流為「_envior.WebRootPath + "/images/" + photoName」,後面是假設沒有此串流就創建一個
                    pIn.photo.CopyTo(new FileStream(_envior.WebRootPath + "/images/" + photoName, FileMode.Create));
                }
                pDb.FName = pIn.FName;
                pDb.FCost = pIn.FCost;
                pDb.FPrice = pIn.FPrice;
                pDb.FQty= pIn.FQty;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
