using System.Collections.Generic;
using System.Data.SqlClient;
using MyWebApp.Models;
using System.Configuration;
using Web.Common;


namespace MyWepApi.DataAccess
{
    public class AdventureWorksRepository : IAdventureWorksRepository
    {
        private readonly string _adventureWorksConnectionString;

        public AdventureWorksRepository()
        {
            _adventureWorksConnectionString = ConfigurationManager.ConnectionStrings["AdventureWorksDB"].ConnectionString;
        }

        public List<ProductViewModel> GetPaginatedProducts(int page, int pageSize)
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var conn = new SqlConnection(_adventureWorksConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "[dbo].[usp_PaginatedProducts]";
                    cmd.Parameters.AddWithValue("@Page", page);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductViewModel()
                            {
                                ProductID = reader.GetInt32Value("ProductID"),
                                Name = reader.GetStringValue("Name"),
                                ProductNumber = reader.GetStringValue("ProductNumber"),
                                Color = reader.GetStringValue("Color"),
                                StandardCost = reader.GetDecimalValue("StandardCost"),
                                ListPrice = reader.GetDecimalValue("ListPrice"),
                                Size = reader.GetStringValue("Size"),
                                Weight = reader.GetNullableDecimalValue("Weight"),
                                Class = reader.GetNullableCharValue("Class"),
                                Style = reader.GetNullableCharValue("Style"),
                                SellStartDate = reader.GetDateTimeValue("SellStartDate"),
                                SellEndDate = reader.GetNullableDateTimeValue("SellEndDate"),
                                RowGuid = reader.GetGuidValue("rowguid"),
                                ModifiedDate = reader.GetDateTimeValue("ModifiedDate"),
                                RowCount = reader.GetInt32Value("RowCount")

                            });
                        }
                        conn.Close();
                    }
                }
            }

            return products;
        }
    }
}