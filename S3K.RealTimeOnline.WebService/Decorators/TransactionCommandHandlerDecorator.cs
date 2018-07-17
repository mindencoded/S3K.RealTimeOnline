﻿using System.Threading.Tasks;

namespace S3K.RealTimeOnline.WebService.Decorators
{
    public class TransactionCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _decorated;

        public TransactionCommandHandlerDecorator(ICommandHandler<TCommand> decorated)
        {
            _decorated = decorated;
        }

        public void Handle(TCommand command)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                _decorated.Handle(command);
                scope.Complete();
            }
        }

        public async Task HandleAsync(TCommand command)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                await _decorated.HandleAsync(command);
                scope.Complete();
            }
        }
    }
}