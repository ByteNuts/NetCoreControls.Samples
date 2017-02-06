using System;
using System.Collections.Generic;
using ByteNuts.NetCoreControls.Controls.Grid.Events;
using ByteNuts.NetCoreControls.Models;
using ByteNuts.NetCoreControls.Models.Grid;
using ByteNuts.NetCoreControls.Services;
using ByteNuts.NetCoreControls.Samples.Controls.Dapper.Data;
using ByteNuts.NetCoreControls.Samples.Controls.Dapper.Models.Db;

namespace ByteNuts.NetCoreControls.Samples.Controls.Dapper.NccEvents
{
    public class CustomEditableGridEvents : NccGridEvents
    {
        public override async void Update(NccEventArgs eventArgs)
        {
            try
            {
                var dataAccess = (IDataAccess) eventArgs.Controller.HttpContext.RequestServices.GetService(typeof(IDataAccess));

                var gridContext = (NccGridContext) eventArgs.NccControlContext;
                if (gridContext == null) throw new Exception("Could not obtain NccGridContext to process Update. Wrong control type?");

                var model = new List<ProductModel>();

                var ok = await eventArgs.Controller.TryUpdateModelAsync(model, gridContext.Id);
                if (!ok) throw new Exception("Error binding model to object or list");

                ok = EventService.NccBindDataKeys(model, gridContext.DataKeysValues);
                if (!ok) throw new Exception("DataKeys list is bigger than submited list. No match possible!!");

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

                var ok = await eventArgs.Controller.TryUpdateModelAsync(model, gridContext.Id);
                if (!ok) throw new Exception("Error binding model to object or list");

                ok = EventService.NccBindDataKeys(model, gridContext.DataKeysValues);
                if (!ok) throw new Exception("DataKeys list is bigger than submited list. No match possible!!");

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
