namespace prjMvcCoreDemo.Models
{
    public class CProductWrap
    {
        private TProduct _prod = null;
        public CProductWrap()
        {
            _prod = new TProduct();
        }
        public TProduct product { 
            get { return _prod; }
            set { _prod = value; }
        }
        public int FId 
        {
            get { return _prod.FId; }
            set { _prod.FId = value; } 
        }

        public string? FName
        {
            get { return _prod.FName; }
            set { _prod.FName = value; }
        }

        public int? FQty {
            get { return _prod.FQty; }
            set { _prod.FQty = value; } 
        }

        public decimal? FCost {
            get { return _prod.FCost; }
            set { _prod.FCost = value; } 
        }

        public decimal? FPrice {
            get { return _prod.FPrice; }
            set { _prod.FPrice = value; } 
        }

        public string? FImagePath {
            get { return _prod.FImagePath; }
            set { _prod.FImagePath = value; } 
        }
        public IFormFile photo { get;set; }
    }
}
