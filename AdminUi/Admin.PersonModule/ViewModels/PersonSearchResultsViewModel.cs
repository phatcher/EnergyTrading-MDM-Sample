﻿// This code was generated by a tool: ViewModelTemplates\EntitySearchResultsViewModelTemplate.tt
namespace Admin.PersonModule.ViewModels
{
	using System.Windows;
	using System.Windows.Media;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Admin.PersonModule.Uris;
    using Admin.PersonModule.Views;

    using Common.Events;
    using Common.Extensions;
    using Common.Services;

    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    using EnergyTrading.Contracts.Search;
    using EnergyTrading.Mdm.Client.Services;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public class PersonSearchResultsViewModel : NotificationObject, IActiveAware
    {
        private readonly IMdmService entityService;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private bool isActive;

        private ObservableCollection<PersonViewModel> persons;

        private Search search;
        private PersonViewModel selectedPerson;

        public PersonSearchResultsViewModel(
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

        public ObservableCollection<PersonViewModel> Persons
        {
            get
            {
                return this.persons;
            }

            set
            {
                this.persons = value;
                this.RaisePropertyChanged(() => this.Persons);
                if (Persons != null && Persons.Count > 0 && SelectedPerson == null) SelectedPerson = Persons[0];            }
        }

        public PersonViewModel SelectedPerson
        {
            get
            {
                return this.selectedPerson;
            }

            set
            {
                this.selectedPerson = value;
                this.RaisePropertyChanged(() => this.SelectedPerson);
            }
        }
        
        public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && this.SelectedPerson != null)
            {
                this.navigationService.NavigateMain(new PersonEditUri(this.SelectedPerson.Id.Value, this.search.AsOf.Value));
            }
        }

        public void NavigateToDetailDoubleClick(object sender, MouseButtonEventArgs e)
        {
			DependencyObject src = VisualTreeHelper.GetParent((DependencyObject)e.OriginalSource);

		    if (src.GetType() == typeof(System.Windows.Controls.ContentPresenter))
		    {
	            if (this.SelectedPerson != null)
	            {
	                this.navigationService.NavigateMain(new PersonEditUri(this.SelectedPerson.Id.Value, this.search.AsOf.Value));
	            }
		    }
        }

        public void Sorting()
        {
            this.SelectedPerson = null;
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
            searchRequestEvent.Search.SearchOptions.OrderBy = "LastName";

            this.entityService.ExecuteAsyncSearch<Person>(
                this.search = searchRequestEvent.Search, 
                (response) =>
                    {
                        IList<Person> searchResults = response;
                        this.Persons =
                            new ObservableCollection<PersonViewModel>(
                                searchResults.Select(
                                    x =>
                                    new PersonViewModel(new EntityWithETag<Person>(x, null), this.eventAggregator)).OrderBy(y => y.Name));
                    }, 
                this.eventAggregator);
        }
    }
}
