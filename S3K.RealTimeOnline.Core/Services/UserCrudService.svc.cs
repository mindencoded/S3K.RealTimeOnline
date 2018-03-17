﻿
using S3K.RealTimeOnline.Contracts.Services;
using S3K.RealTimeOnline.Dtos;
using S3K.RealTimeOnline.SecurityDataAccess.UnitOfWork;
using S3K.RealTimeOnline.SecurityDomain;
using Unity;

namespace S3K.RealTimeOnline.Core.Services
{
    public class UserCrudService : CrudService<ISecurityUnitOfWork, User, UserDto>, IUserCrudService
    {
        public UserCrudService(IUnityContainer container) : base(container)
        {
        }
    }
}
