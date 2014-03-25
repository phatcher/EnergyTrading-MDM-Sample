﻿// This code was generated by a tool: ViewModelTemplates\EntityViewModelTemplate.tt
namespace Admin.PartyRoleModule.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Common.Events;
    using Common.Extensions;
    using Common.Framework;
    using Common.Services;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.ViewModel;

    using EnergyTrading.MDM.Contracts.Sample; using EnergyTrading.Mdm.Contracts;

    public class PartyRoleViewModel : NotificationObject
    {
        private readonly IEventAggregator eventAggregator;
        private readonly PartyRole partyrole;
        private bool canSave;

        private DateTime end;

        private DateTime start;

        public PartyRoleViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.partyrole = new PartyRole
            {
                MdmSystemData = new SystemData { StartDate = DateUtility.MinDate, EndDate = DateUtility.MaxDate } 
            };

            this.Start = this.partyrole.MdmSystemData.StartDate.Value;

            this.End = this.partyrole.MdmSystemData.EndDate.Value;
        }

        public PartyRoleViewModel(EntityWithETag<PartyRole> ewe, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.partyrole = ewe.Object;

            this.Id = this.partyrole.MdmId();
            this.ETag = ewe.ETag;

            if (this.partyrole.MdmSystemData != null && this.partyrole.MdmSystemData.StartDate != null)
            {
                this.Start = this.partyrole.MdmSystemData.StartDate.Value;
            }

            if (this.partyrole.MdmSystemData != null && this.partyrole.MdmSystemData.EndDate != null)
            {
                this.End = this.partyrole.MdmSystemData.EndDate.Value;
            }

            this.Name = this.partyrole.Details.Name;

            this.PartyRoleType = this.partyrole.PartyRoleType;

            this.PartyId = this.partyrole.Party.MdmId();

            this.PartyName = this.partyrole.Party != null ? this.partyrole.Party.Name : null;
        }

        private System.String name;
        public System.String Name 
        { 
            get { return this.name; }
            set { this.ChangeProperty(() => this.Name, ref this.name, value); }
        }

        private System.String partyroletype;
        public System.String PartyRoleType 
        { 
            get { return this.partyroletype; }
            set { this.ChangeProperty(() => this.PartyRoleType, ref this.partyroletype, value); }
        }

        private int? partyId;
        public int? PartyId 
        {
            get { return this.partyId; }
            set { this.ChangeProperty(() => this.PartyId, ref this.partyId, value); }
        }

        private string partyName;
        public string PartyName 
        {
            get { return this.partyName; }
            set { this.partyName = value; this.RaisePropertyChanged(() => this.PartyName); }
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

        public PartyRole Model()
        {
            return new PartyRole
            {
                Details = new PartyRoleDetails 
                {
                    Name = this.Name
                }, 
                MdmSystemData = new SystemData { StartDate = this.Start, EndDate = this.End }
                    ,PartyRoleType = this.PartyRoleType
                    ,Party = this.PartyId == null ? null :
                        new EntityId { Identifier = new MdmId { IsMdmId = true, Identifier = this.PartyId.ToString() } }
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
                    this.partyrole.MdmSystemData.StartDate == this.Start 
                    && this.partyrole.MdmSystemData.EndDate == this.End
                    && this.partyrole.Details.Name == this.Name 
                    && this.partyrole.PartyRoleType == this.PartyRoleType 
                    && this.PartyHasNoChanges()
                );
        }

        private bool PartyHasNoChanges()
        {
            int? id;
            if (this.partyrole.Party == null)
            {
                id = null;
            }
            else
            {
                id = this.partyrole.Party.Identifier.Identifier.ParseToNullableInt();
            }

            return id == this.partyId;
        }

        public string EditFormViewName
        {
            get
            {
                IList<string> derivedTypes = new List<string> {
                "Broker"
                ,"BusinessUnit"
                ,"Exchange"
                                };
                if (derivedTypes.Contains(partyrole.PartyRoleType))
                    return partyrole.PartyRoleType.Replace(" ", string.Empty) + "EditView";
                return "PartyRoleEditView";
            }
        }
    }
}
