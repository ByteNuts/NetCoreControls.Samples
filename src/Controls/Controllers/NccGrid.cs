using ByteNuts.NetCoreControls.Models;
using ByteNuts.NetCoreControls.Models.Grid;
using ByteNuts.NetCoreControls.Samples.Controls.NccEvents;
using ByteNuts.NetCoreControls.Samples.DapperSource;
using ByteNuts.NetCoreControls.Services;
using Microsoft.AspNetCore.Mvc;


namespace ByteNuts.NetCoreControls.Samples.Controls.Controllers
{
    public class NccGrid : Controller
    {
        private IDataAccess DataAccess { get; }
        public NccGrid(IDataAccess dataAccess)
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
                UseDependencyInjection = true,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/SimpleGrid.cshtml"}
            };

            ViewData[context.Id] = context;

            return View();
        }

        public IActionResult SimpleGridWithClientPaging()
        {
            var context = new NccGridContext
            {
                Id = "SimpleGridWithClientPaging",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductList",
                UseDependencyInjection = true,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/SimpleGridWithClientPaging.cshtml" }
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
                UseDependencyInjection = true,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/SimpleGridWithServerPaging.cshtml" }
            };

            ViewData[context.Id] = context;

            return View();
        }


        public IActionResult GridWithFilter()
        {
            var context = new NccGridContext
            {
                Id = "GridWithFilter1",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductListFiltered",
                SelectParameters = new { discontinued = (bool?)null, supplierId = (int?)null, categoryId = (int?)null }.NccToExpando(),
                UseDependencyInjection = true,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_GridWithFilter1.cshtml" }
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
                SelectParameters = new { orderId = 10249 }.NccToExpando(),
                UseDependencyInjection = true,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_MultiGridWithFilter1.cshtml" }
            };

            ViewData[context1.Id] = context1;

            var context2 = new NccGridContext
            {
                Id = "MultiGridWithFilter2",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetOrderDetailByOrder",
                SelectParameters = new { orderId = 10249 }.NccToExpando(),
                UseDependencyInjection = true,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/Partials/_MultiGridWithFilter2.cshtml" }
            };

            ViewData[context2.Id] = context2;

            ViewData["Orders"] = DataAccess.GetOrders();

            return View();
        }

        public IActionResult EditableGrid()
        {
            var context = new NccGridContext
            {
                Id = "EditableGrid",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductList",
                UseDependencyInjection = true,
                EventHandlerClass = typeof(EditableGridEvents).AssemblyQualifiedName,
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccGrid/EditableGrid.cshtml" }
            };

            ViewData[context.Id] = context;

            return View();
        }

    }
}
