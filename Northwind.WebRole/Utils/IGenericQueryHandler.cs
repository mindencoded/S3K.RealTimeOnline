﻿using System.Threading.Tasks;

namespace Northwind.WebRole.Utils
{
    public interface IGenericQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle<TEntity>(TQuery query) where TEntity : class;

        Task<TResult> HandleAsync<TEntity>(TQuery query) where TEntity : class;
    }
}