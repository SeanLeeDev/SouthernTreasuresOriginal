using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SouthernTreasuresDAL;

namespace SouthernTreasuresBLL
{
    public class CategoryBLL
    {
        public CategoryBLL (CategoryDAL categoryDAL)
        {
            CategoryID = categoryDAL.CategoryID;
            CategoryName = categoryDAL.CategoryName;
        }
        public CategoryBLL()
        {

        }

        public override string ToString()
        {
            return $"CategoryID:{CategoryID} CategoryName:{CategoryName}";
        }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        private List<CategoryBLL> _category = null;
        public List<CategoryBLL> Category
        {
            get
            {
                if (_category == null)
                {
                    throw new Exception("No Category here");
                }
                return _category;
            }
        }
    }
}
