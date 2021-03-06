﻿// This code was generated by a tool: ViewModelTemplates\EntitySearchResultsViewModelTemplate.tt
namespace Admin.PartyAccountabilityModule.ViewModels
{
	using System.Windows;
	using System.Windows.Media;

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

    public class PartyAccountabilitySearchResultsViewModel : NotificationObject, IActiveAware
    {
        private readonly IMdmService entityService;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private bool isActive;

        private ObservableCollection<PartyAccountabilityViewModel> partyaccountabilitys;

        private Search search;
        private PartyAccountabilityViewModel selectedPartyAccountability;

        public PartyAccountabilitySearchResultsViewModel(
            INavigationService navigationService, 
            IEventAggregator eventAggregator, 
            IMdmService entityService)
        {
            this.navigationService = navigationService;
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
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
                if (PartyAccountabilitys != null && PartyAccountabilitys.Count > 0 && SelectedPartyAccountability == null) SelectedPartyAccountability = PartyAccountabilitys[0];            }
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

        public void NavigateToDetailDoubleClick(object sender, MouseButtonEventArgs e)
        {
			DependencyObject src = VisualTreeHelper.GetParent((DependencyObject)e.OriginalSource);

		    if (src.GetType() == typeof(System.Windows.Controls.ContentPresenter))
		    {
	            if (this.SelectedPartyAccountability != null)
	            {
	                this.navigationService.NavigateMain(new PartyAccountabilityEditUri(this.SelectedPartyAccountability.Id.Value, this.search.AsOf.Value));
	            }
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
                this.eventAggregator.Subscribe<SearchRequestEvent>(this.SearchRequest);
                return;
            }

            this.eventAggregator.Unsubscribe<SearchRequestEvent>(this.SearchRequest);
        }

        private void SearchRequest(SearchRequestEvent searchRequestEvent)
        {
            searchRequestEvent.Search.SearchOptions.OrderBy = "Name";

            this.entityService.ExecuteAsyncSearch<PartyAccountability>(
                this.search = searchRequestEvent.Search, 
                (response) =>
                    {
                        IList<PartyAccountability> searchResults = response;
                        this.PartyAccountabilitys =
                            new ObservableCollection<PartyAccountabilityViewModel>(
                                searchResults.Select(
                                    x =>
                                    new PartyAccountabilityViewModel(new EntityWithETag<PartyAccountability>(x, null), this.eventAggregator)).OrderBy(y => y.Name));
                    }, 
                this.eventAggregator);
        }
    }
}
