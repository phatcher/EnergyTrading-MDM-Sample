﻿// This code was generated by a tool: ViewTemplates\EntityAddViewCodeTemplate.tt
namespace Admin.BrokerModule.Views
{
    using System.Windows.Controls;

    using Admin.BrokerModule.ViewModels;

    /// <summary>
    /// Interaction logic for BrokerAddView.xaml
    /// </summary>
    public partial class BrokerAddView : UserControl
    {
        public BrokerAddView(BrokerAddViewModel brokerAddViewModel)
        {
            this.DataContext = brokerAddViewModel;
            this.InitializeComponent();
        }
    }
}