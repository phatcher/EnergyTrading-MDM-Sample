﻿// This code was generated by a tool: ViewTemplates\EntityEditViewCodeTemplate.tt
namespace Admin.ExchangeModule.Views
{
    using System.Windows.Controls;
    using Admin.ExchangeModule.ViewModels;

    /// <summary>
    /// Interaction logic for ExchangeEditView.xaml
    /// </summary>
    public partial class ExchangeEditView : UserControl
    {
        public ExchangeEditView(ExchangeEditViewModel exchangeEditViewModel)
        {
            this.DataContext = exchangeEditViewModel;
            this.InitializeComponent();
        }
    }
}
