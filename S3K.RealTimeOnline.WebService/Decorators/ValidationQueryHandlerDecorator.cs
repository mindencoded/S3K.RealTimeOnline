﻿using System.Threading.Tasks;
using S3K.RealTimeOnline.CommonUtils;

namespace S3K.RealTimeOnline.WebService.Decorators
{
    public class ValidationQueryHandlerDecorator<TQuery, TResult>
        : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decorated;

        public ValidationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated)
        {
            _decorated = decorated;
        }

        public TResult Handle(TQuery query)
        {
            ValidationHelper.ValidateObject(query);
            return _decorated.Handle(query);
        }

        public async Task<TResult> HandleAsync(TQuery query)
        {
            ValidationHelper.ValidateObject(query);
            return await _decorated.HandleAsync(query);
        }
    }
}