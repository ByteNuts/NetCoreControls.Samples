using System;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using ByteNuts.NetCoreControls.Samples.Controls.Dapper.Models;
using ByteNuts.NetCoreControls.Samples.Controls.Dapper.Models.Db;

namespace ByteNuts.NetCoreControls.Samples.Controls.Dapper.Data
{
    public class DapperDataSource : IDataAccess
    {
        public IOptions<ConnectionStringsModel> _connStrings { get; set; }
        public DapperDataSource(IOptions<ConnectionStringsModel> connStrings)
        {
            _connStrings = connStrings;

        }
        public List<ProductModel> GetProductList()
        {
            var sqlCommand = $@" SELECT * FROM Products";
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<ProductModel>(sqlCommand);
            }).Result.AsList();
        }

        public void UpdateProduct(ProductModel model)
        {
            object parameters = new
            {
                ProductID = model.ProductID,
                ProductName = model.ProductName,
                SupplierID = model.SupplierID,
                CategoryID = model.CategoryID,
                QuantityPerUnit = model.QuantityPerUnit,
                UnitPrice = model.UnitPrice,
                UnitsInStock = model.UnitsInStock,
                UnitsOnOrder = model.UnitsOnOrder,
                ReorderLevel = model.ReorderLevel,
                Discontinued = model.Discontinued
            };
            var sqlCommand = $@" UPDATE p
                                SET p.ProductName = @ProductName,
                                    p.SupplierID = @SupplierID,
                                    p.CategoryID = @CategoryID,
                                    p.QuantityPerUnit = @QuantityPerUnit,
                                    p.UnitPrice = @UnitPrice,
                                    p.UnitsInStock = @UnitsInStock,
                                    p.UnitsOnOrder = @UnitsOnOrder,
                                    p.ReorderLevel = @ReorderLevel,
                                    p.Discontinued = @Discontinued
                                FROM Products p
                                WHERE p.ProductID = @ProductID";

            using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
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

            using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
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
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
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
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<dynamic>(sqlCommand, parameters, null, true, null, CommandType.Text);
            }).Result.ToList();
        }

        public List<dynamic> GetOrderDetailByOrder(int? orderId)
        {
            Thread.Sleep(2000);
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
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<dynamic>(sqlCommand, parameters, null, true, null, CommandType.Text);
            }).Result.ToList();
        }




        public List<SupplierModel> GetSuppliers()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<SupplierModel>($@" SELECT * FROM Suppliers");
            }).Result.ToList();
        }

        public List<CategoryModel> GetCategories()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<CategoryModel>($@" SELECT * FROM Categories");
            }).Result.ToList();
        }
        public List<CategoryModel> GetCategoriesBySupplier(int? supplierId)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<CategoryModel>($@" SELECT * FROM Categories INNER JOIN Suppliers ON SupplierID = {supplierId ?? -1}");
            }).Result.ToList();
        }


        public List<ProductModel> GetProductsByCategoryAndSupplier(int? supplierId, int? categoryId)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<ProductModel>($@" SELECT * FROM Products p WHERE p.CategoryID = {categoryId ?? -1} AND p.SupplierID = {supplierId ?? -1}");
            }).Result.ToList();
        }


        public List<dynamic> GetOrders()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<dynamic>($@" SELECT * FROM Orders");
            }).Result.ToList();
        }


        public List<dynamic> GetCustomers()
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<dynamic>($@" SELECT * FROM Customers");
            }).Result.ToList();
        }

        public List<dynamic> GetOrderByCustomer(string customerId)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var connection = new SqlConnection(_connStrings.Value.DefaultConnection))
                    return connection.Query<dynamic>($@" SELECT * FROM Orders o WHERE o.CustomerID = {(string.IsNullOrEmpty(customerId) ? "NULL" : "'" + customerId + "'")}");
            }).Result.ToList();
        }

    }
}
