using System.Collections.Generic;
using ByteNuts.NetCoreControls.Models;
using ByteNuts.NetCoreControls.Models.Grid;
using ByteNuts.NetCoreControls.Samples.Controls.Dapper.Data;
using ByteNuts.NetCoreControls.Samples.Controls.Dapper.NccEvents;
using ByteNuts.NetCoreControls.Services;
using Microsoft.AspNetCore.Mvc;


namespace ByteNuts.NetCoreControls.Samples.Controls.Dapper.Controllers
{
    public class NccGridController : Controller
    {
        private IDataAccess DataAccess { get; }
        public NccGridController(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleGrid()
        {
            var context = new NccGridContext
            {
                Id = "SimpleGrid",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductList",
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/SimpleGrid.cshtml"}
            };

            ViewData[context.Id] = context;

            return View();
        }

        public IActionResult SimpleGridWithServerPagingNoBind()
        {
            var context = new NccGridContext
            {
                Id = "SimpleGridWithServerPagingNoBind",
                AutoBind = false,
                DataSource = DataAccess.GetProductList(),
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_SimpleGridWithServerPagingNoBind.cshtml" }
            };

            ViewData[context.Id] = context;

            return View();
        }

        public IActionResult SimpleGridWithServerPaging()
        {
            var context = new NccGridContext
            {
                Id = "SimpleGridWithServerPaging",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductListPaginated",
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_SimpleGridWithServerPaging.cshtml" }
            };

            ViewData[context.Id] = context;

            return View();
        }


        public IActionResult GridWithFilter()
        {
            var context = new NccGridContext
            {
                Id = "GridWithFilter",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductListFiltered",
                SelectParameters = new { discontinued = (bool?)null, supplierId = (int?)null, categoryId = (int?)null }.NccToExpando(),
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_GridWithFilter.cshtml" }
            };

            ViewData[context.Id] = context;

            ViewData["Suppliers"] = DataAccess.GetSuppliers();
            ViewData["Categories"] = DataAccess.GetCategories();

            return View();
        }

        public IActionResult MultiGridWithFilter()
        {
            var context1 = new NccGridContext
            {
                Id = "MultiGridWithFilter1",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetCustomerDetailByOrder",
                SelectParameters = new { orderId = (int?)null }.NccToExpando(),
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_MultiGridWithFilter1.cshtml" }
            };

            ViewData[context1.Id] = context1;

            var context2 = new NccGridContext
            {
                Id = "MultiGridWithFilter2",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetOrderDetailByOrder",
                SelectParameters = new { orderId = (int?)null }.NccToExpando(),
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_MultiGridWithFilter2.cshtml" }
            };

            ViewData[context2.Id] = context2;

            ViewData["Orders"] = DataAccess.GetOrders();

            return View();
        }

        public IActionResult EditableGrid()
        {
            var suppliers = DataAccess.GetSuppliers();
            var categories = DataAccess.GetCategories();

            var context = new NccGridContext
            {
                Id = "EditableGrid",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductList",
                UpdateMethod = "UpdateProduct",
                AutoGenerateEditButton = true,
                DatabaseModelType = typeof(Models.Db.ProductModel).AssemblyQualifiedName,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_EditableGrid.cshtml" },
                AdditionalData = new Dictionary<string, object>
                {
                    { "Suppliers", suppliers },
                    { "Categories", categories }
                }
            };

            ViewData[context.Id] = context;

            return View();
        }

        public IActionResult CustomEditableGrid()
        {
            var suppliers = DataAccess.GetSuppliers();
            var categories = DataAccess.GetCategories();

            var context = new NccGridContext
            {
                Id = "CustomEditableGrid",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductList",
                EventHandlerClass = typeof(CustomEditableGridEvents).AssemblyQualifiedName,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_CustomEditableGrid.cshtml" },
                AdditionalData = new Dictionary<string, object>
                {
                    { "Suppliers", suppliers },
                    { "Categories", categories }
                }
            };

            ViewData[context.Id] = context;

            return View();
        }

    }
}
