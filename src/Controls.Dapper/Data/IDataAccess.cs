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



        List<SupplierModel> GetSuppliers();

        List<CategoryModel> GetCategories();

        List<CategoryModel> GetCategoriesBySupplier(int? supplierId);

        List<ProductModel> GetProductsByCategoryAndSupplier(int? supplierId, int? categoryId);

        List<dynamic> GetOrders();

        List<dynamic> GetCustomers();

        List<dynamic> GetOrderByCustomer(string customerId);

    }
}
