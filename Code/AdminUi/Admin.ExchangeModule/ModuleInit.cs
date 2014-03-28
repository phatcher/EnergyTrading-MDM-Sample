﻿// This code was generated by a tool: InfrastructureTemplates\ModuleInitTemplate.tt
namespace Admin.ExchangeModule
{
    using System;

    using Admin.ExchangeModule.ViewModels;
    using Admin.ExchangeModule.Views;

    using Common.Services;

    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.MDM.Contracts.Sample;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;

    public class ModuleInit : IModule
    {
        private readonly IUnityContainer container;

        private readonly IApplicationMenuRegistry menuRegistry;

        public ModuleInit(IUnityContainer container, IApplicationMenuRegistry menuRegistry)
        {
            this.container = container;
            this.menuRegistry = menuRegistry;
        }

        public void Initialize()
        {
            this.Register();
            this.PopulateReferenceData();

            this.menuRegistry.RegisterMenuItem(
                "Exchange", 
                string.Empty, 
                typeof(ExchangeSearchResultsView), 
                new Uri(ExchangeViewNames.ExchangeAddView, UriKind.Relative), 
                "Name", 
                "_Name", 
                "PartyRole");
            this.menuRegistry.RegisterEntitySelector("Exchange", typeof(ExchangeSelectorView));
        }

        private void PopulateReferenceData()
        {
        }

        private void Register()
        {
            this.container.RegisterType<IMdmEntityService<Exchange>, MdmEntityService<Exchange>>(
                new ContainerControlledLifetimeManager(), 
                new InjectionConstructor(Server.Name + "exchange", new ResolvedParameter<IMessageRequester>()));
            this.container.RegisterType<object, ExchangeEditView>(ExchangeViewNames.ExchangeEditView);
            this.container.RegisterType<object, ExchangeAddView>(ExchangeViewNames.ExchangeAddView);
            this.container.RegisterType<object, ExchangeSearchResultsView>(ExchangeViewNames.ExchangeSearchResultsView);
            this.container.RegisterType<object, ExchangeEmbeddedSearchResultsView>(
                ExchangeViewNames.ExchangeEmbeddedSearchResultsView, 
                new ContainerControlledLifetimeManager());

            this.container.RegisterType<ExchangeAddViewModel>(
                new ContainerControlledLifetimeManager(), 
                new InjectionConstructor(
                    new ResolvedParameter<IEventAggregator>(), 
                    new ResolvedParameter<IMdmService>()));

            this.container.RegisterType<ExchangeEditViewModel>(
                new ContainerControlledLifetimeManager(), 
                new InjectionConstructor(
                    new ResolvedParameter<IEventAggregator>(), 
                    new ResolvedParameter<IMdmService>(), 
                    new ResolvedParameter<INavigationService>(), 
                    new ResolvedParameter<IMappingService>(), 
                    new ResolvedParameter<IApplicationCommands>()));

            this.container.RegisterType<ExchangeSelectorView>();
            this.container.RegisterType<ExchangeSelectorViewModel>();

            this.container.RegisterInstance<Func<int, IMdmEntity>>(
                "Exchange", 
                entityId =>
                    {
                        var entityService = container.Resolve<IMdmEntityService<Exchange>>();
                        var response = entityService.Get(entityId);
                        return response.Message;
                    });
        }
    }
}