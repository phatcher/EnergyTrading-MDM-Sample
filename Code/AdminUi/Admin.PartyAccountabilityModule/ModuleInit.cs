﻿// This code was generated by a tool: InfrastructureTemplates\ModuleInitTemplate.tt
namespace Admin.PartyAccountabilityModule
{
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Linq;

    using Admin.PartyAccountabilityModule.ViewModels;
    using Admin.PartyAccountabilityModule.Views;

    
    using Common.Services;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;

    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

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

            this.menuRegistry.RegisterMenuItem("PartyAccountability", string.Empty, typeof(PartyAccountabilitySearchResultsView), new Uri(PartyAccountabilityViewNames.PartyAccountabilityAddView, UriKind.Relative), "Name", "_Name");
            this.menuRegistry.RegisterEntitySelector("PartyAccountability", typeof(PartyAccountabilitySelectorView));
        }

        private void Register()
        {
            this.container.RegisterType<IMdmEntityService<PartyAccountability>, MdmEntityService<PartyAccountability>>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(Server.Name + "partyaccountability", new ResolvedParameter<IMessageRequester>()));
            this.container.RegisterType<object, PartyAccountabilityEditView>(PartyAccountabilityViewNames.PartyAccountabilityEditView);
            this.container.RegisterType<object, PartyAccountabilityAddView>(PartyAccountabilityViewNames.PartyAccountabilityAddView);
            this.container.RegisterType<object, PartyAccountabilitySearchResultsView>(PartyAccountabilityViewNames.PartyAccountabilitySearchResultsView);
            this.container.RegisterType<object, PartyAccountabilityEmbeddedSearchResultsView>(PartyAccountabilityViewNames.PartyAccountabilityEmbeddedSearchResultsView, new ContainerControlledLifetimeManager());
                    
            this.container.RegisterType<PartyAccountabilityAddViewModel>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                new ResolvedParameter<IEventAggregator>(),
                new ResolvedParameter<IMdmService>()
                          ,new ResolvedParameter<IList<string>>("partyaccountabilitytypes")));

            this.container.RegisterType<PartyAccountabilityEditViewModel>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(
                new ResolvedParameter<IEventAggregator>(),
                new ResolvedParameter<IMdmService>(),
                new ResolvedParameter<INavigationService>(),
                new ResolvedParameter<IMappingService>(),
                new ResolvedParameter<IApplicationCommands>()
                                ,new ResolvedParameter<IList<string>>("partyaccountabilitytypes")));

                    
            this.container.RegisterType<PartyAccountabilitySelectorView>();
            this.container.RegisterType<PartyAccountabilitySelectorViewModel>();
			
            this.container.RegisterInstance<Func<int, IMdmEntity>>("PartyAccountability",
                (entityId) =>
                    {
                        var entityService = container.Resolve<IMdmEntityService<PartyAccountability>>();
                        var response = entityService.Get(entityId);
                        return response.Message;
                    });
        }

        private void PopulateReferenceData()
        {
                    ReferenceDataList message = null;
                    message = this.container.Resolve<IMessageRequester>().Request<ReferenceDataList>(
                    Server.Name + "ReferenceData/list/PartyAccountabilityType").Message;
            IList<string> partyaccountabilitytype = message != null ?
                message.Select(data => data.Value).OrderBy(s => s).ToList() : new List<string>();

            this.container.RegisterInstance("partyaccountabilitytypes", partyaccountabilitytype, new ContainerControlledLifetimeManager());
                  }
    }
}
