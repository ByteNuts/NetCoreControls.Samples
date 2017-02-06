using System;
using System.Collections.Generic;

namespace ByteNuts.NetCoreControls.Samples.Controls.Ef.Data.Northwind
{
    public partial class EmployeeTerritories
    {
        public int EmployeeId { get; set; }
        public string TerritoryId { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Territories Territory { get; set; }
    }
}
