using ByteNuts.NetCoreControls.Core.Extensions;
using ByteNuts.NetCoreControls.Core.Models;
using ByteNuts.NetCoreControls.Models.Grid;
using ByteNuts.NetCoreControls.Models.Select;
using ByteNuts.NetCoreControls.Samples.Dapper.Data;
using Microsoft.AspNetCore.Mvc;

namespace ByteNuts.NetCoreControls.Samples.Dapper.Controllers
{
    public class NccSelectController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DropDownList()
        {
            var context = new NccSelectContext
            {
                Id = "DropDownList1",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetSuppliers",
                TextValue = "CompanyName",
                DataValue = "SupplierID",
                FirstItem = "--- Please select ---",
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccSelect/Partials/_DropDownList1.cshtml" }
            };

            ViewData[context.Id] = context;

            var context2 = new NccSelectContext
            {
                Id = "DropDownList2",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetCategoriesBySupplier",
                SelectParameters = new { supplierId = (int?)null }.NccToExpando(),
                TextValue = "CategoryName",
                DataValue = "CategoryID",
                FirstItem = "--- Please select ---",
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccSelect/Partials/_DropDownList2.cshtml" }
            };

            ViewData[context2.Id] = context2;

            var context3 = new NccSelectContext
            {
                Id = "DropDownList3",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetProductsByCategoryAndSupplier",
                SelectParameters = new { supplierId = (int?)null, categoryId = (int?)null }.NccToExpando(),
                TextValue = "ProductName",
                DataValue = "ProductID",
                FirstItem = "--- Please select ---",
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccSelect/Partials/_DropDownList3.cshtml" }
            };

            ViewData[context3.Id] = context3;
            return View();
        }

        public IActionResult DifferentControls()
        {
            var context = new NccSelectContext
            {
                Id = "DifferentControls1",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetCustomers",
                TextValue = "CompanyName",
                DataValue = "CustomerID",
                FirstItem = "--- Please select ---",
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccSelect/Partials/_DifferentControls1.cshtml" }
            };

            ViewData[context.Id] = context;

            var context2 = new NccSelectContext
            {
                Id = "DifferentControls2",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetOrderByCustomer",
                SelectParameters = new { customerId = (int?)null }.NccToExpando(),
                TextValue = "OrderDate",
                DataValue = "OrderID",
                FirstItem = "--- Please select ---",
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccSelect/Partials/_DifferentControls2.cshtml" }
            };

            ViewData[context2.Id] = context2;

            var context3 = new NccGridContext
            {
                Id = "DifferentControls3",
                DataAccessClass = typeof(IDataAccess).AssemblyQualifiedName,
                SelectMethod = "GetOrderDetailByOrder",
                SelectParameters = new { customerId = (int?)null, orderId = (int?)null }.NccToExpando(),
                ViewPaths = new ViewsPathsModel { ViewPath = "/Views/NccSelect/Partials/_DifferentControls3.cshtml" }
            };

            ViewData[context3.Id] = context3;
            return View();
        }

    }
}
