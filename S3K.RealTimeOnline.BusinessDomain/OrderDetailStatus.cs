﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using S3K.RealTimeOnline.GenericDomain;

namespace S3K.RealTimeOnline.BusinessDomain
{
    [Table("ORDER_DETAIL_STATUS")]
    public class OrderDetailStatus : Entity
    {
        [Key] [Column("ID")] public byte Id { get; set; }

        [Column("STATUS_NAME")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string StatusName { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}