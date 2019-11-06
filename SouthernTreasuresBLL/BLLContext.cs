using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SouthernTreasuresDAL;

namespace SouthernTreasuresBLL
{
    public class BLLContext : IDisposable
    {
        private SouthernTreasuresDAL.DALContext dalCtx = new DALContext();

        public BLLContext()
        {

            dalCtx.ConnectionString = @"Data Source=GDC-BC-13\SQLEXPRESS;Initial Catalog=SouthernTreasures;Integrated Security=True";

        }

        public void Dispose()
        {
            dalCtx.Dispose();
        }

        // BLL CRUD for the table "category"

        public List<CategoryBLL> GetAllCategories()
        {
            List<CategoryBLL> rv = new List<CategoryBLL>();
            List<CategoryDAL> list = dalCtx.ReadAllCategories();
            foreach (CategoryDAL item in list)
            {
                rv.Add(new CategoryBLL(item));
            }
            return rv;
        }

        public CategoryBLL GetCategory(int CategoryID)
        {

            CategoryDAL rdal = dalCtx.ReadSpecificCategory(CategoryID);
            if (rdal == null) return null;
            return new CategoryBLL(rdal);
        }
        public void DeleteCategory(int CategoryID)
        {
            dalCtx.DeleteCategory(CategoryID);
        }
        public void DeleteCategory(CategoryBLL category)
        {
            DeleteCategory(category.CategoryID);
        }
        public void UpdateCategory(int CategoryID, string CategoryName)
        {
            dalCtx.UpdateCategory(CategoryID, CategoryName);
        }
        public void UpdateCategory(CategoryBLL category)
        {
            UpdateCategory(category.CategoryID, category.CategoryName);
        }
        public int InsertNewCategory(string CategoryName)
        {
            return dalCtx.InsertNewCategory(CategoryName);
        }

        // BLL CRUD for the table "products"

        public List<ProductsBLL> GetAllProducts()
        {
            List<ProductsBLL> rv = new List<ProductsBLL>();
            List<ProductDAL> list = dalCtx.ReadAllProducts();
            foreach (ProductDAL item in list)
            {
                rv.Add(new ProductsBLL(item));
            }
            return rv;
        }

        public ProductsBLL GetProduct(int ProductID)
        {

            ProductDAL rdal = dalCtx.ReadSpecificProduct(ProductID);
            if (rdal == null) return null;
            return new ProductsBLL(rdal);
        }
        public void DeleteProduct(int ProductID)
        {
            dalCtx.DeleteProduct(ProductID);
        }
        public void DeleteProduct(ProductsBLL products)
        {
            DeleteProduct(products.ProductID);
        }
        public void UpdateProduct(int ProductID, string ProductName, string ProductDescription, decimal MinimumPrice, int CategoryID, int UserID)
        {
            dalCtx.UpdateProduct(ProductID, ProductName, ProductDescription, MinimumPrice, CategoryID, UserID);
        }
        public void UpdateProduct(ProductsBLL products)
        {
            UpdateProduct(products.ProductID, products.ProductName, products.ProductDescription, products.MinimumPrice, products.CategoryID, products.UserID);
        }
        public void InsertProduct(string ProductName, string ProductDescription, decimal MinimumPrice, int CategoryID, int UserID)
        {
            dalCtx.InsertProduct(ProductName, ProductDescription, MinimumPrice, CategoryID, UserID);
        }

        // BLL CRUD for the table "offer"
        public List<OfferBLL> GetAllOffers()
        {
            List<OfferBLL> rv = new List<OfferBLL>();
            List<OfferDAL> list = dalCtx.ReadAllOffers();
            foreach (OfferDAL item in list)
            {
                rv.Add(new OfferBLL(item));
            }
            return rv;
        }

        public OfferBLL GetOffer(int OfferID)
        {

            OfferDAL rdal = dalCtx.ReadSpecificOffer(OfferID);
            if (rdal == null) return null;
            return new OfferBLL(rdal);
        }
        public List<OfferBLL> GetOffersBySpecificSeller(string EmailAddress)
        {
            List<OfferBLL> rv = new List<OfferBLL>();
            List<OfferDAL> list = dalCtx.ReadOffersBySpecificSeller(EmailAddress);

            foreach (OfferDAL item in list)
            {            
                rv.Add(new OfferBLL(item));
            }
            return rv;

        }
        public void DeleteOffer(int OfferID)
        {
            dalCtx.DeleteOffer(OfferID);
        }
        public void DeleteOffer(OfferBLL offer)
        {
            DeleteOffer(offer.OfferID);
        }
        public void UpdateOffer(int OfferID, decimal OfferPrice, int NewUserID, int NewProductID, DateTime NewStartTime, int NewOfferInfo)
        {
            dalCtx.UpdateOffer(OfferID, OfferPrice, NewUserID, NewProductID, NewStartTime, NewOfferInfo);
        }
        public void UpdateOffer(OfferBLL offer)
        {
            UpdateOffer(offer.OfferID, offer.OfferPrice, offer.UserID, offer.ProductID, offer.StartTime, (int)offer.OfferInfo);
        }
        public void InsertOffer(int OfferID, decimal OfferPrice, int UserID, int ProductID, DateTime StartTime, int OfferInfo)
        {
            dalCtx.InsertOffer(OfferID, OfferPrice, UserID, ProductID, StartTime, OfferInfo);
        }
        public List<OfferBLL> GetOffersBySpecificUser(string EmailAddress)
        {
            List<OfferBLL> rv = new List<OfferBLL>();
            List<OfferDAL> list = dalCtx.ReadOffersBySpecificUser(EmailAddress);
            foreach (OfferDAL item in list)
            {
                rv.Add(new OfferBLL(item));
            }
            return rv;
        }

        // BLL CRUD for the table "user"

        public List<UserBLL> GetAllUsers()
        {
            List<UserBLL> rv = new List<UserBLL>();
            List<UserDAL> list = dalCtx.ReadAllUsers();
            foreach (UserDAL item in list)
            {
                rv.Add(new UserBLL(item));
            }
            return rv;
        }

        public UserBLL GetUser(int UserID)
        {

            UserDAL rdal = dalCtx.ReadSpecificUser(UserID);
            if (rdal == null) return null;
            return new UserBLL(rdal);
        }
        public UserBLL GetUserByEmailAddress(string EmailAddress)
        {

            UserDAL rdal = dalCtx.ReadSpecificUserByEmailAddress(EmailAddress);
            if (rdal == null) return null;
            return new UserBLL(rdal);
        }
        public void DeleteUser(int UserID)
        {
            dalCtx.DeleteUser(UserID);
        }
        public void DeleteUser(UserBLL user)
        {
            DeleteUser(user.UserID);
        }
        public void UpdateUser(int ExistingUserID, string NewName, string NewPassword, string NewRole, string NewEmailAddress, string NewCreditInfo)
        {
            dalCtx.UpdateUser(ExistingUserID, NewName, NewPassword, NewRole, NewEmailAddress, NewCreditInfo);
        }
        public void UpdateUser(UserBLL user)
        {
            UpdateUser(user.UserID, user.Name, user.Password, user.Role, user.EmailAddress, user.CreditInfo);
        }
        public void InsertUser(string Name, string Password, string Role, string EmailAddress, string CreditInfo)
        {
            dalCtx.InsertUser(Name, Password, Role, EmailAddress, CreditInfo);
        }
        // This is my meaningful calculation in the BLL adding $10 shipping to any item
        public decimal AddShippingCharges(ProductsBLL p)
        {
            return p.MinimumPrice + 10m;
        }
        public decimal AddShippingCharges(OfferBLL p)
        {
            return p.OfferPrice + 10m;
        }
    }
}
