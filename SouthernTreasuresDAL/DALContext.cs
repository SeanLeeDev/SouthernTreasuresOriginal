using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Logger;

namespace SouthernTreasuresDAL
{
    // Mapper provides the connection between SQL Server and Visual Studio's 
    // two different ways to write data
    public class Mapper
    {
        public static CategoryDAL CategoryDALFromReader(SqlDataReader r)
        {
            CategoryDAL rv = new CategoryDAL();
            rv.CategoryID = r.GetInt32(0);
            rv.CategoryName = r.GetString(1);
            return rv;
        }

        public static OfferDAL OfferDALFromReader(SqlDataReader r)
        {
            OfferDAL rv = new OfferDAL();
            rv.OfferID = r.GetInt32(0);
            rv.OfferPrice = r.GetDecimal(1);
            rv.UserID = r.GetInt32(2);
            rv.ProductID = r.GetInt32(3);
            rv.StartTime = r.GetDateTime(4);
            rv.OfferInfo = r.GetInt32(5);
            return rv;
        }
        public static UserDAL UserDALFromReader(SqlDataReader r)
        {
            UserDAL rv = new UserDAL();
            rv.UserID = r.GetInt32(0);
            rv.Name = r.GetString(1);
            rv.Password = r.GetString(2);
            rv.Role = r.GetString(3);
            rv.EmailAddress = r.GetString(4);
            rv.CreditInfo = r.GetString(5);
            return rv;
        }
        public static ProductDAL ProductDALFromReader(SqlDataReader r)
        {   //conversion from SQL to Visual Studio
            ProductDAL rv = new ProductDAL();
            rv.ProductID = r.GetInt32(0);
            rv.ProductName = r.GetString(1);
            rv.ProductDescription = r.GetString(2);
            rv.MinimumPrice = r.GetDecimal(3);
            rv.CategoryID = r.GetInt32(4);
            rv.UserID = r.GetInt32(5);
            return rv;
        }
    }
    // This is my context
    // IDisposable has the "using" built in and prevents not properly closing the connection string 
    public class DALContext : IDisposable
    {
        private System.Data.SqlClient.SqlConnection
        _connection = new SqlConnection();
        //is necessary to connect to the database and provides everything to connect to each other
        public string ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }
        // if the context gets out of scope there will be a way to clean it up
        public void Dispose()
        {
            _connection.Dispose();
        }
        private void EnsureConnected()
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                {
                    _connection.Open();
                }
            }
            catch (Exception ex)
            {
                // log the exception
                LogMe.Log(ex);
                throw;
            }
        }


        public int InsertNewCategory(string CategoryName)
        {
            EnsureConnected();
            // don't have to call dispose if I use "using"
            using (SqlCommand command = new SqlCommand("SP_InsertCategory", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryID", 0);
                command.Parameters["@CategoryID"].Direction = ParameterDirection.InputOutput;
                command.Parameters.AddWithValue("@CategoryName", CategoryName);
                // activates exception handling 
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
                return Convert.ToInt32(command.Parameters["@CategoryID"].Value);
            }
        }
        public void UpdateCategory(int ExistingCategoryID, string NewCategoryName)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_UpdateCategory", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryID", ExistingCategoryID);
                command.Parameters.AddWithValue("@NewCategoryName", NewCategoryName);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }

            }
        }
        public void DeleteCategory(int ExistingCategoryId)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_DeleteCategory", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryID", ExistingCategoryId);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }
        public List<CategoryDAL> ReadAllCategories()
        {
            EnsureConnected();
            List<CategoryDAL> rv = new List<CategoryDAL>();
            using (SqlCommand cmd = new SqlCommand("SP_ReadAllCategories", _connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {

                        while (r.Read())
                        {
                            CategoryDAL category = Mapper.CategoryDALFromReader(r);
                            rv.Add(category);
                        }

                        return rv;

                    }
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }


        }
        public CategoryDAL ReadSpecificCategory(int CategoryID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_ReadSpecificCategory", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Mapper.CategoryDALFromReader(reader);
                    }
                    else return null;

                }
            }
        }
        public void InsertUser(string Name, string Password, string Role, string EmailAddress, string CreditInfo)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_InsertUser", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@Role", Role);
                command.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                command.Parameters.AddWithValue("@UserID", 0);
                command.Parameters.AddWithValue("@CreditInfo", CreditInfo);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }
        public void UpdateUser(int ExistingUserID, string NewName, string NewPassword, string NewRole, string NewEmailAddress, string NewCreditInfo)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_UpdateUser", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", ExistingUserID);
                command.Parameters.AddWithValue("@Name", NewName);
                command.Parameters.AddWithValue("@Password", NewPassword);
                command.Parameters.AddWithValue("@Role", NewRole);
                command.Parameters.AddWithValue("@EmailAddress", NewEmailAddress);
                command.Parameters.AddWithValue("@CreditInfo", NewCreditInfo);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }

            }
        }
        public void DeleteUser(int UserId)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_DeleteUser", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserId);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }
        public List<UserDAL> ReadAllUsers()
        {
            EnsureConnected();
            List<UserDAL> rv = new List<UserDAL>();
            using (SqlCommand cmd = new SqlCommand("SP_ReadAllUsers", _connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {

                        while (r.Read())
                        {
                            UserDAL user = Mapper.UserDALFromReader(r);
                            rv.Add(user);
                        }

                        return rv;

                    }
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }


        }
        public UserDAL ReadSpecificUser(int UserID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_ReadSpecificUser", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", UserID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Mapper.UserDALFromReader(reader);
                    }

                    else return null;

                }
            }
        }
        public UserDAL ReadSpecificUserByEmailAddress(string EmailAddress)
        {
            if (string.IsNullOrWhiteSpace(EmailAddress))
            {
                return null;
            }
                EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_ReadSpecificUserByEmailAddress", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Mapper.UserDALFromReader(reader);
                    }

                    else return null;

                }
            }
        }
        public void InsertProduct(string ProductName, string ProductDescription, decimal MinimumPrice, int CategoryID, int UserID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_InsertProduct", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", 0);
                command.Parameters.AddWithValue("@ProductName", ProductName);
                command.Parameters.AddWithValue("@ProductDescription", ProductDescription);
                command.Parameters.AddWithValue("@MinimumPrice", MinimumPrice);
                command.Parameters.AddWithValue("@CategoryID", CategoryID);
                command.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }
        public void UpdateProduct(int ExistingProductID, string NewProductName, string NewProductDescription, decimal NewMinimumPrice, int NewCategoryID, int NewUserID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_UpdateProduct", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", ExistingProductID);
                command.Parameters.AddWithValue("@NewProductName", NewProductName);
                command.Parameters.AddWithValue("@NewProductDescription", NewProductDescription);
                command.Parameters.AddWithValue("@NewMinimumPrice", NewMinimumPrice);
                command.Parameters.AddWithValue("@NewCategoryID", NewCategoryID);
                command.Parameters.AddWithValue("@NewUserID", NewUserID);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }

            }
        }
        public void DeleteProduct(int ExistingProductId)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_DeleteProduct", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", ExistingProductId);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }
        public List<ProductDAL> ReadAllProducts()
        {
            EnsureConnected();
            List<ProductDAL> rv = new List<ProductDAL>();
            using (SqlCommand cmd = new SqlCommand("SP_ReadAllProducts", _connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {

                        while (r.Read())
                        {
                            ProductDAL Product = Mapper.ProductDALFromReader(r);
                            rv.Add(Product);
                        }

                        return rv;

                    }
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }


        }
        public ProductDAL ReadSpecificProduct(int ProductID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_ReadSpecificProduct", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", ProductID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Mapper.ProductDALFromReader(reader);
                    }
                    else return null;

                }
            }
        }
        public void InsertOffer(int OfferID, decimal OfferPrice, int UserID, int ProductID, DateTime StartTime, int OfferInfo)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_InsertOffer", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("OfferID", OfferID);
                command.Parameters["OfferID"].Direction = ParameterDirection.InputOutput;
                command.Parameters.AddWithValue("@OfferPrice", OfferPrice);
                command.Parameters.AddWithValue("@UserID", UserID);
                command.Parameters.AddWithValue("@ProductID", ProductID);
                command.Parameters.AddWithValue("@StartTime", StartTime);
                command.Parameters.AddWithValue("@OfferInfo", OfferInfo);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }
        public void UpdateOffer(int ExistingOfferID, decimal NewPrice, int NewUserID, int NewProductID, DateTime NewStartTime, int NewOfferInfo)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_UpdateOffer", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferID", ExistingOfferID);
                command.Parameters.AddWithValue("@NewOfferPrice", NewPrice);
                command.Parameters.AddWithValue("@NewUserID", NewUserID);
                command.Parameters.AddWithValue("@NewProductID", NewProductID);
                command.Parameters.AddWithValue("@NewStartTime", NewStartTime);
                command.Parameters.AddWithValue("@NewOfferInfo", NewOfferInfo);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }

            }
        }
        public void DeleteOffer(int OfferId)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_DeleteOffer", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferID", OfferId);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }
        public List<OfferDAL> ReadAllOffers()
        {
            EnsureConnected();
            List<OfferDAL> rv = new List<OfferDAL>();
            using (SqlCommand cmd = new SqlCommand("SP_ReadAllOffers", _connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {

                        while (r.Read())
                        {
                            OfferDAL offer = Mapper.OfferDALFromReader(r);
                            rv.Add(offer);
                        }

                        return rv;

                    }
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }


        }
        public OfferDAL ReadSpecificOffer(int OfferID)
        {
            EnsureConnected();
            using (SqlCommand command = new SqlCommand("SP_ReadSpecificOffer", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@OfferID", OfferID);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return Mapper.OfferDALFromReader(reader);
                    }
                    else return null;

                }
            }
        }
        public List<OfferDAL> ReadOffersBySpecificUser(string EmailAddress)
        {
            EnsureConnected();
            List<OfferDAL> rv = new List<OfferDAL>();
            using (SqlCommand cmd = new SqlCommand("SP_ReadOffersBySpecificUser", _connection))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {

                        while (r.Read())
                        {
                            OfferDAL offer = Mapper.OfferDALFromReader(r);
                            rv.Add(offer);
                        }

                        return rv;

                    }
                }
                catch (Exception ex)
                {
                    LogMe.Log(ex);
                    throw;
                }
            }
        }

            public List<OfferDAL> ReadOffersBySpecificSeller(string EmailAddress)
            {
                EnsureConnected();
                List<OfferDAL> rv = new List<OfferDAL>();
                using (SqlCommand cmd = new SqlCommand("SP_ReadOffersBySpecificSeller", _connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                        using (SqlDataReader r = cmd.ExecuteReader())
                        {

                            while (r.Read())
                            {
                                OfferDAL offer = Mapper.OfferDALFromReader(r);
                                rv.Add(offer);
                            }

                            return rv;

                        }
                    }
                    catch (Exception ex)
                    {
                    LogMe.Log(ex);
                    throw;
                }
                }
            }

        }
    }

