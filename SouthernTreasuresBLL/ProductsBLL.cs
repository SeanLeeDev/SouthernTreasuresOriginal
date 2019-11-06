using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SouthernTreasuresDAL;

namespace SouthernTreasuresBLL
{
    public class ProductsBLL
    {  
        public ProductsBLL(ProductDAL productDAL)
        {   // this constructor is used by my code to create a BLL object from a DAL object
            ProductID = productDAL.ProductID;
            ProductName = productDAL.ProductName;
            ProductDescription = productDAL.ProductDescription;
            MinimumPrice = productDAL.MinimumPrice;
            CategoryID = productDAL.CategoryID;
            UserID = productDAL.UserID;
           
        }
        // this constructor is used by the MVC controller
        public ProductsBLL()
        {

        }
        // This is here for testing purposes with console
        public override string ToString()
        {
            return $"ProductID:{ProductID} ProductName:{ProductName} ProductDescription:{ProductDescription} MinimumPrice:{MinimumPrice} CategoryID:{CategoryID} UserID:{UserID}";
        }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal MinimumPrice { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }

        private List<ProductsBLL> _product=null;
        public List<ProductsBLL> Product
        {
            get
            {
                if (_product == null)
                {
                    throw new Exception("No Products here");
                }
                return _product;
            }
        }
    }
}
