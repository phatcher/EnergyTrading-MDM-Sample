﻿// This code was generated by a tool: ViewTemplates\EntitySelectorViewCodeTemplate.tt

using System.Windows;
using System.Windows.Input;

namespace Admin.LocationModule.Views
{
    using System.Windows.Controls;
    using Admin.LocationModule.ViewModels;

    /// <summary>
    /// Interaction logic for LocationSelectorView.xaml
    /// </summary>
    public partial class LocationSelectorView : UserControl
    {
        public LocationSelectorView(LocationSelectorViewModel locationSelectorViewModel)
        {
            this.DataContext = locationSelectorViewModel;
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        public void SelectLocationMDC(object sender, MouseButtonEventArgs e)
        {
            ((LocationSelectorViewModel)DataContext).SelectLocation();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(SearchCriteriaTextBox);
        }
    }
}