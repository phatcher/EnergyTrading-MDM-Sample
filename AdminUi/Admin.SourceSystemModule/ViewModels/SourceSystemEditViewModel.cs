﻿// This code was generated by a tool: ViewModelTemplates\EntityEditViewModelTemplate.tt
namespace Admin.SourceSystemModule.ViewModels
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

    public class SourceSystemEditViewModel : NotificationObject, IConfirmNavigationRequest
    {
        private readonly InteractionRequest<Confirmation> confirmationFromViewModelInteractionRequest;
        private readonly IMdmService entityService;
        private readonly IEventAggregator eventAggregator;
        private readonly INavigationService navigationService;
        private readonly IMappingService mappingService;
        private ObservableCollection<MappingViewModel> mappings;
        private SourceSystemViewModel sourcesystem;
        private MappingViewModel selectedMapping;
        private ICommand deleteMappingCommand;
        private ICommand updateMappingCommand;
        private DateTime validAtString;
        private readonly IApplicationCommands applicationCommands;
        
        private bool showSourceSystems;

        public SourceSystemEditViewModel(
            IEventAggregator eventAggregator,
            IMdmService entityService,
            INavigationService navigationService,
            IMappingService mappingService,
            IApplicationCommands applicationCommands)
        {
            this.navigationService = navigationService;
            this.mappingService = mappingService;
            this.applicationCommands = applicationCommands;
            
            this.eventAggregator = eventAggregator;
            this.entityService = entityService;
            this.confirmationFromViewModelInteractionRequest = new InteractionRequest<Confirmation>();
            this.CanEdit = AuthorisationHelpers.HasEntityRights("SourceSystem");

        }

        public bool CanEdit { get; private set; }


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
            return AuthorisationHelpers.HasMappingRights("SourceSystem", system);
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
                this.eventAggregator.Publish(new MappingUpdateEvent(this.SourceSystem.Id.Value, mappingViewModel.MappingId.Value, mappingViewModel.MappingString, "SourceSystem"));
            }
        }

        private bool CanAddMappings()
        {
            foreach (var system in mappingService.GetSourceSystemNames())
            {
                if (AuthorisationHelpers.HasMappingRights("SourceSystem", system))
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

        public SourceSystemViewModel SourceSystem
        {
            get
            {
                return this.sourcesystem;
            }

            set
            {
                this.sourcesystem = value;
                this.RaisePropertyChanged(() => this.SourceSystem);
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
                            this.SourceSystem.Id.Value, "SourceSystem", Convert.ToInt32(this.SelectedMapping.MappingId), this.SourceSystem.Name));
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
            if (this.SourceSystem.CanSave)
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

            this.ShowSourceSystems = false;
            this.applicationCommands.CloseView("SourceSystemEmbeddedSearchResultsView", "SourceSystem-SourceSystemSearchResultsRegion");

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

            if (this.SourceSystem == null || this.validAtString != validAtStringParam ||
                this.SourceSystem.Id != idParam)
            {
                this.ShowSourceSystems = false;
                this.applicationCommands.CloseView("SourceSystemEmbeddedSearchResultsView", "SourceSystem-SourceSystemSearchResultsRegion");

            }

            this.validAtString = validAtStringParam;
            this.LoadSourceSystemFromService(idParam, validAtString);

            this.applicationCommands.OpenView("SourceSystemEmbeddedSearchResultsView", "SourceSystem-SourceSystemSearchResultsRegion", idParam, validAtStringParam, "SourceSystem");

            this.eventAggregator.Publish(new CanCreateNewChangeEvent(CanAddMappings()));
        }

        public bool ShowSourceSystems
        {
            get
            {
                return this.showSourceSystems;
            }

            set
            {
                this.showSourceSystems = value;
                this.RaisePropertyChanged(() => this.ShowSourceSystems);
            }
        }


        private void InitiateClone(CloneEvent obj)
        {
            this.navigationService.NavigateMain(new SourceSystemAddCloneUri(this.SourceSystem.Id.Value, this.SourceSystem.Start));
        }


        private void EntitySelected(EntitySelectedEvent obj)
        {
            switch (obj.EntityKey)
            {
                case "Parent":
                    this.SourceSystem.ParentId = obj.Id;
                    this.SourceSystem.ParentName = obj.EntityValue;
                    break;

            }
        }


        public void NavigateToParent()
        {
            this.navigationService.NavigateMain(new EntityEditUri("SourceSystem", this.SourceSystem.ParentId, this.SourceSystem.Start));
        }

        public void SelectParent()
        {
            this.eventAggregator.Publish(new EntitySelectEvent("SourceSystem", "Parent"));
        }

        public void DeleteParent()
        {
            this.SourceSystem.ParentId = null;
            this.SourceSystem.ParentName = string.Empty;
        }

        private void MappingUpdated(MappingUpdatedEvent updatedEvent)
        {
            if (updatedEvent.Cancelled)
                return;

            EntityWithETag<MdmId> entityWithETag;
            if (!TryGetMapping("SourceSystem", updatedEvent, out entityWithETag))
                return;

            if (entityWithETag.Object.EndDate <= updatedEvent.StartDate)
            {
                var message = string.Format("The start date of the new mapping must be before {0}", entityWithETag.Object.EndDate);
                this.eventAggregator.Publish(new ErrorEvent(message));
                return;
            }

            var newMapping = NewMapping(entityWithETag, updatedEvent);

            if (!TryCreateMapping("SourceSystem", newMapping, updatedEvent))
                return;

            if (!TryGetMapping("SourceSystem", updatedEvent, out entityWithETag))
                return;

            entityWithETag.Object.EndDate = updatedEvent.StartDate.AddSeconds(-1);

            if (!TryUpdateMapping("SourceSystem", entityWithETag, updatedEvent))
                return;

            this.LoadSourceSystemFromService(updatedEvent.EntityId, updatedEvent.StartDate, true);
            this.eventAggregator.Publish(new StatusEvent(Message.MappingUpdated));
        }

        private void MappingDeleteConfirmed(MappingDeleteConfirmedEvent obj)
        {
            if (obj.Cancelled)
                return;

            var response = this.mappingService.DeleteMapping(
                "SourceSystem",
                obj.MappingId,
                this.SourceSystem.Id.Value);

            if (response.IsValid)
            {
                this.LoadSourceSystemFromService(this.sourcesystem.Id.Value, validAtString, true);
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
            this.navigationService.NavigateMain(new MappingAddUri(this.SourceSystem.Id.Value, "SourceSystem", this.SourceSystem.Name));
        }

        private void LoadSourceSystemFromService(int sourcesystemId, DateTime validAt, bool publishChangeNotification = false)
        {
            this.entityService.ExecuteAsync(
                () => this.entityService.Get<SourceSystem>(sourcesystemId, validAt),
                (response) =>
                {
                    this.SourceSystem = new SourceSystemViewModel(new EntityWithETag<SourceSystem>(response.Message, response.Tag), this.eventAggregator);

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
                         () => this.entityService.Update(this.SourceSystem.Id.Value, this.SourceSystem.Model(), this.SourceSystem.ETag),
                         () => this.LoadSourceSystemFromService(this.SourceSystem.Id.Value, this.SourceSystem.Start, true),
                         string.Format(Message.EntityUpdatedFormatString, "SourceSystem"),
                         this.eventAggregator);
        }

        public void StartToday()
        {
            this.SourceSystem.Start = SystemTime.UtcNow().Date;
        }

        public void StartMinimum()
        {
            this.SourceSystem.Start = DateUtility.MinDate;
        }
    }
}
