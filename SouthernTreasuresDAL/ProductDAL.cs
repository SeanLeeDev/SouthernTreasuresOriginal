using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthernTreasuresDAL
{
    public class ProductDAL
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal MinimumPrice { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }
        

        public override string ToString()
        {
            return $"ProductID:{ProductID} ProductName:{ProductName} ProductDescription:{ProductDescription} MinimumPrice:{MinimumPrice} CategoryID:{CategoryID} UserID:{UserID}";
        }
    }
}
