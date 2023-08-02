using Microsoft.AspNetCore.Mvc;
using prjMvcCoreDemo.Models;
using prjMvcCoreDemo.ViewModels;

namespace prjMvcCoreDemo.Controllers
{
    public class CustomerController : SupperController
    {
        DbDemoContext db = new DbDemoContext();
        
        public IActionResult List(CKeywordViewModel vm)
        {
            IEnumerable<TCustomer> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
            {
                datas = from t in db.TCustomers select t;
            }
            else
            {
                datas = db.TCustomers.Where(p => p.FName.ToUpper().Contains(vm.txtKeyword.ToUpper())
                || p.FAddress.ToUpper().Contains(vm.txtKeyword.ToUpper())
                || p.FEmail.ToUpper().Contains(vm.txtKeyword.ToUpper())
                || p.FPhone.ToUpper().Contains(vm.txtKeyword.ToUpper()));
            }
            
            return View(datas);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TCustomer customer)
        {
            db.TCustomers.Add(customer);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                TCustomer cust = db.TCustomers.FirstOrDefault(p => p.FId == id);
                if (cust != null)
                {
                    db.TCustomers.Remove(cust);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("List");
            TCustomer cust = db.TCustomers.FirstOrDefault(p => p.FId == id);
            if (cust == null)
            {
                return RedirectToAction("List");
            }
            return View(cust);
        }

        [HttpPost]
        public IActionResult Edit(TCustomer pIn)
        {
            TCustomer pDb = db.TCustomers.FirstOrDefault(p=>p.FId == pIn.FId);
            if (pDb != null)
            {
                pDb.FEmail = pIn.FEmail;
                pDb.FName = pIn.FName;
                pDb.FAddress = pIn.FAddress;
                pDb.FPassword = pIn.FPassword;
                pDb.FPhone = pIn.FPhone;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
