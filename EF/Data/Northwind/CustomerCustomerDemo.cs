﻿using System;
using System.Collections.Generic;

namespace ByteNuts.NetCoreControls.Samples.Ef.Data.Northwind
{
    public partial class CustomerCustomerDemo
    {
        public string CustomerId { get; set; }
        public string CustomerTypeId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual CustomerDemographics CustomerType { get; set; }
    }
}
