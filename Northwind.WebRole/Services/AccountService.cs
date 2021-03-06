﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Northwind.WebRole.Contracts;
using Northwind.WebRole.Decorators;
using Northwind.WebRole.Domain.Security;
using Northwind.WebRole.Dtos;
using Northwind.WebRole.Queries;
using Northwind.WebRole.Security;
using Northwind.WebRole.Utils;
using Unity;

namespace Northwind.WebRole.Services
{
    [RoutePrefix("Account")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AccountService : WebHttpService, IAccountService
    {
        public AccountService(IUnityContainer container) : base(container)
        {
        }

        public Stream Login(LoginDto login)
        {
            try
            {
                if (login == null)
                {
                    throw new ArgumentNullException("login");
                }

                UriTemplateMatch uriTemplateMatch =
                    (UriTemplateMatch) OperationContext.Current.IncomingMessageProperties["UriTemplateMatchResults"];
                string host = uriTemplateMatch.BaseUri.Host;
                string username = login.Username;
                string password = Md5Hash.Create(login.Password);
                VerifyUsernamePasswordQuery verifyUsernamePasswordQuery =
                    new VerifyUsernamePasswordQuery
                    {
                        Username = username,
                        Password = password
                    };

                IQueryHandler<VerifyUsernamePasswordQuery, bool> verifyUsernamePasswordQueryHandler =
                    Container.Resolve<IQueryHandler<VerifyUsernamePasswordQuery, bool>>(HandlerDecoratorType
                        .ValidationCommand.ToString());
                if (verifyUsernamePasswordQueryHandler.Handle(verifyUsernamePasswordQuery))
                {
                    SelectRolesByUserNameQuery selectRolesByUserNameQuery = new SelectRolesByUserNameQuery
                    {
                        Username = username
                    };
                    IQueryHandler<SelectRolesByUserNameQuery, IEnumerable<Role>> selectRolesByUserNameQueryHandler =
                        Container.Resolve<IQueryHandler<SelectRolesByUserNameQuery, IEnumerable<Role>>>();
                    string[] roles = selectRolesByUserNameQueryHandler.Handle(selectRolesByUserNameQuery)
                        .Select(r => r.Name).ToArray();
                    string token;
                    if (AppConfig.EncryptionAlgorithm == "RSA")
                    {
                        RSACryptoServiceProvider rsa = RsaStore.GetServiceProvider("Northwind");
                        token = JwtRsaGenerator.Encode(rsa, username, null, roles, host, host,
                            AppConfig.TokenExpirationMinutes);
                    }
                    else if (AppConfig.EncryptionAlgorithm == "HMAC")
                    {
                        byte[] symmetricKey = HmacStore.GetSecretKey("Northwind");
                        token = JwtHmacGenerator.Encode(symmetricKey, username, null, roles, host, host,
                            AppConfig.TokenExpirationMinutes);
                    }
                    else
                    {
                        throw new Exception("The encryption algorithm is not recognized.");
                    }

                    string data = DataToString(new {JWTTOKEN = token});
                    return CreateStreamResponse(data);
                }

                throw new WebFaultException(HttpStatusCode.Unauthorized);
                //throw new FaultException<ErrorMessage>(new ErrorMessage("Unauthorized"), new FaultReason("Unauthorized"));
                //throw new FaultException(new FaultReason("Unauthorized"), new FaultCode("Sender", new FaultCode("Unauthorized")));
            }
            catch (ValidationException ex)
            {
                throw new WebFaultException<ErrorMessage>(new ErrorMessage(ex), HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<ErrorMessage>(new ErrorMessage(ex), HttpStatusCode.InternalServerError);
            }
        }

        public static void Configure(ServiceConfiguration config)
        {
            Configure<IAccountService>(config, "");
        }
    }
}