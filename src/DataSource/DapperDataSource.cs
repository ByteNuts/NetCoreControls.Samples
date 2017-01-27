using System;
using Dapper;
using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ByteNuts.NetCoreControls.Samples.Models;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using ByteNuts.NetCoreControls.Samples.Models.Db;

namespace ByteNuts.NetCoreControls.Samples.DapperSource
{
    public class DapperDataSource : IDataAccess
    {
        public IOptions<ConnectionStringsModel> _connStrings { get; set; }
        public DapperDataSource(IOptions<ConnectionStringsModel> connStrings)
        {
            _connStrings = connStrings;

        }
        public List<dynamic> GetProductList()
        {
            var sqlCommand = $@" SELECT * FROM Products";
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                    return connection.Query<dynamic>(sqlCommand);
            }).Result.ToList();
        }

        public void UpdateProduct(ProductModel product)
        {
            object parameters = new
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                //UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                UnitsOnOrder = product.UnitsOnOrder,
                //ReorderLevel = product.ReorderLevel,
                Discontinued = product.Discontinued
            };
            var sqlCommand = $@" UPDATE p
                                SET p.ProductName = @ProductName,
                                    p.QuantityPerUnit = @QuantityPerUnit,
                                    p.UnitsInStock = @UnitsInStock,
                                    p.UnitsOnOrder = @UnitsOnOrder,
                                    p.Discontinued = @Discontinued
                                FROM Products p
                                WHERE p.ProductID = @ProductID";

            using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                connection.Execute(sqlCommand, parameters, null, null, CommandType.Text);
        }

        public Tuple<IList<dynamic>, int> GetProductListPaginated(int pageNumber, int pageSize)
        {
            object parameters = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var sqlCommand = $@"SELECT * FROM Products p ORDER BY
                        p.ProductID
                        OFFSET(@PageNumber - 1) * @PageSize ROWS
                        FETCH NEXT @PageSize ROWS ONLY;
                        SELECT Count(*) FROM Products;";

            using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
            {
                using (var multi = connection.QueryMultiple(sqlCommand, parameters, null, null, CommandType.Text))
                {
                    var result = multi.Read<dynamic>().ToList();
                    var count = multi.Read<int>().FirstOrDefault();

                    return new Tuple<IList<dynamic>, int>(result, count);
                }
            }
        }

        public List<dynamic> GetProductListFiltered(bool? discontinued, int? supplierId, int? categoryId)
        {
            object parameters = new
            {
                Discontinued = discontinued,
                SupplierID = supplierId,
                CategoryID = categoryId
            };

            var sqlCommand = $@" SELECT *
                                FROM Products
                                WHERE
                                (@Discontinued IS NULL OR Discontinued = @Discontinued) AND
                                (@SupplierID IS NULL OR SupplierID = @SupplierID) AND
                                (@CategoryID IS NULL OR CategoryID = @CategoryID)";
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                    return connection.Query<dynamic>(sqlCommand, parameters, null, true, null, CommandType.Text);
            }).Result.ToList();
        }

        public List<dynamic> GetCustomerDetailByOrder(int? orderId)
        {
            object parameters = new
            {
                OrderID = orderId
            };

            var sqlCommand = $@" SELECT c.*
                                FROM Customers c
                                INNER JOIN Orders o ON o.CustomerID = c.CustomerID
                                WHERE
                                o.OrderID = @OrderID";
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                    return connection.Query<dynamic>(sqlCommand, parameters, null, true, null, CommandType.Text);
            }).Result.ToList();
        }

        public List<dynamic> GetOrderDetailByOrder(int? orderId)
        {
            object parameters = new
            {
                OrderID = orderId
            };

            var sqlCommand = $@" SELECT od.*, p.ProductName
                                FROM [Order Details] od
                                INNER JOIN Products p ON od.ProductID = p.ProductID
                                WHERE
                                od.OrderID = @OrderID";
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                    return connection.Query<dynamic>(sqlCommand, parameters, null, true, null, CommandType.Text);
            }).Result.ToList();
        }





        #region Non binded data

        public List<dynamic> GetSuppliers()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                    return connection.Query<dynamic>($@" SELECT * FROM Suppliers");
            }).Result.ToList();
        }

        public List<dynamic> GetCategories()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                    return connection.Query<dynamic>($@" SELECT * FROM Categories");
            }).Result.ToList();
        }

        public List<dynamic> GetOrders()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.LocalDb))
                    return connection.Query<dynamic>($@" SELECT * FROM Orders");
            }).Result.ToList();
        }

        #endregion Non binded data
    }
}
