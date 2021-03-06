﻿// This code was generated by a tool: InfrastructureTemplates\EntityEditUriTemplate.tt
namespace Admin.SourceSystemModule.Uris
{
    using System;

    using Admin.SourceSystemModule.Views;

    using Common;

    public class SourceSystemEditUri : Uri
    {
        public SourceSystemEditUri(int sourcesystemId, DateTime validAt)
            : base(
                SourceSystemViewNames.SourceSystemEditView
                + string.Format(
                    "?{0}={1}&{2}={3}", 
                    NavigationParameters.EntityId, 
                    sourcesystemId, 
                    NavigationParameters.ValidAtDate, 
                    validAt), 
                UriKind.Relative)
        {
        }
    }
}