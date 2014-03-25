﻿// This code was generated by a tool: ViewModelTemplates\EntityEditViewModelTemplate.tt
namespace Admin.LocationModule.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    using Common;
    
    using Common.Authorisation;
    using Common.Commands;
    using Common.Events;
    using Common.Extensions;
    using Common.Services;
    using Common.UI;
    using Common.UI.Uris;
    using Common.UI.ViewModels;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Prism.ViewModel;

    using EnergyTrading;
    using EnergyTrading.Mdm.Client.WebClient;
    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;
    using EnergyTrading.Mdm.Client.Services;

    using Uris;

    public class LocationEditViewModel : NotificationObject, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;
        private readonly IMdmService entityService;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private readonly IMappingService mappingService;
        private ObservableCollection<MappingViewModel> mappings;
        private LocationViewModel location;
        private MappingViewModel selectedMapping;
        private ICommand deleteMappingCommand;
        private ICommand updateMappingCommand;
        private DateTime validAtString;
        private readonly IApplicationCommands applicationCommands;
        
        private bool showBusinessUnits;
        private bool showCurves;
        private bool showLocations;
        private bool showMarkets;
        private bool showShipperCodes;

        public LocationEditViewModel(
            IEventAggregator eventAggregator,
            IMdmService entityService,
            INavigationService navigationService,
            IMappingService mappingService,
            IApplicationCommands applicationCommands
                            , IList<string> typeConfiguration
            )
        {
            this.navigationService = navigationService;
            this.mappingService = mappingService;
            this.applicationCommands = applicationCommands;
            
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.CanEdit = AuthorisationHelpers.HasEntityRights("Location");

            this.TypeConfiguration = typeConfiguration;
        }

        public bool CanEdit { get; private set; }

        public IList<string> TypeConfiguration
        {
            get;
            set;
        }

        public ICommand DeleteMappingCommand
        {
            get
            {
                if (this.deleteMappingCommand == null)
                {
                    this.deleteMappingCommand = new RelayCommand(param => this.DeleteMapping(param), param => CanEditOrDeleteMapping(param));
                }

                return this.deleteMappingCommand;
            }
        }

        public ICommand UpdateMappingCommand
        {
            get
            {
                if (this.updateMappingCommand == null)
                {
                    this.updateMappingCommand = new RelayCommand(param => this.UpdateMapping(param), param => CanEditOrDeleteMapping(param));
                }

                return this.updateMappingCommand;
            }
        }

        private bool CanEditOrDeleteMapping(object mapping)
        {
            var mappingViewModel = mapping as MappingViewModel;
            if (mappingViewModel == null)
            {
                return false;
            }

            if (mappingViewModel.MappingId == null)
            {
                return false;
            }

            return CanEditOrDeleteMapping(mappingViewModel.MappingId.Value);
        }

        private bool CanEditOrDeleteMapping(int mappingId)
        {
            var system = Mappings.Where(x => x.MappingId == mappingId).Select(x => x.SystemName).FirstOrDefault();
            return AuthorisationHelpers.HasMappingRights("Location", system);
        }

        private void DeleteMapping(object mapping)
        {
            var mappingViewModel = mapping as MappingViewModel;
            if (mappingViewModel == null)
            {
                return;
            }

            if (mappingViewModel.MappingId == null)
            {
                return;
            }

            this.eventAggregator.Publish(new ConfirmMappingDeleteEvent(mappingViewModel.MappingId.Value,
                mappingViewModel.MappingString, mappingViewModel.SystemName));
        }

        private void UpdateMapping(object mapping)
        {
            var mappingViewModel = mapping as MappingViewModel;
            if (mappingViewModel == null)
            {
                return;
            }

            if (mappingViewModel.MappingId == null)
            {
                return;
            }

            if (mappingViewModel != null)
            {
                this.eventAggregator.Publish(new MappingUpdateEvent(this.Location.Id.Value, mappingViewModel.MappingId.Value, mappingViewModel.MappingString, "Location"));
            }
        }

        private bool CanAddMappings()
        {
            foreach (var system in mappingService.GetSourceSystemNames())
            {
                if (AuthorisationHelpers.HasMappingRights("Location", system))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets the notification from view model interaction request. View binds to this property
        /// </summary>
        public IInteractionRequest ConfirmationFromViewModelInteractionRequest
        {
            get
            {
                return this.confirmationFromViewModelInteractionRequest;
            }
        }

        public ObservableCollection<MappingViewModel> Mappings
        {
            get
            {
                return this.mappings;
            }

            set
            {
                this.mappings = value;
                this.RaisePropertyChanged(() => this.Mappings);
            }
        }

        public LocationViewModel Location
        {
            get
            {
                return this.location;
            }

            set
            {
                this.location = value;
                this.RaisePropertyChanged(() => this.Location);
            }
        }

        public MappingViewModel SelectedMapping
        {
            get
            {
                return this.selectedMapping;
            }

            set
            {
                this.selectedMapping = value;
                this.RaisePropertyChanged(() => this.SelectedMapping);
            }
        }

        public void NavigateToDetail(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.NavigateToDetailScreen();
            }
        }

        public void NavigateToDetailDoubleClick()
        {
            this.NavigateToDetailScreen();
        }

        private void NavigateToDetailScreen()
        {
            if (this.SelectedMapping != null && CanEditOrDeleteMapping(this.SelectedMapping))
            {
                if (!this.SelectedMapping.IsMdmId)
                {
                    this.navigationService.NavigateMain(
                        new MappingEditUri(
                            this.Location.Id.Value, "Location", Convert.ToInt32(this.SelectedMapping.MappingId), this.Location.Name));
                    return;
                }

                this.eventAggregator.Publish(new StatusEvent("MdmSystemData ID cannot be edited"));
            }
        }

        public void Sorting()
        {
            this.SelectedMapping = null;
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (this.Location.CanSave)
            {
                this.eventAggregator.Publish(new DialogOpenEvent(true));
                this.confirmationFromViewModelInteractionRequest.Raise(
                    new Confirmation { Content = Message.UnsavedChanges, Title = Message.UnsavedChangeTitle },
                    confirmation =>
                    {
                        continuationCallback(confirmation.Confirmed);
                        this.eventAggregator.Publish(new DialogOpenEvent(false));
                    });
            }
            else
            {
                continuationCallback(true);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            this.eventAggregator.Unsubscribe<SaveEvent>(this.Save);
            this.eventAggregator.Unsubscribe<CreateEvent>(this.CreateMapping);
            this.eventAggregator.Unsubscribe<EntitySelectedEvent>(this.EntitySelected);
            this.eventAggregator.Unsubscribe<MappingUpdatedEvent>(this.MappingUpdated);
            this.eventAggregator.Unsubscribe<MappingDeleteConfirmedEvent>(this.MappingDeleteConfirmed);
            this.eventAggregator.Publish(new CanCloneChangeEvent(false));
            this.eventAggregator.Unsubscribe<CloneEvent>(this.InitiateClone);

            this.ShowBusinessUnits = false;
            this.applicationCommands.CloseView("BusinessUnitEmbeddedSearchResultsView", "Location-BusinessUnitSearchResultsRegion");

            this.ShowCurves = false;
            this.applicationCommands.CloseView("CurveEmbeddedSearchResultsView", "Location-CurveSearchResultsRegion");

            this.ShowLocations = false;
            this.applicationCommands.CloseView("LocationEmbeddedSearchResultsView", "Location-LocationSearchResultsRegion");

            this.ShowMarkets = false;
            this.applicationCommands.CloseView("MarketEmbeddedSearchResultsView", "Location-MarketSearchResultsRegion");

            this.ShowShipperCodes = false;
            this.applicationCommands.CloseView("ShipperCodeEmbeddedSearchResultsView", "Location-ShipperCodeSearchResultsRegion");

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.eventAggregator.Subscribe<SaveEvent>(this.Save);
            this.eventAggregator.Subscribe<CreateEvent>(this.CreateMapping);
            this.eventAggregator.Subscribe<CloneEvent>(this.InitiateClone);
            this.eventAggregator.Publish(new CanCloneChangeEvent(true));
            this.eventAggregator.Subscribe<MappingUpdatedEvent>(this.MappingUpdated);
            this.eventAggregator.Subscribe<MappingDeleteConfirmedEvent>(this.MappingDeleteConfirmed);
            this.eventAggregator.Subscribe<EntitySelectedEvent>(this.EntitySelected);
            int idParam = int.Parse(navigationContext.Parameters[NavigationParameters.EntityId]);
            DateTime validAtStringParam = DateTime.Parse(navigationContext.Parameters[NavigationParameters.ValidAtDate]);

            if (this.Location == null || this.validAtString != validAtStringParam ||
                this.Location.Id != idParam)
            {
                this.ShowBusinessUnits = false;
                this.applicationCommands.CloseView("BusinessUnitEmbeddedSearchResultsView", "Location-BusinessUnitSearchResultsRegion");

                this.ShowCurves = false;
                this.applicationCommands.CloseView("CurveEmbeddedSearchResultsView", "Location-CurveSearchResultsRegion");

                this.ShowLocations = false;
                this.applicationCommands.CloseView("LocationEmbeddedSearchResultsView", "Location-LocationSearchResultsRegion");

                this.ShowMarkets = false;
                this.applicationCommands.CloseView("MarketEmbeddedSearchResultsView", "Location-MarketSearchResultsRegion");

                this.ShowShipperCodes = false;
                this.applicationCommands.CloseView("ShipperCodeEmbeddedSearchResultsView", "Location-ShipperCodeSearchResultsRegion");

            }

            this.validAtString = validAtStringParam;
            this.LoadLocationFromService(idParam, validAtString);

            this.applicationCommands.OpenView("BusinessUnitEmbeddedSearchResultsView", "Location-BusinessUnitSearchResultsRegion", idParam, validAtStringParam, "Location");

            this.applicationCommands.OpenView("CurveEmbeddedSearchResultsView", "Location-CurveSearchResultsRegion", idParam, validAtStringParam, "Location");

            this.applicationCommands.OpenView("LocationEmbeddedSearchResultsView", "Location-LocationSearchResultsRegion", idParam, validAtStringParam, "Location");

            this.applicationCommands.OpenView("MarketEmbeddedSearchResultsView", "Location-MarketSearchResultsRegion", idParam, validAtStringParam, "Location");

            this.applicationCommands.OpenView("ShipperCodeEmbeddedSearchResultsView", "Location-ShipperCodeSearchResultsRegion", idParam, validAtStringParam, "Location");

            this.eventAggregator.Publish(new CanCreateNewChangeEvent(CanAddMappings()));
        }

        public bool ShowBusinessUnits
        {
            get
            {
                return this.showBusinessUnits;
            }

            set
            {
                this.showBusinessUnits = value;
                this.RaisePropertyChanged(() => this.ShowBusinessUnits);
            }
        }
        public bool ShowCurves
        {
            get
            {
                return this.showCurves;
            }

            set
            {
                this.showCurves = value;
                this.RaisePropertyChanged(() => this.ShowCurves);
            }
        }
        public bool ShowLocations
        {
            get
            {
                return this.showLocations;
            }

            set
            {
                this.showLocations = value;
                this.RaisePropertyChanged(() => this.ShowLocations);
            }
        }
        public bool ShowMarkets
        {
            get
            {
                return this.showMarkets;
            }

            set
            {
                this.showMarkets = value;
                this.RaisePropertyChanged(() => this.ShowMarkets);
            }
        }
        public bool ShowShipperCodes
        {
            get
            {
                return this.showShipperCodes;
            }

            set
            {
                this.showShipperCodes = value;
                this.RaisePropertyChanged(() => this.ShowShipperCodes);
            }
        }


        private void InitiateClone(CloneEvent obj)
        {
            this.navigationService.NavigateMain(new LocationAddCloneUri(this.Location.Id.Value, this.Location.Start));
        }


        private void EntitySelected(EntitySelectedEvent obj)
        {
            switch (obj.EntityKey)
            {
                case "Parent":
                    this.Location.ParentId = obj.Id;
                    this.Location.ParentName = obj.EntityValue;
                    break;

            }
        }


        public void NavigateToParent()
        {
            this.navigationService.NavigateMain(new EntityEditUri("Location", this.Location.ParentId, this.Location.Start));
        }

        public void SelectParent()
        {
            this.eventAggregator.Publish(new EntitySelectEvent("Location", "Parent"));
        }

        public void DeleteParent()
        {
            this.Location.ParentId = null;
            this.Location.ParentName = string.Empty;
        }

        private void MappingUpdated(MappingUpdatedEvent updatedEvent)
        {
            if (updatedEvent.Cancelled)
                return;

            EntityWithETag<MdmId> entityWithETag;
            if (!TryGetMapping("Location", updatedEvent, out entityWithETag))
                return;

            if (entityWithETag.Object.EndDate <= updatedEvent.StartDate)
            {
                var message = string.Format("The start date of the new mapping must be before {0}", entityWithETag.Object.EndDate);
                this.eventAggregator.Publish(new ErrorEvent(message));
                return;
            }

            var newMapping = NewMapping(entityWithETag, updatedEvent);

            if (!TryCreateMapping("Location", newMapping, updatedEvent))
                return;

            if (!TryGetMapping("Location", updatedEvent, out entityWithETag))
                return;

            entityWithETag.Object.EndDate = updatedEvent.StartDate.AddSeconds(-1);

            if (!TryUpdateMapping("Location", entityWithETag, updatedEvent))
                return;

            this.LoadLocationFromService(updatedEvent.EntityId, updatedEvent.StartDate, true);
            this.eventAggregator.Publish(new StatusEvent(Message.MappingUpdated));
        }

        private void MappingDeleteConfirmed(MappingDeleteConfirmedEvent obj)
        {
            if (obj.Cancelled)
                return;

            var response = this.mappingService.DeleteMapping(
                "Location",
                obj.MappingId,
                this.Location.Id.Value);

            if (response.IsValid)
            {
                this.LoadLocationFromService(this.location.Id.Value, validAtString, true);
                this.eventAggregator.Publish(new StatusEvent(Message.MappingDeleted));
                return;
            }

            this.eventAggregator.Publish(
                new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unknown Error"));
        }

        private bool TryGetMapping(string entityName, MappingUpdatedEvent updatedEvent, out EntityWithETag<MdmId> mapping)
        {
            mapping = mappingService.GetMapping(entityName, updatedEvent.EntityId, updatedEvent.MappingId);
            if (mapping.Object == null)
            {
                this.eventAggregator.Publish(
                    new ErrorEvent("Unable to retrieve original mapping"));
                return false;
            }
            return true;
        }

        private static MdmId NewMapping(EntityWithETag<MdmId> entityWithETag, MappingUpdatedEvent updatedEvent)
        {
            return new MdmId
                {
                    DefaultReverseInd = updatedEvent.IsDefault,
                    IsMdmId = false,
                    SourceSystemOriginated = updatedEvent.IsSourceSystemOriginated,
                    StartDate = updatedEvent.StartDate,
                    Identifier = updatedEvent.NewValue,
                    EndDate = entityWithETag.Object.EndDate,
                    SystemName = entityWithETag.Object.SystemName,
                };
        }

        private bool TryUpdateMapping(string entityName, EntityWithETag<MdmId> entityWithETag, MappingUpdatedEvent updatedEvent)
        {
            var response = mappingService.UpdateMapping(entityName, updatedEvent.MappingId, updatedEvent.EntityId, entityWithETag);
            if (!response.IsValid)
            {
                this.eventAggregator.Publish(
                    new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unknown Error"));
                return false;
            }
            return true;
        }

        private bool TryCreateMapping(string entityName, MdmId newMapping, MappingUpdatedEvent updatedEvent)
        {
            var response = mappingService.CreateMapping(entityName, updatedEvent.EntityId, newMapping);
            if (!response.IsValid)
            {
                this.eventAggregator.Publish(
                    new ErrorEvent(response.Fault != null ? response.Fault.Message : "Unknown Error"));
                return false;
            }
            return true;
        }

        private void CreateMapping(CreateEvent obj)
        {
            this.navigationService.NavigateMain(new MappingAddUri(this.Location.Id.Value, "Location", this.Location.Name));
        }

        private void LoadLocationFromService(int locationId, DateTime validAt, bool publishChangeNotification = false)
        {
            this.entityService.ExecuteAsync(
                () => this.entityService.Get<Location>(locationId, validAt),
                (response) =>
                {
                    this.Location = new LocationViewModel(new EntityWithETag<Location>(response.Message, response.Tag), this.eventAggregator);

                    this.Mappings =
                        new ObservableCollection<MappingViewModel>(
                            response.Message.Identifiers.Select(
                                nexusId =>
                                new MappingViewModel(
                                    new EntityWithETag<MdmId>(nexusId, null), this.eventAggregator)));

                    this.RaisePropertyChanged(string.Empty);
                },
                this.eventAggregator);
        }

        private void Save(SaveEvent saveEvent)
        {
            this.entityService.ExecuteAsync(
                         () => this.entityService.Update(this.Location.Id.Value, this.Location.Model(), this.Location.ETag),
                         () => this.LoadLocationFromService(this.Location.Id.Value, this.Location.Start, true),
                         string.Format(Message.EntityUpdatedFormatString, "Location"),
                         this.eventAggregator);
        }

        public void StartToday()
        {
            this.Location.Start = SystemTime.UtcNow().Date;
        }

        public void StartMinimum()
        {
            this.Location.Start = DateUtility.MinDate;
        }
    }
}
