﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3K.RealTimeOnline.BusinessDomain
{
    [Table("CUSTOMER")]
    public class Customer : Person
    {
        public virtual ICollection<Order> Orders { get; set; }
    }
}