﻿// This code was generated by a tool: ViewTemplates\EntitySearchResultsViewCodeTemplate.tt
namespace Admin.LegalEntityModule.Views
{
    using System.Windows.Controls;

    using Admin.LegalEntityModule.ViewModels;

    /// <summary>
    /// Interaction logic for LegalEntitySearchResultsView.xaml
    /// </summary>
    public partial class LegalEntitySearchResultsView : UserControl
    {
        public LegalEntitySearchResultsView(LegalEntitySearchResultsViewModel legalentitySearchResultsViewModel)
        {
            this.DataContext = legalentitySearchResultsViewModel;
            this.InitializeComponent();
        }
    }
}