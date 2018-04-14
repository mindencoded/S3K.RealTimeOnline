﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S3K.RealTimeOnline.GenericDataAccess.Repositories;
using S3K.RealTimeOnline.GenericDataAccess.Tools;
using S3K.RealTimeOnline.SecurityDataAccess.UnitOfWork;
using S3K.RealTimeOnline.SecurityDomain;

namespace S3K.RealTimeOnline.SecurityDataAccess.QueryHandlers.VerifyUsernamePassword
{
    class VerifyUsernamePasswordQueryHandler : IQueryHandler<VerifyUsernamePasswordQuery, bool>
    {
        private readonly ISecurityUnitOfWork _unitOfWork;

        public VerifyUsernamePasswordQueryHandler(ISecurityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Handle(VerifyUsernamePasswordQuery query)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"Username", query.Username},
                {"Password", query.Password},
                {"Active", query.Active}
            };

            using (_unitOfWork)
            {
                _unitOfWork.Open();
                IRepository<User> userRepository = _unitOfWork.Repository<User>();
                User user = userRepository.Select(parameters).FirstOrDefault();
                return user != null;
            }
        }

        public async Task<bool> HandleAsync(VerifyUsernamePasswordQuery query)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"Username", query.Username},
                {"Password", query.Password},
                {"Active", query.Active}
            };

            using (_unitOfWork)
            {
                await _unitOfWork.OpenAsync();
                IRepository<User> userRepository = _unitOfWork.Repository<User>();
                IEnumerable<User> user = await userRepository.SelectAsync(parameters);
                return user.Any();
            }
        }
    }
}