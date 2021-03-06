﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.WebRole.Domain.Security
{
    [Table("ROLE_GROUP")]
    public class RoleGroup : Entity
    {
        [Key] [Column("ID")] public int Id { get; set; }

        [Column("NAME")]
        [StringLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}