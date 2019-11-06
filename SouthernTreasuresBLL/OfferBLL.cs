using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SouthernTreasuresDAL;
using SouthernTreasuresBLL;

namespace SouthernTreasuresBLL
{
    public enum OfferStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        CounterOffer = 3,
        Paid = 4,
        Shipped = 5,
        PackageReceived = 6,
    }
    
    
    public class OfferBLL
    {
        public OfferBLL(OfferDAL offerDAL)
        {
            OfferID = offerDAL.OfferID;
            OfferPrice = offerDAL.OfferPrice;
            UserID = offerDAL.UserID;
            ProductID = offerDAL.ProductID;
            StartTime = offerDAL.StartTime;
            // (OfferStatus) here forces an int to change into an enum
            // this is called casting
            OfferInfo = (OfferStatus)offerDAL.OfferInfo;
        }
        public OfferBLL()
        {

        }

        public override string ToString()
        {
            return $"OfferID:{OfferID} OfferPrice:{OfferPrice} UserID:{UserID} ProductID:{ProductID} StartTime:{StartTime} OfferInfo:{OfferInfo}";
        }
        public int OfferID { get; set; }
        public decimal OfferPrice{ get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public DateTime StartTime { get; set; }
        public OfferStatus OfferInfo { get; set; }

        private List<OfferBLL> _offer=null;
        public List<OfferBLL> Offer
        {
            get
            {
                if (_offer == null)
                {
                    throw new Exception("No Offers here");
                }
                return _offer;
            }
        }
    }

}
