﻿using Northwind.WebRole.Domain.Business;

namespace Northwind.WebRole.Repositories
{
    public interface IShipperRepository : ICommandRepository<Shipper>
    {
    }
}