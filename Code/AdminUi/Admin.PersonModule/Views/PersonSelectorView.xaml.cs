﻿// This code was generated by a tool: ViewTemplates\EntitySelectorViewCodeTemplate.tt
namespace Admin.PersonModule.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    using Admin.PersonModule.ViewModels;

    /// <summary>
    /// Interaction logic for PersonSelectorView.xaml
    /// </summary>
    public partial class PersonSelectorView : UserControl
    {
        public PersonSelectorView(PersonSelectorViewModel personSelectorViewModel)
        {
            this.DataContext = personSelectorViewModel;
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        public void SelectPersonMDC(object sender, MouseButtonEventArgs e)
        {
            ((PersonSelectorViewModel)DataContext).SelectPerson();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(SearchCriteriaTextBox);
        }
    }
}