using System;
using System.Collections.Generic;
using ByteNuts.NetCoreControls.Samples.Controls.Dapper.Models.Db;

namespace ByteNuts.NetCoreControls.Samples.Controls.Dapper.Data
{
    public interface IDataAccess
    {
        List<ProductModel> GetProductList();
        void UpdateProduct(ProductModel model);
        Tuple<IList<dynamic>, int> GetProductListPaginated(int pageNumber, int pageSize);
        List<dynamic> GetProductListFiltered(bool? discontinued, int? supplierId, int? categoryId);
        List<dynamic> GetCustomerDetailByOrder(int? orderId);
        List<dynamic> GetOrderDetailByOrder(int? orderId);


        #region Non binded data

        List<SupplierModel> GetSuppliers();

        List<CategoryModel> GetCategories();

        List<dynamic> GetOrders();

        #endregion Non binded data

    }
}
