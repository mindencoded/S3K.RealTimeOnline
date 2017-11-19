﻿using S3K.RealTimeOnline.BusinessDomain;
using S3K.RealTimeOnline.GenericDataAccess.Repositories;

namespace S3K.RealTimeOnline.BusinessDataAccess.Repositories.Contracts
{
    public interface IInventoryTransactionRepository : IRepository<InventoryTransaction>
    {
    }
}
