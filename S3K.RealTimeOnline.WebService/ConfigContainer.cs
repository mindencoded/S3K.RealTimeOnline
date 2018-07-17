﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using S3K.RealTimeOnline.CommonUtils;
using S3K.RealTimeOnline.WebService.CommandHandlers;
using S3K.RealTimeOnline.WebService.Decorators;
using S3K.RealTimeOnline.WebService.QueryHandlers;
using S3K.RealTimeOnline.WebService.Services;
using S3K.RealTimeOnline.WebService.Tools;
using S3K.RealTimeOnline.WebService.UnitOfWork;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace S3K.RealTimeOnline.WebService
{
    public class ConfigContainer
    {
        public static readonly IDictionary<GenericCommandType, Type> GenericCommandHandlerTypeDictionary =
            new Dictionary<GenericCommandType, Type>
            {
                {GenericCommandType.Insert, typeof(GenericInsertCommandHandler<>)},
                {GenericCommandType.Update, typeof(GenericUpdateCommandHandler<>)},
                {GenericCommandType.Delete, typeof(GenericDeleteCommandHandler<>)},
                {GenericCommandType.DeleteById, typeof(GenericDeleteByIdCommandHandler<>)}
            };

        public static readonly IDictionary<UnitOfWorkType, Type> UnitOfWorkDictionary =
            new Dictionary<UnitOfWorkType, Type>
            {
                {UnitOfWorkType.Business, typeof(IBusinessUnitOfWork)},
                {UnitOfWorkType.Security, typeof(ISecurityUnitOfWork)},
            };

        private static readonly IDictionary<UnitOfWorkType, string> DomainAssembliesDictionary =
            new Dictionary<UnitOfWorkType, string>
            {
                {UnitOfWorkType.Business, "S3K.RealTimeOnline.BusinessDomain"},
                {UnitOfWorkType.Security, "S3K.RealTimeOnline.SecurityDomain"},
            };

        private static readonly IList<string> DataAccessAssemblyNames = new List<string>
        {
            "S3K.RealTimeOnline.BusinessDataAccess",
            "S3K.RealTimeOnline.SecurityDataAccess",
            "S3K.RealTimeOnline.CommonDataAccess"
        };

        public IUnityContainer Instance(IUnityContainer container)
        {
            return Build(container);
        }

        public IUnityContainer Instance()
        {
            return Build(new UnityContainer());
        }


        private IUnityContainer Build(IUnityContainer container)
        {
            container.RegisterType<ISecurityUnitOfWork, SecurityUnitOfWork>(new HierarchicalLifetimeManager(),
                new InjectionConstructor(DbManager.GetSqlConnection(AppConfig.SecurityDbConnectionName)));
            container.RegisterType<IBusinessUnitOfWork, BusinessUnitOfWork>(new HierarchicalLifetimeManager(),
                new InjectionConstructor(DbManager.GetSqlConnection(AppConfig.BusinessDbConnectionName)));
            IEnumerable<Assembly> currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            IEnumerable<Assembly> dataAccessAssemblies =
                currentAssemblies.Where(x => DataAccessAssemblyNames.Contains(x.GetName().Name));

            foreach (Assembly dataAccessAssembly in dataAccessAssemblies)
            {
                IEnumerable<Type> types = dataAccessAssembly.GetTypes().Where(t => t.GetInterfaces()
                                                                                       .Count(i => i.IsGenericType &&
                                                                                                   (i.GetGenericTypeDefinition() ==
                                                                                                    typeof(
                                                                                                        ICommandHandler<
                                                                                                        >) ||
                                                                                                    i.GetGenericTypeDefinition() ==
                                                                                                    typeof(IQueryHandler
                                                                                                        <,>))) >
                                                                                   0);

                foreach (Type type in types)
                {
                    Type[] interfaces = type.GetInterfaces();
                    container.RegisterType(interfaces.First(), type);
                }
            }

            container
                .RegisterType(
                    typeof(ICommandHandler<>),
                    typeof(TransactionCommandHandlerDecorator<>),
                    HandlerDecoratorType.TransactionCommand.ToString(),
                    new InjectionConstructor(new ResolvedParameter(typeof(ICommandHandler<>)))
                );

            container
                .RegisterType(
                    typeof(ICommandHandler<>),
                    typeof(ValidationCommandHandlerDecorator<>),
                    HandlerDecoratorType.ValidationCommand.ToString(),
                    new InjectionConstructor(new ResolvedParameter(typeof(ICommandHandler<>),
                        HandlerDecoratorType.TransactionCommand.ToString()))
                );

            foreach (KeyValuePair<UnitOfWorkType, Type> unitOfWorkEntry in UnitOfWorkDictionary)
            {
                Type genericSelectQueryHandlerType = typeof(GenericSelectQueryHandler<>)
                    .MakeGenericType(unitOfWorkEntry.Value);
                container.RegisterType(typeof(IGenericQueryHandler<,>), genericSelectQueryHandlerType,
                    unitOfWorkEntry.Key + "_" + GenericQueryType.Select);
            }

            foreach (KeyValuePair<UnitOfWorkType, Type> unitOfWorkEntry in UnitOfWorkDictionary)
            {
                Type genericSelectQueryHandlerType = typeof(GenericCountQueryHandler<>)
                    .MakeGenericType(unitOfWorkEntry.Value);
                container.RegisterType(typeof(IGenericQueryHandler<,>), genericSelectQueryHandlerType,
                    unitOfWorkEntry.Key + "_" + GenericQueryType.Count);
            }

            foreach (KeyValuePair<UnitOfWorkType, string> domainAssembliesEntry in DomainAssembliesDictionary)
            {
                IEnumerable<Type> domainTypes =
                    GetTypesByAssemblyName(typeof(Entity), domainAssembliesEntry.Value);

                foreach (Type domainType in domainTypes)
                {
                    Type genericCommandHandlerType =
                        typeof(GenericSelectByIdQueryHandler<,>).MakeGenericType(
                            UnitOfWorkDictionary[domainAssembliesEntry.Key], domainType);
                    container.RegisterType(genericCommandHandlerType);
                }
            }

            container
                .RegisterType(typeof(IQueryHandler<,>),
                    typeof(ValidationQueryHandlerDecorator<,>), HandlerDecoratorType.ValidationCommand.ToString(),
                    new InjectionConstructor(
                        new ResolvedParameter(typeof(IQueryHandler<,>))));


            foreach (GenericCommandType genericCommandType in Enum.GetValues(typeof(GenericCommandType)))
            {
                foreach (KeyValuePair<UnitOfWorkType, Type> unitOfWorkEntry in UnitOfWorkDictionary)
                {
                    Type genericCommandHandlerType = GenericCommandHandlerTypeDictionary[genericCommandType]
                        .MakeGenericType(unitOfWorkEntry.Value);
                    container.RegisterType(typeof(IGenericCommandHandler), genericCommandHandlerType,
                        unitOfWorkEntry.Key + "_" + genericCommandType);

                    container
                        .RegisterType(
                            typeof(IGenericCommandHandler),
                            typeof(TransactionGenericCommandHandlerDecorator),
                            HandlerDecoratorType.TransactionCommand + "_" + unitOfWorkEntry.Key + "_" +
                            genericCommandType,
                            new InjectionConstructor(new ResolvedParameter(typeof(IGenericCommandHandler),
                                unitOfWorkEntry.Key + "_" + genericCommandType))
                        );

                    if (genericCommandType == GenericCommandType.Insert ||
                        genericCommandType == GenericCommandType.Update)
                    {
                        container
                            .RegisterType(
                                typeof(IGenericCommandHandler),
                                typeof(ValidationGenericCommandHandlerDecorator),
                                HandlerDecoratorType.ValidationCommand + "_" + unitOfWorkEntry.Key + "_" +
                                genericCommandType,
                                new InjectionConstructor(new ResolvedParameter(typeof(IGenericCommandHandler),
                                    HandlerDecoratorType.TransactionCommand + "_" + unitOfWorkEntry.Key + "_" +
                                    genericCommandType))
                            );
                    }
                }
            }


            Type[] serviceTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(p => typeof(IService).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToArray();
            foreach (Type serviceType in serviceTypes)
            {
                Type contractType = serviceType.GetInterfaces().FirstOrDefault(p =>
                    typeof(IService).IsAssignableFrom(p) && p != typeof(IService));
                if (contractType != null)
                {
                    container.RegisterType(contractType, serviceType);
                }
            }

            return container;
        }

        private IEnumerable<Type> GetTypesByAssemblyName(Type type, string assemblyName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .First(x => x.GetName().Name == assemblyName).GetTypes()
                .Where(t => type.IsAssignableFrom(t));
        }
    }
}