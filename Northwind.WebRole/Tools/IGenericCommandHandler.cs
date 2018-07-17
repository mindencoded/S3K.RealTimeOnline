﻿using System.Threading.Tasks;

namespace Northwind.WebRole.Tools
{
    public interface IGenericCommandHandler
    {
        void Handle<TEntity>(object command) where TEntity : class;

        Task HandleAsync<TEntity>(object command) where TEntity : class;
    }
}