using System;
using System.Collections.Generic;
using ByteNuts.NetCoreControls.Samples.Models.Db;

namespace ByteNuts.NetCoreControls.Samples.DapperSource
{
    public class EfDataSource : IDataAccess
    {

        public List<CategoryModel> GetCategories()
        {
            throw new NotImplementedException();
        }

        public List<dynamic> GetCustomerDetailByOrder(int? orderId)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> GetOrderDetailByOrder(int? orderId)
        {
            throw new NotImplementedException();
        }

        public List<dynamic> GetOrders()
        {
            throw new NotImplementedException();
        }

        public List<ProductModel> GetProductList()
        {
            throw new NotImplementedException();
        }

        public List<dynamic> GetProductListFiltered(bool? discontinued, int? supplierId, int? categoryId)
        {
            throw new NotImplementedException();
        }

        public Tuple<IList<dynamic>, int> GetProductListPaginated(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<SupplierModel> GetSuppliers()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(ProductModel model)
        {
            throw new NotImplementedException();
        }
    }
}
