﻿// This code was generated by a tool: ViewTemplates\EntitySearchResultsViewCodeTemplate.tt
namespace Admin.BrokerModule.Views
{
    using System.Windows.Controls;
    using Admin.BrokerModule.ViewModels;

    /// <summary>
    /// Interaction logic for BrokerSearchResultsView.xaml
    /// </summary>
    public partial class BrokerSearchResultsView : UserControl
    {
        public BrokerSearchResultsView(BrokerSearchResultsViewModel brokerSearchResultsViewModel)
        {
            this.DataContext = brokerSearchResultsViewModel;
            this.InitializeComponent();
        }
    }
}
