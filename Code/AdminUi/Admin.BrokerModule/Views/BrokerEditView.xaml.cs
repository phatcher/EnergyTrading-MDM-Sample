﻿// This code was generated by a tool: ViewTemplates\EntityEditViewCodeTemplate.tt
namespace Admin.BrokerModule.Views
{
    using System.Windows.Controls;

    using Admin.BrokerModule.ViewModels;

    /// <summary>
    /// Interaction logic for BrokerEditView.xaml
    /// </summary>
    public partial class BrokerEditView : UserControl
    {
        public BrokerEditView(BrokerEditViewModel brokerEditViewModel)
        {
            this.DataContext = brokerEditViewModel;
            this.InitializeComponent();
        }
    }
}