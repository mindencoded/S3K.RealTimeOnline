﻿using System.Threading.Tasks;

namespace S3K.RealTimeOnline.GenericDataAccess.Tools
{
    public interface ICommandHandlerAsync<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}