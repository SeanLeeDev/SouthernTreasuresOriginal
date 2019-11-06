using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthernTreasuresDAL
{
    public class CategoryDAL
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return $"CategoryID:{CategoryID} CategoryName:{CategoryName}";
        }
    }
}
