﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Northwind.WebRole.Dtos
{
    [DataContract]
    public class UserTypeDto : SerializableDynamicObject
    {
        [DataMember] public byte Id { get; set; }

        [DataMember]
        [Required]
        [StringLength(25)]
        public string TypeName { get; set; }
    }
}