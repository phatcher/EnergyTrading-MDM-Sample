﻿// This code was generated by a tool: ViewTemplates\EntityAddViewCodeTemplate.tt
namespace Admin.ExchangeModule.Views
{
    using System.Windows.Controls;

    using Admin.ExchangeModule.ViewModels;

    /// <summary>
    /// Interaction logic for ExchangeAddView.xaml
    /// </summary>
    public partial class ExchangeAddView : UserControl
    {
        public ExchangeAddView(ExchangeAddViewModel exchangeAddViewModel)
        {
            this.DataContext = exchangeAddViewModel;
            this.InitializeComponent();
        }
    }
}