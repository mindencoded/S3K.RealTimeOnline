﻿using System.Threading.Tasks;
using S3K.RealTimeOnline.GenericDataAccess.Tools;
using S3K.RealTimeOnline.GenericDataAccess.UnitOfWork;

namespace S3K.RealTimeOnline.GenericDataAccess.CommandHandlers
{
    public class GenericDeleteCommandHandler<TUnitOfWork, TEntity> : ICommandHandler<object>
        where TEntity : class where TUnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericDeleteCommandHandler(TUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Handle(object command)
        {
            using (_unitOfWork)
            {
                _unitOfWork.Open();
                _unitOfWork.Repository<TEntity>().Delete(command);
            }
        }

        public async Task HandleAsync(object command)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.OpenAsync();
                await _unitOfWork.Repository<TEntity>().DeleteAsync(command);
            }
        }
    }
}