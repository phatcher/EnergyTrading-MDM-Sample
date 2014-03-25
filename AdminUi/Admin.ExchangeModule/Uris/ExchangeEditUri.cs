﻿// This code was generated by a tool: InfrastructureTemplates\EntityEditUriTemplate.tt
namespace Admin.ExchangeModule.Uris
{
    using System;
    using Admin.ExchangeModule.Views;
    using Common;

    public class ExchangeEditUri : Uri
    {
        public ExchangeEditUri(int exchangeId, DateTime validAt) : 
            base(ExchangeViewNames.ExchangeEditView + string.Format("?{0}={1}&{2}={3}", NavigationParameters.EntityId, exchangeId, NavigationParameters.ValidAtDate, validAt), UriKind.Relative)
        {
        }
    }
}
    