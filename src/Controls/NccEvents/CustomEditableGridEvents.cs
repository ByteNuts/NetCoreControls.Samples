using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByteNuts.NetCoreControls.Controls.Grid.Events;
using ByteNuts.NetCoreControls.Samples.DapperSource;
using ByteNuts.NetCoreControls.Models;
using ByteNuts.NetCoreControls.Models.Grid;
using ByteNuts.NetCoreControls.Samples.Models.Db;
using ByteNuts.NetCoreControls.Extensions;
using ByteNuts.NetCoreControls.Services;

namespace ByteNuts.NetCoreControls.Samples.Controls.NccEvents
{
    public class CustomEditableGridEvents : NccGridEvents
    {
        public override async void Update(NccEventArgs eventArgs)
        {
            base.Update(eventArgs);

            try
            {
                var dataAccess = (IDataAccess) eventArgs.Controller.HttpContext.RequestServices.GetService(typeof(IDataAccess));

                var gridContext = (NccGridContext) eventArgs.NccControlContext;
                if (gridContext == null) throw new Exception("Could not obtain NccGridContext to process Update. Wrong control type?");

                var model = new List<ProductModel>();

                var ok = await EventService.NccBindModel(eventArgs.Controller, model, gridContext.DataKeysValues, gridContext.Id);
                if (!ok) throw new Exception("Could not bind any model from FormCollection. Is RenderForm=\"true\" attribute present on grid element?");

                foreach (var product in model)
                {
                    dataAccess.UpdateProduct(product);
                }

                eventArgs.Controller.ViewBag.Message = "All grid products updated successfully!";
                eventArgs.Controller.ViewBag.MessageType = "success";
            }
            catch (Exception e)
            {
                eventArgs.Controller.ViewBag.Message = "An error occured updating products info!";
                eventArgs.Controller.ViewBag.MessageType = "danger";
            }
        }


        public override async void UpdateRow(NccEventArgs eventArgs, int rowPos)
        {
            try
            {
                var dataAccess = (IDataAccess)eventArgs.Controller.HttpContext.RequestServices.GetService(typeof(IDataAccess));

                var gridContext = (NccGridContext)eventArgs.NccControlContext;
                if (gridContext == null) throw new Exception("Could not obtain NccGridContext to process Update. Wrong control type?");

                var model = new List<ProductModel>();

                var ok = await EventService.NccBindModel(eventArgs.Controller, model, gridContext.DataKeysValues, gridContext.Id);
                if (!ok) throw new Exception("Could not bind any model from FormCollection. Is RenderForm=\"true\" attribute present on grid element?");

                dataAccess.UpdateProduct(model[rowPos]);

                eventArgs.Controller.ViewBag.Message = "Product info updated successfully!";
                eventArgs.Controller.ViewBag.MessageType = "success";
            }
            catch (Exception e)
            {
                eventArgs.Controller.ViewBag.Message = "An error occured updating product info!";
                eventArgs.Controller.ViewBag.MessageType = "danger";
            }
        }
    }
}
