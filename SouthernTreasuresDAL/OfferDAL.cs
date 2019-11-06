using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthernTreasuresDAL
{
    public class OfferDAL
    {
        public int OfferID {get; set;}
        public decimal OfferPrice { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }    
        public DateTime StartTime { get; set; }
        public int OfferInfo { get; set; }

        public override string ToString()
        {
            return $"OfferID:{OfferID} OfferPrice:{OfferPrice} UserID:{UserID} ProductID:{ProductID} Starttime:{StartTime} OfferInfo:{OfferInfo}";
        }
    }
}
