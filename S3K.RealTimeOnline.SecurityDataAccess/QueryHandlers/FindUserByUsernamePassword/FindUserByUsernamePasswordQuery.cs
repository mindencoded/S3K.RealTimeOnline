﻿using System.ComponentModel.DataAnnotations;
using S3K.RealTimeOnline.GenericDataAccess.Tools;
using S3K.RealTimeOnline.SecurityDomain;

namespace S3K.RealTimeOnline.SecurityDataAccess.QueryHandlers.FindUserByUsernamePassword
{
    public class FindUserByUsernamePasswordQuery : IQuery<User>
    {
        public FindUserByUsernamePasswordQuery()
        {
            Active = true;
        }

        [Required] [StringLength(50)] public string Username { get; set; }

        [Required] [StringLength(32)] public string Password { get; set; }

        public bool Active { get; set; }
    }
}