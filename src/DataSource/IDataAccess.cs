using System;
using System.Collections.Generic;
using ByteNuts.NetCoreControls.Samples.Models.Db;

namespace ByteNuts.NetCoreControls.Samples.DapperSource
{
    public interface IDataAccess
    {
        List<dynamic> GetProductList();
        void UpdateProduct(ProductModel model);
        Tuple<IList<dynamic>, int> GetProductListPaginated(int pageNumber, int pageSize);
        List<dynamic> GetProductListFiltered(bool? discontinued, int? supplierId, int? categoryId);
        List<dynamic> GetCustomerDetailByOrder(int? orderId);
        List<dynamic> GetOrderDetailByOrder(int? orderId);


        #region Non binded data

        List<dynamic> GetSuppliers();

        List<dynamic> GetCategories();

        List<dynamic> GetOrders();

        #endregion Non binded data

    }
}
