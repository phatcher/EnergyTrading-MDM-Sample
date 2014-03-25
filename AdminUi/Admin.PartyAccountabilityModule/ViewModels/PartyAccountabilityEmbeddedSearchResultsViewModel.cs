﻿// This code was generated by a tool: ViewModelTemplates\EntityEmbeddedSearchResultsViewModelTemplate.tt

using Common;
using Microsoft.Practices.Prism.Regions;

namespace Admin.PartyAccountabilityModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Admin.PartyAccountabilityModule.Uris;
    using Admin.PartyAccountabilityModule.Views;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;
    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Search;

    public class PartyAccountabilityEmbeddedSearchResultsViewModel : NotificationObject, IActiveAware
    {
        private readonly IMdmService entityService;
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private bool isActive;

        private ObservableCollection<PartyAccountabilityViewModel> partyaccountabilitys;

        private Search search;
        private PartyAccountabilityViewModel selectedPartyAccountability;

        public PartyAccountabilityEmbeddedSearchResultsViewModel(
            INavigationService navigationService, 
            IEventAggregator eventAggregator, 
            IMdmService entityService, 
            IRegionManager regionManager)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
            this.regionManager = regionManager;
            this.IsActiveChanged += this.OnIsActiveChanged;
        }

        public event EventHandler IsActiveChanged;

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }

            set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                }

                this.IsActiveChanged(this, EventArgs.Empty);
            }
        }

        public ObservableCollection<PartyAccountabilityViewModel> PartyAccountabilitys
        {
            get
            {
                return this.partyaccountabilitys;
            }

            set
            {
                this.partyaccountabilitys = value;
                this.RaisePropertyChanged(() => this.PartyAccountabilitys);
                if (PartyAccountabilitys != null && PartyAccountabilitys.Count > 0 && SelectedPartyAccountability == null) 
                    SelectedPartyAccountability = PartyAccountabilitys[0];
            }
        }

        public PartyAccountabilityViewModel SelectedPartyAccountability
        {
            get
            {
                return this.selectedPartyAccountability;
            }

            set
            {
                this.selectedPartyAccountability = value;
                this.RaisePropertyChanged(() => this.SelectedPartyAccountability);
            }
        }
        
        public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.SelectedPartyAccountability != null)
            {
                this.navigationService.NavigateMain(new PartyAccountabilityEditUri(this.SelectedPartyAccountability.Id.Value, this.search.AsOf.Value));
            }
        }

        public void NavigateToDetailDoubleClick()
        {
            if (this.SelectedPartyAccountability != null)
            {
                this.navigationService.NavigateMain(new PartyAccountabilityEditUri(this.SelectedPartyAccountability.Id.Value, this.search.AsOf.Value));
            }
        }

        public void Sorting()
        {
            this.SelectedPartyAccountability = null;
        }

        private void OnIsActiveChanged(object sender, EventArgs eventArgs)
        {
            if (this.isActive)
            {
                Search search = SearchBuilder.CreateSearch();

                var context =
                    MyRegion(this.regionManager.Regions).Context as
                    Tuple<int, DateTime?, string>;
                search.AsOf = context.Item2;

                string field;
                switch (context.Item3)
                {
                    case "PartyAccountability":
                        field = "Parent.Id";
                        break;
                                    case "Person":
                        field = "SourcePerson.Id|TargetPerson.Id";
                        break;
                                    case "Party":
                        field = "SourceParty.Id|TargetParty.Id";
                        break;
                                    default:
                        field = context.Item3 + ".Id";
                        break;
                }
                
                if (field.Contains("|"))
                {
                    search.SearchFields.Combinator = SearchCombinator.Or;
                    var fields = field.Split(new Char[]{'|'});
                    
                    foreach (string f in fields)
                    {
                        search.AddSearchCriteria(SearchCombinator.And).AddCriteria(
                            f, SearchCondition.NumericEquals, context.Item1.ToString());
                    }
                }
                else
                {
                    search.AddSearchCriteria(SearchCombinator.And).AddCriteria(
                        field, SearchCondition.NumericEquals, context.Item1.ToString());
                }

                this.entityService.ExecuteAsyncSearch<PartyAccountability>(
                    this.search = search,
                    (response) =>
                        {
                            IList<PartyAccountability> searchResults = response;
                            this.PartyAccountabilitys =
                                new ObservableCollection<PartyAccountabilityViewModel>(
                                    searchResults.Select(
                                        x =>
                                        new PartyAccountabilityViewModel(
                                            new EntityWithETag<PartyAccountability>(x, null), this.eventAggregator)).
                                        OrderBy(y => y.Name));
                        },
                    this.eventAggregator, false);
                return;
            }

            this.PartyAccountabilitys = new ObservableCollection<PartyAccountabilityViewModel>();
        }

        IRegion MyRegion(IRegionCollection regions)
        {
            for (int i = regions.Count() - 1; i >= 0; i--)
            {
                if (regions.ElementAt(i).Name.EndsWith("-PartyAccountabilitySearchResultsRegion") && regions.ElementAt(i).Context != null)
                {
                    return regions.ElementAt(i);
                }
            }

            return null;
        }
    }
}