﻿// This code was generated by a tool: ViewTemplates\EntitySelectorViewCodeTemplate.tt

using System.Windows;
using System.Windows.Input;

namespace Admin.PartyRoleModule.Views
{
    using System.Windows.Controls;
    using Admin.PartyRoleModule.ViewModels;

    /// <summary>
    /// Interaction logic for PartyRoleSelectorView.xaml
    /// </summary>
    public partial class PartyRoleSelectorView : UserControl
    {
        public PartyRoleSelectorView(PartyRoleSelectorViewModel partyroleSelectorViewModel)
        {
            this.DataContext = partyroleSelectorViewModel;
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        public void SelectPartyRoleMDC(object sender, MouseButtonEventArgs e)
        {
            ((PartyRoleSelectorViewModel)DataContext).SelectPartyRole();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(SearchCriteriaTextBox);
        }
    }
}