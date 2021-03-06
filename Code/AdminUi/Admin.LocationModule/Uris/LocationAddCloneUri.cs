﻿// This code was generated by a tool: InfrastructureTemplates\EntityAddCloneUriTemplate.tt
namespace Admin.LocationModule.Uris
{
    using System;

    using Admin.LocationModule.Views;

    using Common;

    public class LocationAddCloneUri : Uri
    {
        public LocationAddCloneUri(int locationId, DateTime validAt)
            : base(
                LocationViewNames.LocationAddCloneView
                + string.Format(
                    "?{0}={1}&{2}={3}", 
                    NavigationParameters.EntityId, 
                    locationId, 
                    NavigationParameters.ValidAtDate, 
                    validAt), 
                UriKind.Relative)
        {
        }
    }
}