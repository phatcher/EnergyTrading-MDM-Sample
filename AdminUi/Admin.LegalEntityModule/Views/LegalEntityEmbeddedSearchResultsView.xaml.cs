﻿// This code was generated by a tool: ViewTemplates\EntityEmbeddedSearchResultsViewCodeTemplate.tt
namespace Admin.LegalEntityModule.Views
{
    using System.Windows.Controls;
    using Admin.LegalEntityModule.ViewModels;

    /// <summary>
    /// Interaction logic for LegalEntityEmbeddedSearchResultsView.xaml
    /// </summary>
    public partial class LegalEntityEmbeddedSearchResultsView : UserControl
    {
        public LegalEntityEmbeddedSearchResultsView(LegalEntityEmbeddedSearchResultsViewModel legalentityEmbeddedSearchResultsViewModel)
        {
            this.DataContext = legalentityEmbeddedSearchResultsViewModel;
            this.InitializeComponent();
        }
    }
}
