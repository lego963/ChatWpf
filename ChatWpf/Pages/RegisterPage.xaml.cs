﻿using System.Security;
using ChatWpf.Core.ViewModel.Application;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Pages
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor with specific view model
        /// </summary>
        public RegisterPage(RegisterViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        #endregion


        /// <summary>
        /// The secure password for this login page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
