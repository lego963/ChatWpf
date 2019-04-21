﻿using System.Windows.Controls;
using static ChatWpf.DI.DI;

namespace ChatWpf.Controls
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();

            // Set data context to settings view model
            DataContext = ViewModelSettings;
        }
    }
}
