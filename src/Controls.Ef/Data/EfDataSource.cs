using System;
using System.Collections.Generic;
using System.Linq;
using ByteNuts.NetCoreControls.Samples.Controls.Ef.Data.Northwind;

namespace ByteNuts.NetCoreControls.Samples.Controls.Ef.Data
{
    public class EfDataSource : IDataAccess
    {
        protected readonly NorthwindContext _dbContext;
        public EfDataSource(NorthwindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Products> GetProductList()
        {
            return _dbContext.Products;
        }

        public void UpdateProduct(Products model)
        {
            var product = _dbContext.Products.Find(model.ProductId);
            if (product != null)
            {
                _dbContext.Entry(product).CurrentValues.SetValues(model);
                _dbContext.SaveChanges();
            }
        }

        public Tuple<IQueryable<Products>, int>  GetProductListPaginated(int pageNumber, int pageSize)
        {
            return new Tuple<IQueryable<Products>, int>(_dbContext.Products.Skip(pageSize * (pageNumber - 1)).Take(pageSize), _dbContext.Products.Count());
        }

        public IQueryable<Products> GetProductListFiltered(bool? discontinued, int? supplierId, int? categoryId)
        {
            return from u in _dbContext.Products
                        where (!discontinued.HasValue || u.Discontinued == discontinued.Value) &&
                        (!supplierId.HasValue || (supplierId.Value > 0 && u.SupplierId == supplierId)) &&
                        (!categoryId.HasValue || (categoryId.Value > 0 && u.CategoryId == categoryId))
                        select u;
        }

        public IQueryable<Customers> GetCustomerDetailByOrder(int? orderId)
        {
            return from u in _dbContext.Orders
                where orderId.HasValue && u.OrderId == orderId.Value
                select u.Customer;
        }

        public IQueryable<dynamic> GetOrderDetailByOrder(int? orderId)
        {
            return from u in _dbContext.OrderDetails
                where orderId.HasValue && u.OrderId == orderId.Value
                select new  
                {
                    OrderId = u.OrderId,
                    ProductId = u.ProductId,
                    ProductName = u.Product.ProductName,
                    UnitPrice = u.UnitPrice,
                    Quantity = u.Quantity,
                    Discount = u.Discount
                };
        }





        #region Non binded data

        public IQueryable<Suppliers> GetSuppliers()
        {
            return _dbContext.Suppliers;
        }

        public IQueryable<Categories> GetCategories()
        {
            return _dbContext.Categories;
        }

        public IQueryable<Orders> GetOrders()
        {
            return _dbContext.Orders;
        }

        #endregion Non binded data
    }
}
