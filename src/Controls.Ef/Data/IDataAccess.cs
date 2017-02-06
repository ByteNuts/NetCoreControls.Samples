using ByteNuts.NetCoreControls.Samples.Controls.Ef.Data.Northwind;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ByteNuts.NetCoreControls.Samples.Controls.Ef.Data
{
    public interface IDataAccess
    {
        IQueryable<Products> GetProductList();
        void UpdateProduct(Products model);
        Tuple<IQueryable<Products>, int> GetProductListPaginated(int pageNumber, int pageSize);
        IQueryable<Products> GetProductListFiltered(bool? discontinued, int? supplierId, int? categoryId);
        IQueryable<Customers> GetCustomerDetailByOrder(int? orderId);
        IQueryable<dynamic> GetOrderDetailByOrder(int? orderId);


        #region Non binded data

        IQueryable<Suppliers> GetSuppliers();

        IQueryable<Categories> GetCategories();

        IQueryable<Orders> GetOrders();

        #endregion Non binded data

    }
}
