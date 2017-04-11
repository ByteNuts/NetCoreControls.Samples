using ByteNuts.NetCoreControls.Samples.Ef.Data.Northwind;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteNuts.NetCoreControls.Samples.Ef.Data
{
    public interface IDataAccess
    {
        IQueryable<Products> GetProductList();
        void UpdateProduct(Products model);
        Tuple<IQueryable<Products>, int> GetProductListPaginated(int pageNumber, int pageSize);
        IQueryable<Products> GetProductListFiltered(bool? discontinued, int? supplierId, int? categoryId);
        IQueryable<Customers> GetCustomerDetailByOrder(int? orderId);
        IQueryable<dynamic> GetOrderDetailByOrder(int? orderId);


        IQueryable<Suppliers> GetSuppliers();

        IQueryable<Categories> GetCategories();

        IQueryable<Categories> GetCategoriesBySupplier(int? supplierId);

        IQueryable<Products> GetProductsByCategoryAndSupplier(int? supplierId, int? categoryId);

        IQueryable<Orders> GetOrders();

        IQueryable<Customers> GetCustomers();

        IQueryable<Orders> GetOrderByCustomer(string customerId);

    }
}
