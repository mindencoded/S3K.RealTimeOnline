﻿using System.ServiceModel;
using Northwind.WebRole.Dtos;

namespace Northwind.WebRole.Contracts.Maintenances
{
    [ServiceContract]
    public interface IUserTypeService : IMaintenanceService<UserTypeDto>
    {
    }
}