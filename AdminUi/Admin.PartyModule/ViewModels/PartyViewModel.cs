﻿// This code was generated by a tool: ViewModelTemplates\EntityViewModelTemplate.tt
namespace Admin.PartyModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Common.Events;
    using Common.Extensions;
    using Common.Framework;
    using Common.Services;

    using EnergyTrading.Mdm.Contracts;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    using EnergyTrading.MDM.Contracts.Sample;

    public class PartyViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;
        private readonly Party party;
        private bool canSave;

        private DateTime end;

        private DateTime start;

        public PartyViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.party = new Party
            {
                MdmSystemData = new SystemData { StartDate = DateUtility.MinDate, EndDate = DateUtility.MaxDate } 
            };

            this.Start = this.party.MdmSystemData.StartDate.Value;

            this.End = this.party.MdmSystemData.EndDate.Value;
        }

        public PartyViewModel(EntityWithETag<Party> ewe, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.party = ewe.Object;

            this.Id = this.party.MdmId();
            this.ETag = ewe.ETag;

            if (this.party.MdmSystemData != null && this.party.MdmSystemData.StartDate != null)
            {
                this.Start = this.party.MdmSystemData.StartDate.Value;
            }

            if (this.party.MdmSystemData != null && this.party.MdmSystemData.EndDate != null)
            {
                this.End = this.party.MdmSystemData.EndDate.Value;
            }

            this.Name = this.party.Details.Name;

            this.FaxNumber = this.party.Details.FaxNumber;

            this.TelephoneNumber = this.party.Details.TelephoneNumber;

            this.Role = this.party.Details.Role;

            this.IsInternal = this.party.Details.IsInternal;
        }

        private System.String name;
        public System.String Name 
        { 
            get { return this.name; }
            set { this.ChangeProperty(() => this.Name, ref this.name, value); }
        }

        private System.String faxnumber;
        public System.String FaxNumber 
        { 
            get { return this.faxnumber; }
            set { this.ChangeProperty(() => this.FaxNumber, ref this.faxnumber, value); }
        }

        private System.String telephonenumber;
        public System.String TelephoneNumber 
        { 
            get { return this.telephonenumber; }
            set { this.ChangeProperty(() => this.TelephoneNumber, ref this.telephonenumber, value); }
        }

        private System.String role;
        public System.String Role 
        { 
            get { return this.role; }
            set { this.ChangeProperty(() => this.Role, ref this.role, value); }
        }

        private bool isInternal;
        public bool IsInternal 
        {
            get { return this.isInternal; }
            set { this.ChangeProperty(() => this.IsInternal, ref this.isInternal, value); }
        }

        public bool CanSave
        {
            get
            {
                return this.canSave;
            }
            set
            {
                this.canSave = value;
                this.RaisePropertyChanged(() => this.CanSave);
            }
        }

        public string ETag { get; private set; }

        public DateTime End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.ChangeProperty(() => this.End, ref this.end, value);
            }
        }

        public int? Id { get; private set; }

        public DateTime Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.ChangeProperty(() => this.Start, ref this.start, value);
            }
        }

        public Party Model()
        {
            return new Party
            {
                Details = new PartyDetails 
                {
                    Name = this.Name
                    ,FaxNumber = this.FaxNumber
                    ,TelephoneNumber = this.TelephoneNumber
                    ,Role = this.Role
                    ,IsInternal = this.IsInternal
                }, 
                MdmSystemData = new SystemData { StartDate = this.Start, EndDate = this.End }
            };
        }

        private void ChangeProperty<T>(Expression<Func<T>> property, ref T variable, T newValue)
        {
            variable = newValue;
            this.RaisePropertyChanged(property);
            this.CanSave = this.HasChanges();
            this.eventAggregator.Publish(new CanSaveEvent(this.CanSave));
        }

        private bool HasChanges()
        {
            return
                !(
                    this.party.MdmSystemData.StartDate == this.Start 
                    && this.party.MdmSystemData.EndDate == this.End
                    && this.party.Details.Name == this.Name 
                    && this.party.Details.FaxNumber == this.FaxNumber 
                    && this.party.Details.TelephoneNumber == this.TelephoneNumber 
                    && this.party.Details.Role == this.Role 
                    && this.party.Details.IsInternal == this.IsInternal
                );
        }
    }
}